﻿using RampSQL.Schema;

namespace RampSQL.Query
{
    public class UpdateKeyValueQuery : WhereSelector, IQuerySection
    {
        public UpdateKeyValueQuery(QueryData data) : base(data) { }

        public UpdateKeyValueQuery Value(RampColumn column, SQLFunction function, params object[] parameters) => Value(column, FunctionParser.Parse(function, false, parameters), false);
        public UpdateKeyValueQuery Value(RampColumn column, object value) => Value(column, value, true);
        public UpdateKeyValueQuery Value(RampColumn column, object value, bool parameterize)
        {
            data.ValuePairs.Add(new RampParameterType(column, value, parameterize));
            return this;
        }
    }
}
