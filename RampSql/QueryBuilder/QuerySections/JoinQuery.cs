﻿using RampSql.Schema;

namespace RampSql.QueryBuilder
{
    public class JoinQuery : WhereExtSelector, IRampQuery
    {
        internal JoinQuery(RampQueryData data) : base(data) { }

        public JoinQuery LeftJoin(IRampColumn existingTableColumn, IRampColumn newTableColumn) => Join(existingTableColumn, newTableColumn, TableJoinType.Left, null);
        public JoinQuery LeftJoin(IRampColumn existingTableColumn, IRampColumn newTableColumn, string alias) => Join(existingTableColumn, newTableColumn, TableJoinType.Left, alias);
        public JoinQuery RightJoin(IRampColumn existingTableColumn, IRampColumn newTableColumn) => Join(existingTableColumn, newTableColumn, TableJoinType.Right, null);
        public JoinQuery RightJoin(IRampColumn existingTableColumn, IRampColumn newTableColumn, string alias) => Join(existingTableColumn, newTableColumn, TableJoinType.Right, alias);
        public JoinQuery InnerJoin(IRampColumn existingTableColumn, IRampColumn newTableColumn) => Join(existingTableColumn, newTableColumn, TableJoinType.Inner, null);
        public JoinQuery InnerJoin(IRampColumn existingTableColumn, IRampColumn newTableColumn, string alias) => Join(existingTableColumn, newTableColumn, TableJoinType.Inner, alias);
        public JoinQuery FullOuterJoin(IRampColumn existingTableColumn, IRampColumn newTableColumn) => Join(existingTableColumn, newTableColumn, TableJoinType.FullOuter, null);
        public JoinQuery FullOuterJoin(IRampColumn existingTableColumn, IRampColumn newTableColumn, string alias) => Join(existingTableColumn, newTableColumn, TableJoinType.FullOuter, alias);
        public JoinQuery Join(IRampColumn existingTableColumn, IRampColumn newTableColumn, TableJoinType join) => Join(existingTableColumn, newTableColumn, join, null);
        public JoinQuery Join(IRampColumn existingTableColumn, IRampColumn newTableColumn, TableJoinType join, string alias)
        {
            data.Join.Add(new RampJoinElement(data, existingTableColumn, newTableColumn, join, alias));
            return this;
        }
    }
}