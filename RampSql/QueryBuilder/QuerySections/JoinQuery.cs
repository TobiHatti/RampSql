using RampSql.QueryBuilder;
using RampSql.QueryConnectors;
using RampSql.Schema;

namespace RampSql.QuerySections
{
    public class JoinQuery : WhereExtSelector, IRampQuery
    {
        internal JoinQuery(RampQueryData data) : base(data) { }

        public JoinQuery LeftJoin(IRampColumn existingTableColumn, IRampColumn newTableColumn) { return this; }
        public JoinQuery LeftJoin(IRampColumn existingTableColumn, IRampColumn newTableColumn, string alias) { return this; }
        public JoinQuery RightJoin(IRampColumn existingTableColumn, IRampColumn newTableColumn) { return this; }
        public JoinQuery RightJoin(IRampColumn existingTableColumn, IRampColumn newTableColumn, string alias) { return this; }
        public JoinQuery InnerJoin(IRampColumn existingTableColumn, IRampColumn newTableColumn) { return this; }
        public JoinQuery InnerJoin(IRampColumn existingTableColumn, IRampColumn newTableColumn, string alias) { return this; }
        public JoinQuery FullOuterJoin(IRampColumn existingTableColumn, IRampColumn newTableColumn) { return this; }
        public JoinQuery FullOuterJoin(IRampColumn existingTableColumn, IRampColumn newTableColumn, string alias) { return this; }
        public JoinQuery Join(IRampColumn existingTableColumn, IRampColumn newTableColumn, TableJoinType join) { return this; }
        public JoinQuery Join(IRampColumn existingTableColumn, IRampColumn newTableColumn, TableJoinType join, string alias) { return this; }
    }
}
