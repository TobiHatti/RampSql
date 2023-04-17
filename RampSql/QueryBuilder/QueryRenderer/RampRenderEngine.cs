using RampSql.Schema;
using System.Runtime.InteropServices;
using System.Text;

namespace RampSql.QueryBuilder.QueryRenderer
{
    public enum RampRFormat
    {
        RealName,
        AliasName,
        AliasDeclaring
    }

    public class RampRenderEngine
    {
        private List<IRampRenderInstruction> instructions = new List<IRampRenderInstruction>();


        public RampRenderEngine Instruction(string value)
        {
            instructions.Add(new RampRenderConstant(value));
            return this;
        }

        public RampRenderEngine Raw(string value)
        {
            instructions.Add(new RampRenderRaw(value));
            return this;
        }

        public RampRenderEngine Value(IRampValue value, RampRFormat format)
        {
            instructions.Add(new RampRenderValue(value, format));
            return this;
        }

        //public RampRenderEngine Query(IRampQuery query, RampRFormat format, string alias = null)
        //{
        //    instructions.Add(new RampRenderQuery(query, format));
        //    if (string.IsNullOrEmpty(alias))
        //    {
        //        Instruction("AS").Raw(alias);
        //    }
        //    return this;
        //}

        public RampRenderEngine Column(IRampColumn column, RampRFormat format, string alias = null)
        {
            instructions.Add(new RampRenderColumn(column, format));
            return this;
        }

        public RampRenderEngine Table(RampTable table, RampRFormat format, string alias = null)
        {
            instructions.Add(new RampRenderTable(table, format));
            table.Alias = alias;
            return this;
        }

        public RampRenderEngine Target(IRampTarget target, string alias = null)
        {
            instructions.Add(new RampRenderTarget(target));
            target.AsAlias(alias);
            return this;
        }


        public RampRenderEngine Function(IRampFunction function, RampRFormat format, string alias = null)
        {
            instructions.Add(new RampRenderFunction(function, format));
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
        public RampRenderConstant(string value)
        {
            Value = value;
        }

        public string Render() => Value;
    }

    public class RampRenderRaw : IRampRenderInstruction
    {
        private string Value { get; }
        public RampRenderRaw(string value)
        {
            Value = value;
        }

        public string Render() => Value;
    }

    public class RampRenderTable : IRampRenderInstruction
    {
        private RampTable Table { get; }
        private RampRFormat Format { get; }
        public RampRenderTable(RampTable table, RampRFormat format)
        {
            Table = table;
            Format = format;
        }

        public string Render()
        {
            switch (Format)
            {
                case RampRFormat.RealName: return Table.QuotedTableName;
                case RampRFormat.AliasName: return Table.QuotedSelectorName;
                case RampRFormat.AliasDeclaring: return Table.AliasDeclaring;
            }
            throw new RampException("");
        }
    }

    public class RampRenderValue : IRampRenderInstruction
    {
        private IRampValue Value { get; }
        private RampRFormat Format { get; }
        public RampRenderValue(IRampValue value, RampRFormat format)
        {
            Value = value;
            Format = format;
        }

        public string Render()
        {
            switch (Format)
            {
                case RampRFormat.RealName: return Value.RealName;
                case RampRFormat.AliasName: return Value.QuotedSelectorName;
                case RampRFormat.AliasDeclaring: return Value.AliasDeclaring;
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

    //    public string Render() => "";
    //}

    public class RampRenderColumn : IRampRenderInstruction
    {
        private IRampColumn Column { get; }
        private RampRFormat Format { get; }
        public RampRenderColumn(IRampColumn column, RampRFormat format)
        {
            Column = column;
            Format = format;
        }

        public string Render()
        {
            switch (Format)
            {
                case RampRFormat.RealName: return Column.RealQuotedName;
                case RampRFormat.AliasName: return Column.QuotedSelectorName;
                case RampRFormat.AliasDeclaring: return Column.AliasDeclaring;
            }
            throw new RampException("");
        }
    }

    public class RampRenderTarget : IRampRenderInstruction
    {
        private IRampTarget Target { get; }
        public RampRenderTarget(IRampTarget target)
        {
            Target = target;
        }

        public string Render() => Target.AliasDeclaring;
    }

    public class RampRenderFunction : IRampRenderInstruction
    {
        private IRampFunction Function { get; }
        private RampRFormat Format { get; }
        public RampRenderFunction(IRampFunction function, RampRFormat format)
        {
            Function = function;
            Format = format;
        }

        public string Render()
        {
            switch (Format)
            {
                case RampRFormat.RealName: return Function.DeclaringStatement;
                case RampRFormat.AliasName: return Function.QuotedSelectorName;
                case RampRFormat.AliasDeclaring: return Function.AliasDeclaring;
            }
            throw new RampException("");
        }
    }
}
