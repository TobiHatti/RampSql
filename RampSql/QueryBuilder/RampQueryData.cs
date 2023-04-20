using RampSql.Schema;

namespace RampSql.QueryBuilder
{
    public struct RampQueryData
    {
        public RampQueryData() { }

        // Main
        public IRampSchema Schema { get; set; } = null;
        private string alias = string.Empty;
        public string QueryAlias
        {
            get => alias;
            set
            {
                if (!string.IsNullOrEmpty(value)) alias = value;
            }
        }
        public OperationType OperationType { get; set; } = OperationType.Unknown;
        public IRampTarget Target { get; set; } = null;


        // Group
        public List<RampGroupElement> GroupBy { get; } = new List<RampGroupElement>();

        // Having
        public List<IRampHavingSegment> Having { get; } = new List<IRampHavingSegment>();

        // Insert/Update
        public List<RampKVPElement> KVPairs { get; } = new List<RampKVPElement>();

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
        public bool SelectAll { get; set; } = false;

        // Where
        public List<IRampWhereSegment> Where { get; } = new List<IRampWhereSegment>();

        // Union
        public IRampQuery[] Union { get; set; } = null;
        public UnionType UnionType { get; set; } = UnionType.Union;


        // Background
        public List<IRampColumn> columnCollection { get; } = new List<IRampColumn>();
    }


    public struct RampGroupElement
    {
        public IRampColumn Column { get; set; }

        public RampGroupElement(RampQueryData data, IRampColumn column)
        {
            Column = column;
            data.columnCollection.Add(column);
        }
    }

    public struct RampKVPElement
    {
        public IRampColumn Column { get; set; }
        public IRampValue Value { get; set; }
        public bool Parameterize { get; set; }

        public RampKVPElement(RampQueryData data, IRampColumn column, IRampValue value, bool parameterize)
        {
            Column = column;
            Value = value;
            Parameterize = parameterize;

            data.columnCollection.Add(column);
            if (value is IRampColumn) data.columnCollection.Add((IRampColumn)value);
        }
    }

    public struct RampFunctionElement
    {
        public string Function { get; set; }
        public IRampValue[] Values { get; set; }

        public RampFunctionElement(RampQueryData data, string function, IRampValue[] values)
        {
            Function = function;
            Values = values;

            foreach (IRampValue value in Values) if (value is IRampColumn) data.columnCollection.Add((IRampColumn)value);
        }
    }

    public struct RampJoinElement
    {
        public IRampColumn ExistingColumn { get; set; }
        public IRampColumn NewColumn { get; set; }
        public TableJoinType JoinType { get; set; }
        public string Alias { get; set; }

        public RampJoinElement(RampQueryData data, IRampColumn existingTableColumn, IRampColumn newTableColumn, TableJoinType joinType, string alias)
        {
            ExistingColumn = existingTableColumn;
            NewColumn = newTableColumn;
            JoinType = joinType;
            Alias = alias;

            data.columnCollection.Add(existingTableColumn);
            data.columnCollection.Add(newTableColumn);
        }
    }

    public struct RampOrderElement
    {
        public IRampValue Column { get; set; }
        public SortDirection SortDirection { get; set; }

        public RampOrderElement(RampQueryData data, IRampValue column, SortDirection sortDirection)
        {
            Column = column;
            SortDirection = sortDirection;

            if (column is IRampColumn) data.columnCollection.Add((IRampColumn)column);
        }
    }

    public struct RampWhereElement : IRampWhereSegment
    {
        public IRampValue ColumnA { get; set; }
        public IRampValue ColumnB { get; set; }
        public WhereType WhereType { get; set; }
        public LikeWildcard LikeWildcard { get; set; }
        public bool Parameterize { get; set; }

        public RampWhereElement(RampQueryData data, IRampValue columnA, IRampValue columnB, WhereType whereType, LikeWildcard likeWildcard, bool parameterize)
        {
            ColumnA = columnA;
            ColumnB = columnB;
            WhereType = whereType;
            LikeWildcard = likeWildcard;
            Parameterize = parameterize;

            if (columnA is IRampColumn) data.columnCollection.Add((IRampColumn)columnA);
            if (columnB is IRampColumn) data.columnCollection.Add((IRampColumn)columnB);
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
