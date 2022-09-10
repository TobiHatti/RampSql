using RampSQL.Schema;

namespace RampSQL.Query
{
    public class JoinQuery : WhereOrderSelector
    {
        public JoinQuery Join(RampColumn existingTableColumn, RampColumn newTableColumn, TableJoinType join)
        {
            return this;
        }

        public JoinQuery LeftJoin(RampColumn existingTableColumn, RampColumn newTableColumn)
        {
            return this;
        }

        public JoinQuery RightJoin(RampColumn existingTableColumn, RampColumn newTableColumn)
        {
            return this;
        }

        public JoinQuery InnerJoin(RampColumn existingTableColumn, RampColumn newTableColumn)
        {
            return this;
        }

        public JoinQuery FullOuterJoin(RampColumn existingTableColumn, RampColumn newTableColumn)
        {
            return this;
        }


        // Bypass where/having clause

        public OrderQuery OrderBy(RampColumn column, SortDirection direction)
        {
            return null;
        }

        public LimitQuery Limit(ulong limitCount)
        {
            return null;
        }

        public LimitQuery Limit(ulong limitCount, int offset)
        {
            return null;
        }

        public LimitQuery Shift(int offset)
        {
            return null;
        }
    }
}