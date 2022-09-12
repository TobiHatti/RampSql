using RampSQL.Schema;

namespace RampSQL.Query
{
    public struct RampJoinData
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

    public struct RampUnionData
    {
        public string SubQuery;
        public string Alias;
        public object[] Parameters;

        public RampUnionData(string subquery, string alias, object[] parameters)
        {
            SubQuery = subquery;
            Alias = alias;
            Parameters = parameters;
        }
    }

    public class RampWhereData : IWhereQuerySegment
    {
        public RampColumn Column { get; set; }
        public object[] Values { get; set; }
        public WhereType WhereType { get; set; }
        public LikeWildcard LikeWildcard { get; set; }

        public RampWhereData(RampColumn column, object[] values, WhereType type, LikeWildcard wildcard)
        {
            Column = column;
            Values = values;
            WhereType = type;
            LikeWildcard = wildcard;
        }
    }

    public class RampConditionConnector : IWhereQuerySegment, IHavingQuerySegment
    {
        public ConditionConnectorType ConnectorType { get; set; } = ConditionConnectorType.None;
    }


}
