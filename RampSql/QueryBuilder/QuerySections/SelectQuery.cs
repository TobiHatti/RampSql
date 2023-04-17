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
        public SelectQuery Count() => ExecSelect(new RampConstant("COUNT(*)", null), null);    // TODO: Replace with funcition call
        public SelectQuery Count(string alias) => ExecSelect(new RampConstant("COUNT(*)", alias), null);  // TODO: Replace with funcition call
        public SelectQuery Count(IRampColumn column) => ExecSelect(new RampConstant("COUNT(*)", null), null);  // TODO: Replace with funcition call
        public SelectQuery Count(IRampColumn column, string alias) => ExecSelect(new RampConstant("COUNT(*)", alias), null);  // TODO: Replace with funcition call
        public SelectQuery Function(IRampFunction function) => ExecSelect(function, null);   // TODO: Function alias seperate param?
        public SelectQuery Function(IRampFunction function, string alias) => ExecSelect(function, alias);   // TODO: Function alias seperate param?
        public SelectQuery Column(IRampColumn column) => ExecSelect(column, null);
        public SelectQuery Column(IRampColumn column, string alias) => ExecSelect(column, alias);
        public SelectQuery Columns(params IRampColumn[] columns) => Values(columns);
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
    }
}
