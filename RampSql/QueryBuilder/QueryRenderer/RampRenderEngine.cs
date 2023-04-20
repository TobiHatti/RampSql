using RampSql.QueryBuilder.QueryBuilder;
using RampSql.Schema;
using System.Runtime.InteropServices;
using System.Text;

namespace RampSql.QueryBuilder
{
    public enum RampRFormat
    {
        RealName,
        AliasName,
        AliasDeclaring,
        Parameter,
        ParameterAliasDeclaring
    }

    public class RampRenderEngine
    {
        private List<IRampRenderInstruction> instructions = new List<IRampRenderInstruction>();
        public List<object> Parameters { get; } = new List<object>();

        public RampRenderEngine Instruction(string value)
        {
            instructions.Add(new RampRenderConstant(this, value));
            return this;
        }

        public RampRenderEngine Raw(string value)
        {
            instructions.Add(new RampRenderRaw(this, value));
            return this;
        }

        public RampRenderEngine Value(IRampValue value, RampRFormat format)
        {
            if (value is IRampQuery) return Instruction("(").Query((IRampQuery)value, "").Instruction(") AS").Raw(((IRampQuery)value).GetData().QueryAlias);
            if (value is IRampFunction) return Function((IRampFunction)value, null);
            instructions.Add(new RampRenderValue(this, value, format));
            return this;
        }

        public RampRenderEngine Query(IRampQuery query, string alias = null)
        {
            RampRenderEngine engine = query.GetRenderer();
            instructions.AddRange(engine.instructions);
            Parameters.AddRange(engine.Parameters);
            return this;
        }

        public RampRenderEngine Column(IRampColumn column, RampRFormat format, string alias = null)
        {
            instructions.Add(new RampRenderColumn(this, column, format));
            return this;
        }

        public RampRenderEngine Table(RampTableData table, RampRFormat format, string alias = null)
        {
            instructions.Add(new RampRenderTable(this, table, format));
            table.Alias = alias;
            return this;
        }

        public RampRenderEngine Target(IRampTarget target, string alias = null)
        {
            if (target is IRampQuery) return Instruction("(").Query((IRampQuery)target).Instruction(") AS").Raw(((IRampQuery)target).GetData().QueryAlias);
            instructions.Add(new RampRenderTarget(this, target));
            target.AsAlias(alias);
            return this;
        }


        public RampRenderEngine Function(IRampFunction function, string alias = null)
        {
            RampRenderEngine engine = function.GetRenderer();
            instructions.AddRange(engine.instructions);
            Parameters.AddRange(engine.Parameters);
            return this;
        }

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IRampRenderInstruction instr in CollectionsMarshal.AsSpan(instructions))
            {
                sb.Append($"{instr.Render()} ");
            }
            return sb.ToString();
        }
    }



    public class RampRenderConstant : IRampRenderInstruction
    {
        private string Value { get; }
        private RampRenderEngine Engine { get; }
        public RampRenderConstant(RampRenderEngine engine, string value)
        {
            Engine = engine;
            Value = value;
        }

        public string Render() => Value;
    }

    public class RampRenderRaw : IRampRenderInstruction
    {
        private string Value { get; }
        private RampRenderEngine Engine { get; }
        public RampRenderRaw(RampRenderEngine engine, string value)
        {
            Engine = engine;
            Value = value;
        }

        public string Render() => Value;
    }

    public class RampRenderTable : IRampRenderInstruction
    {
        private RampTableData Table { get; }
        private RampRFormat Format { get; }
        private RampRenderEngine Engine { get; }
        public RampRenderTable(RampRenderEngine engine, RampTableData table, RampRFormat format)
        {
            Engine = engine;
            Table = table;
            Format = format;
            if (Format == RampRFormat.Parameter) Engine.Parameters.AddRange(Table.GetParameterValues());
        }

        public string Render()
        {
            switch (Format)
            {
                case RampRFormat.RealName: return Table.QuotedTableName;
                case RampRFormat.AliasName: return Table.QuotedSelectorName;
                case RampRFormat.AliasDeclaring: return Table.AliasDeclaring;
                case RampRFormat.Parameter: return "?";
            }
            throw new RampException("");
        }
    }

    public class RampRenderValue : IRampRenderInstruction
    {
        private IRampValue Value { get; }
        private RampRFormat Format { get; }
        private RampRenderEngine Engine { get; }
        public RampRenderValue(RampRenderEngine engine, IRampValue value, RampRFormat format)
        {
            Engine = engine;
            Value = value;
            Format = format;
            if (Format == RampRFormat.Parameter) Engine.Parameters.AddRange(Value.GetParameterValues());
        }

        public string Render()
        {
            switch (Format)
            {
                case RampRFormat.RealName: return Value.RealName;
                case RampRFormat.AliasName: return Value.QuotedSelectorName;
                case RampRFormat.AliasDeclaring: return Value.AliasDeclaring;
                case RampRFormat.Parameter: return "?";
                case RampRFormat.ParameterAliasDeclaring: return $"? AS {Value.QuotedSelectorName}";
            }
            throw new RampException("");
        }
    }

    //public class RampRenderQuery : IRampRenderInstruction
    //{
    //    private IRampQuery Query { get; }
    //    private RampRFormat Format { get; }
    //    public RampRenderQuery(IRampQuery query, RampRFormat format)
    //    {
    //        Query = query;
    //        Format = format;
    //    }

    //    public string Render(RampRenderEngine engine) => string.Empty;
    //}

    public class RampRenderColumn : IRampRenderInstruction
    {
        private IRampColumn Column { get; }
        private RampRFormat Format { get; }
        private RampRenderEngine Engine { get; }
        public RampRenderColumn(RampRenderEngine engine, IRampColumn column, RampRFormat format)
        {
            Engine = engine;
            Column = column;
            Format = format;
            if (Format == RampRFormat.Parameter) Engine.Parameters.AddRange(Column.GetParameterValues());
        }

        public string Render()
        {
            switch (Format)
            {
                case RampRFormat.RealName: return Column.RealQuotedName;
                case RampRFormat.AliasName: return Column.QuotedSelectorName;
                case RampRFormat.AliasDeclaring: return Column.AliasDeclaring;
                case RampRFormat.Parameter: return "?";
                case RampRFormat.ParameterAliasDeclaring: return $"? AS {Column.QuotedSelectorName}";
            }
            throw new RampException("");
        }
    }

    public class RampRenderTarget : IRampRenderInstruction
    {
        private IRampTarget Target { get; }
        private RampRenderEngine Engine { get; }
        public RampRenderTarget(RampRenderEngine engine, IRampTarget target)
        {
            Engine = engine;
            Target = target;
        }

        public string Render() => Target.AliasDeclaring;
    }

    public class RampRenderFunction : IRampRenderInstruction
    {
        private IRampFunction Function { get; }
        private RampRFormat Format { get; }
        private RampRenderEngine Engine { get; }
        public RampRenderFunction(RampRenderEngine engine, IRampFunction function, RampRFormat format)
        {
            Engine = engine;
            Function = function;
            Format = format;
            if (Format == RampRFormat.Parameter) Engine.Parameters.AddRange(Function.GetParameterValues());
        }

        public string Render()
        {
            switch (Format)
            {
                case RampRFormat.RealName: return Function.RealName;
                case RampRFormat.AliasName: return Function.QuotedSelectorName;
                case RampRFormat.AliasDeclaring: return Function.AliasDeclaring;
                case RampRFormat.Parameter: return "?";
            }
            throw new RampException("");
        }
    }
}
