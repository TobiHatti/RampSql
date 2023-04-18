﻿using RampSql.QueryBuilder.QueryBuilder;
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
        Parameter
    }

    public class RampRenderEngine
    {
        private List<IRampRenderInstruction> instructions = new List<IRampRenderInstruction>();
        public List<object> Parameters { get; } = new List<object>();

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
            if (value is IRampQuery) return Instruction("(").Query((IRampQuery)value, "").Instruction(")");

            instructions.Add(new RampRenderValue(value, format));
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
            instructions.Add(new RampRenderColumn(column, format));
            return this;
        }

        public RampRenderEngine Table(RampTableData table, RampRFormat format, string alias = null)
        {
            instructions.Add(new RampRenderTable(table, format));
            table.Alias = alias;
            return this;
        }

        public RampRenderEngine Target(IRampTarget target, string alias = null)
        {
            if (target is IRampQuery) return Instruction("(").Query((IRampQuery)target).Instruction(") AS").Raw(((IRampQuery)target).GetData().QueryAlias);
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
                sb.Append($"{instr.Render(this)} ");
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

        public string Render(RampRenderEngine engine) => Value;
    }

    public class RampRenderRaw : IRampRenderInstruction
    {
        private string Value { get; }
        public RampRenderRaw(string value)
        {
            Value = value;
        }

        public string Render(RampRenderEngine engine) => Value;
    }

    public class RampRenderTable : IRampRenderInstruction
    {
        private RampTableData Table { get; }
        private RampRFormat Format { get; }
        public RampRenderTable(RampTableData table, RampRFormat format)
        {
            Table = table;
            Format = format;
        }

        public string Render(RampRenderEngine engine)
        {
            switch (Format)
            {
                case RampRFormat.RealName: return Table.QuotedTableName;
                case RampRFormat.AliasName: return Table.QuotedSelectorName;
                case RampRFormat.AliasDeclaring: return Table.AliasDeclaring;
                case RampRFormat.Parameter:
                    engine.Parameters.AddRange(Table.GetParameterValues());
                    return "?";
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

        public string Render(RampRenderEngine engine)
        {
            switch (Format)
            {
                case RampRFormat.RealName: return Value.RealName;
                case RampRFormat.AliasName: return Value.QuotedSelectorName;
                case RampRFormat.AliasDeclaring: return Value.AliasDeclaring;
                case RampRFormat.Parameter:
                    engine.Parameters.AddRange(Value.GetParameterValues());
                    return "?";
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
        public RampRenderColumn(IRampColumn column, RampRFormat format)
        {
            Column = column;
            Format = format;
        }

        public string Render(RampRenderEngine engine)
        {
            switch (Format)
            {
                case RampRFormat.RealName: return Column.RealQuotedName;
                case RampRFormat.AliasName: return Column.QuotedSelectorName;
                case RampRFormat.AliasDeclaring: return Column.AliasDeclaring;
                case RampRFormat.Parameter:
                    engine.Parameters.AddRange(Column.GetParameterValues());
                    return "?";
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

        public string Render(RampRenderEngine engine) => Target.AliasDeclaring;
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

        public string Render(RampRenderEngine engine)
        {
            switch (Format)
            {
                case RampRFormat.RealName: return Function.DeclaringStatement;
                case RampRFormat.AliasName: return Function.QuotedSelectorName;
                case RampRFormat.AliasDeclaring: return Function.AliasDeclaring;
                case RampRFormat.Parameter:
                    engine.Parameters.AddRange(Function.GetParameterValues());
                    return "?";
            }
            throw new RampException("");
        }
    }
}
