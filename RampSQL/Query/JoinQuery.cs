using RampSQL.Schema;

namespace RampSQL.Query
{
    public class JoinQuery : WhereExtSelector
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
    }
}