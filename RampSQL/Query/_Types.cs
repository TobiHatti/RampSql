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
        public string SubQuery { get; set; }
        public string Alias { get; set; }
        public object[] Parameters { get; set; }

        public RampUnionData(string subquery, string alias, object[] parameters)
        {
            SubQuery = subquery;
            Alias = alias;
            Parameters = parameters;
        }
    }

    public struct RampParameterType
    {
        public RampColumn Column { get; set; }
        public object ParamColumn { get; set; }
        public string Alias { get; set; }
        public bool Parameterized { get; set; }
        public object Value { get; set; }

        // for selectColumns
        public RampParameterType(RampColumn column, string alias)
        {
            Column = column;
            Alias = alias;

            ParamColumn = null;
            Parameterized = true;
            Value = null;
        }

        // for selectValues
        public RampParameterType(object column, string alias, bool parameterize = true)
        {
            ParamColumn = column;
            Alias = alias;
            Parameterized = parameterize;

            Column = null;
            Value = null;
        }

        // For KeyValuePairs
        public RampParameterType(RampColumn column, object value, bool parameterize = true)
        {
            Column = column;
            Value = value;
            Parameterized = parameterize;

            ParamColumn = null;
            Alias = null;
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
