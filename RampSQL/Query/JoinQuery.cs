using RampSQL.Schema;
using System;

namespace RampSQL.Query
{
    public class JoinQuery : WhereExtSelector, IQuerySection
    {
        public JoinQuery(QueryData data) : base(data) { }
        public JoinQuery LeftJoin(RampColumn existingTableColumn, RampColumn newTableColumn) => Join(existingTableColumn, newTableColumn, TableJoinType.Left);
        public JoinQuery LeftJoin(RampColumn existingTableColumn, RampColumn newTableColumn, string alias) => Join(existingTableColumn, newTableColumn, TableJoinType.Left, alias);
        public JoinQuery RightJoin(RampColumn existingTableColumn, RampColumn newTableColumn) => Join(existingTableColumn, newTableColumn, TableJoinType.Right);
        public JoinQuery RightJoin(RampColumn existingTableColumn, RampColumn newTableColumn, string alias) => Join(existingTableColumn, newTableColumn, TableJoinType.Right, alias);
        public JoinQuery InnerJoin(RampColumn existingTableColumn, RampColumn newTableColumn) => Join(existingTableColumn, newTableColumn, TableJoinType.Inner);
        public JoinQuery InnerJoin(RampColumn existingTableColumn, RampColumn newTableColumn, string alias) => Join(existingTableColumn, newTableColumn, TableJoinType.Inner, alias);
        public JoinQuery FullOuterJoin(RampColumn existingTableColumn, RampColumn newTableColumn) => Join(existingTableColumn, newTableColumn, TableJoinType.FullOuter);
        public JoinQuery FullOuterJoin(RampColumn existingTableColumn, RampColumn newTableColumn, string alias) => Join(existingTableColumn, newTableColumn, TableJoinType.FullOuter, alias);
        public JoinQuery Join(RampColumn existingTableColumn, RampColumn newTableColumn, TableJoinType join) => Join(existingTableColumn, newTableColumn, join, null);
        public JoinQuery Join(RampColumn existingTableColumn, RampColumn newTableColumn, TableJoinType join, string alias)
        {
            data.Joins.Add(new RampJoinData(existingTableColumn, newTableColumn, join, alias));
            return this;
        }


        public JoinQuery JoinFirst(RampColumn existingTableColumn, RampColumn newTableColumn, TableJoinType join)
        {
            return JoinFunction(existingTableColumn, newTableColumn, null, join, SqlFunction.MAX);
        }

        private JoinQuery JoinFunction(RampColumn existingTableColumn, RampColumn newTableColumn, string alias, TableJoinType join, SqlFunction function)
        {
            if (string.IsNullOrEmpty(alias)) alias = Guid.NewGuid().ToString().Replace("-", "");



            this.Join(existingTableColumn, newTableColumn, join, alias);





            return this;
        }
    }
}