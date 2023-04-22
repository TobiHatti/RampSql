using RampSql.Schema;

namespace RampSql.QueryBuilder
{
    public class SelectQuery : JoinQuery, IRampQuery
    {
        internal SelectQuery(RampQueryData data) : base(data) { }

        public SelectQuery All()
        {
            data.SelectAll = true;
            return ExecSelect(new RampConstant("*", null), null);
        }

        public SelectQuery Count() => ExecSelect(new RampFunction(new RampFunctionElement(data, "COUNT", new IRampValue[] { new RampConstant("*", null) })), null);
        public SelectQuery Count(string alias) => ExecSelect(new RampFunction(new RampFunctionElement(data, "COUNT", new IRampValue[] { new RampConstant("*", alias) })), null);
        public SelectQuery Count(IRampColumn column) => ExecSelect(new RampFunction(new RampFunctionElement(data, "COUNT", new IRampValue[] { column })), null);
        public SelectQuery Count(IRampColumn column, string alias) => ExecSelect(new RampFunction(new RampFunctionElement(data, "COUNT", new IRampValue[] { column })), alias);

        public SelectQuery Func(SqlFunction function, params IRampValue[] parameters) => ExecSelect(new RampFunction(new RampFunctionElement(data, function.ToString(), parameters)), null);
        public SelectQuery Func(MySqlFunction function, params IRampValue[] parameters) => ExecSelect(new RampFunction(new RampFunctionElement(data, function.ToString(), parameters)), null);
        public SelectQuery Func(string function, params IRampValue[] parameters) => ExecSelect(new RampFunction(new RampFunctionElement(data, function, parameters)), null);
        public SelectQuery FuncAs(SqlFunction function, string alias, params IRampValue[] parameters) => ExecSelect(new RampFunction(new RampFunctionElement(data, function.ToString(), parameters)), alias);
        public SelectQuery FuncAs(MySqlFunction function, string alias, params IRampValue[] parameters) => ExecSelect(new RampFunction(new RampFunctionElement(data, function.ToString(), parameters)), alias);
        public SelectQuery FuncAs(string function, string alias, params IRampValue[] parameters) => ExecSelect(new RampFunction(new RampFunctionElement(data, function, parameters)), alias);

        public SelectQuery Column(IRampColumn column) => ExecSelect(column, null);
        public SelectQuery Column(IRampColumn column, string alias) => ExecSelect(column, alias);
        public SelectQuery Columns(params IRampColumn[] columns) => Values(columns);
        public SelectQuery Value(object value) => ExecSelect(new RampConstant(value, null), null);
        public SelectQuery Value(object value, string alias) => ExecSelect(new RampConstant(value, alias), null);
        public SelectQuery Value(IRampValue value) => ExecSelect(value, null);

        private SelectQuery ExecSelect(IRampValue value, string alias)
        {
            data.SelectValues.Add(value);
            value.AsAlias(alias);
            return this;
        }

        public SelectQuery Values(params IRampValue[] values)
        {
            data.SelectValues.AddRange(values);
            return this;
        }

        public SelectQuery Query<Schema>(Func<Schema, RampQueryInitiator<Schema>, IRampQuery> query, string alias) where Schema : RampSchema<Schema>
        {
            RampQueryInitiator<Schema> initiator = new RampQueryInitiator<Schema>();
            Schema subSchema = RampSchemaData.CreateSub((Schema)data.Schema);
            subSchema.SetParentSchema((Schema)data.Schema);
            subSchema.Alias = alias;
            RampSchemaData.SwitchBranch(subSchema).Alias = alias;
            (data.Schema as Schema).RegisterSubSchema(subSchema);
            initiator.SetSchema(subSchema);
            return ExecSelect(query(subSchema, initiator), alias);
        }
    }
}
