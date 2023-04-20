namespace RampSql.QueryBuilder
{
    internal class RampFunction : IRampFunction
    {
        private RampFunctionElement function;
        private string alias { get; set; }

        public RampFunction(RampFunctionElement function)
        {
            this.function = function;
        }

        public string RealName => string.Empty;

        public string QuotedSelectorName => string.Empty;

        public string AliasDeclaring => string.Empty;

        public bool HasAlias => !string.IsNullOrEmpty(alias);

        public object[] GetParameterValues() => GetRenderer().Parameters.ToArray();

        public void AsAlias(string alias) => As(alias);
        public void As(string alias) => this.alias = alias;

        public RampRenderEngine GetRenderer()
        {
            RampRenderEngine engine = new RampRenderEngine();

            engine.Instruction(function.Function).Instruction("(");
            bool first = true;
            foreach (IRampValue param in function.Values)
            {
                if (!first) engine.Instruction(",");
                if (param is IRampConstant && (param as IRampConstant).RealName != "*") engine.Value(param, RampRFormat.Parameter);
                else engine.Value(param, RampRFormat.AliasName);
                first = false;
            }
            engine.Instruction(")");
            if (HasAlias) engine.Instruction("AS").Raw(alias);
            return engine;
        }
    }
}
