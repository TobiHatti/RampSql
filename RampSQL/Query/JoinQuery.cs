using RampSQL.Schema;

namespace RampSQL.Query
{
    public class JoinQuery : WhereExtSelector, IQuerySection
    {
        public JoinQuery(QueryData data) : base(data) { }
        public JoinQuery LeftJoin(RampColumn existingTableColumn, RampColumn newTableColumn) => Join(existingTableColumn, newTableColumn, TableJoinType.Left);
        public JoinQuery RightJoin(RampColumn existingTableColumn, RampColumn newTableColumn) => Join(existingTableColumn, newTableColumn, TableJoinType.Right);
        public JoinQuery InnerJoin(RampColumn existingTableColumn, RampColumn newTableColumn) => Join(existingTableColumn, newTableColumn, TableJoinType.Inner);
        public JoinQuery Join(RampColumn existingTableColumn, RampColumn newTableColumn, TableJoinType join)
        {
            data.Joins.Add(new RampJoinData(existingTableColumn, newTableColumn, join));
            return this;
        }
    }
}