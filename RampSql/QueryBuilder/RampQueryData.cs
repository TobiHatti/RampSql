using RampSql.Schema;

namespace RampSql.QueryBuilder
{
    public class RampQueryData
    {
        // Main
        public OperationType OperationType { get; set; } = OperationType.Unknown;

        // Group
        public List<IRampColumn> GroupBy { get; } = new List<IRampColumn>();

        // Having

        // Insert
        public List<RampKVPElement> Insert { get; } = new List<RampKVPElement>();

        // InsertResult
        public bool ReturnInsertID { get; set; } = false;

        // Join
        public List<RampJoinElement> Join { get; } = new List<RampJoinElement>();

        // Limit & Offset
        public ulong SelectLimit { get; set; } = ulong.MaxValue;
        public ulong SelectOffset { get; set; } = ulong.MaxValue;

        // Order
        public List<RampOrderElement> Order { get; } = new List<RampOrderElement>();

        // Select
        public List<IRampColumn> SelectColumns { get; } = new List<IRampColumn>();

        // Update
        public List<RampKVPElement> Update { get; } = new List<RampKVPElement>();

        // Where
        public List<WhereElement> Where { get; } = new List<WhereElement>();
    }


    public struct RampKVPElement
    {
        public IRampColumn Column { get; set; }
        public object Value { get; set; }
        public bool Parameterize { get; set; }

        public RampKVPElement(IRampColumn column, object value, bool parameterize)
        {
            Column = column;
            Value = value;
            Parameterize = parameterize;
        }
    }

    public struct RampJoinElement
    {
        public IRampColumn ColumnA { get; set; }
        public IRampColumn ColumnB { get; set; }
        public TableJoinType JoinType { get; set; }
        public string Alias { get; set; }

        public RampJoinElement(IRampColumn columnA, IRampColumn columnB, TableJoinType joinType, string alias)
        {
            ColumnA = columnA;
            ColumnB = columnB;
            JoinType = joinType;
            Alias = alias;
        }
    }

    public struct RampOrderElement
    {
        public IRampColumn Column { get; set; }
        public SortDirection SortDirection { get; set; }

        public RampOrderElement(IRampColumn column, SortDirection sortDirection)
        {
            Column = column;
            SortDirection = sortDirection;
        }
    }

    public struct WhereElement
    {
        public IRampColumn ColumnA { get; set; }
        public IRampColumn ColumnB { get; set; }
        public WhereType WhereType { get; set; }
        public LikeWildcard LikeWildcard { get; set; }
        public bool Parameterize { get; set; }

        public WhereElement(IRampColumn columnA, IRampColumn columnB, WhereType whereType, LikeWildcard likeWildcard, bool parameterize)
        {
            ColumnA = columnA;
            ColumnB = columnB;
            WhereType = whereType;
            LikeWildcard = likeWildcard;
            Parameterize = parameterize;
        }
    }
}
