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
        public List<IRampHavingSegment> Having { get; } = new List<IRampHavingSegment>();

        // Insert
        public List<RampKVPElement> Insert { get; } = new List<RampKVPElement>();

        // InsertResult
        public bool ReturnInsertID { get; set; } = false;

        // Join
        public List<RampJoinElement> Join { get; } = new List<RampJoinElement>();

        // Limit & Offset
        public ulong SelectLimit { get; set; } = ulong.MaxValue;
        public ulong SelectOffset { get; set; } = 0;

        // Order
        public List<RampOrderElement> Order { get; } = new List<RampOrderElement>();

        // Select
        public List<IRampValue> SelectValues { get; } = new List<IRampValue>();

        // Update
        public List<RampKVPElement> Update { get; } = new List<RampKVPElement>();

        // Where
        public List<IRampWhereSegment> Where { get; } = new List<IRampWhereSegment>();
    }


    public struct RampKVPElement
    {
        public IRampColumn Column { get; set; }
        public IRampValue Value { get; set; }
        public bool Parameterize { get; set; }

        public RampKVPElement(IRampColumn column, IRampValue value, bool parameterize)
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
        public IRampValue Column { get; set; }
        public SortDirection SortDirection { get; set; }

        public RampOrderElement(IRampValue column, SortDirection sortDirection)
        {
            Column = column;
            SortDirection = sortDirection;
        }
    }

    public struct WhereElement : IRampWhereSegment
    {
        public IRampValue ColumnA { get; set; }
        public IRampValue ColumnB { get; set; }
        public WhereType WhereType { get; set; }
        public LikeWildcard LikeWildcard { get; set; }
        public bool Parameterize { get; set; }

        public WhereElement(IRampValue columnA, IRampValue columnB, WhereType whereType, LikeWildcard likeWildcard, bool parameterize)
        {
            ColumnA = columnA;
            ColumnB = columnB;
            WhereType = whereType;
            LikeWildcard = likeWildcard;
            Parameterize = parameterize;
        }
    }

    public struct RampConditionConnector : IRampWhereSegment, IRampHavingSegment
    {
        public ConditionConnectorType ConnectorType { get; set; } = ConditionConnectorType.None;
        public RampConditionConnector(ConditionConnectorType connectorType)
        {
            ConnectorType = connectorType;
        }
    }
}
