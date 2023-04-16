using RampSql.QueryBuilder;
using RampSql.Schema;

namespace RampSql.QuerySections
{
    public class SelectQuery : JoinQuery, IRampQuery
    {
        internal SelectQuery(RampQueryData data) : base(data) { }

        public SelectQuery All() { return this; }
        public SelectQuery Count() { return this; }
        public SelectQuery Count(string alias) { return this; }
        public SelectQuery Count(IRampColumn column) { return this; }
        public SelectQuery Function(IRampFunction function, string alias) { return this; }
        public SelectQuery Column(IRampColumn column) { return this; }
        public SelectQuery Columns(params IRampColumn[] columns) { return this; }
        public SelectQuery Value(object value, string alias) { return this; }
    }
}
