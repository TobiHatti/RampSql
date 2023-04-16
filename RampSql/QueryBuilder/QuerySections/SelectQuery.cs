using RampSql.QueryBuilder;
using RampSql.Schema;

namespace RampSql.QuerySections
{
    public class SelectQuery : JoinQuery, IRampQuery
    {
        internal SelectQuery(RampQueryData data) : base(data) { }

        public SelectQuery All() => Value(new RampConstant("*", null));
        public SelectQuery Count() => Value(new RampConstant("COUNT(*)", null));    // TODO: Replace with funcition call
        public SelectQuery Count(string alias) => Value(new RampConstant("COUNT(*)", alias));  // TODO: Replace with funcition call
        public SelectQuery Count(IRampColumn column) => Value(new RampConstant("COUNT(*)", null));  // TODO: Replace with funcition call
        public SelectQuery Function(IRampFunction function, string alias) => Value(function);   // TODO: Function alias seperate param?
        public SelectQuery Column(IRampColumn column) => Value(column);
        public SelectQuery Columns(params IRampColumn[] columns) => Values(columns);
        public SelectQuery Value(object value, string alias) => Value(new RampConstant(value, alias));

        public SelectQuery Value(IRampValue value)
        {
            data.SelectValues.Add(value);
            return this;
        }

        public SelectQuery Values(params IRampValue[] values)
        {
            data.SelectValues.AddRange(values);
            return this;
        }
    }
}
