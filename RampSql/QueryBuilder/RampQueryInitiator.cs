using RampSql.QuerySections;
using RampSql.Schema;

namespace RampSql.QueryBuilder
{
    public class RampQueryInitiator : IRampQueryInitiator
    {
        public SelectQuery Select(IRampColumn column) { return null; }
        public SelectQuery SelectFrom(IRampTable table) { return null; }
        public SelectQuery SelectFrom(IRampQuery subquery) { return null; }
        public SelectQuery SelectAllFrom(IRampTable table) { return null; }

        public SelectQuery Count(IRampTable table) { return null; }
        public SelectQuery Count(IRampColumn column) { return null; }

        public JoinQuery SearchFrom(IRampTable table) { return null; }

        public InsertQuery InsertInto(IRampTable table) { return null; }
        public UpdateQuery Update(IRampTable table) { return null; }
        public InsertQuery Upsert(IRampTable table) { return null; }
        public WhereSelector DeleteFrom(IRampTable table) { return this; }

        public IRampQuery Union(UnionType unionType, params IRampQuery[] queries) { return null; }

        public object Free() { return null; }
    }
}
