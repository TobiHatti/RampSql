using RampSQL.Schema;

namespace RampSQL.Query
{
    internal struct RampJoinData
    {
        public RampColumn ExistingTableColumn { get; set; }
        public RampColumn NewTableColumn { get; set; }
        public TableJoinType Type { get; set; }

        public RampJoinData(RampColumn existingTableColumn, RampColumn newTableColumn, TableJoinType type)
        {
            ExistingTableColumn = existingTableColumn;
            NewTableColumn = newTableColumn;
            Type = type;
        }
    }

    internal struct RampWhereData
    {
        public RampColumn Column { get; set; }
        public object Value { get; set; }
        public WhereType WhereType { get; set; }
        public LikeWildcard LikeWildcard { get; set; }

        public RampWhereData(RampColumn column, object value, WhereType type, LikeWildcard wildcard)
        {
            Column = column;
            Value = value;
            WhereType = type;
            LikeWildcard = wildcard;
        }
    }

}
