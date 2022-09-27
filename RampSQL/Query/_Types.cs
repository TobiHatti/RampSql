using RampSQL.Schema;
using System;

namespace RampSQL.Query
{
    public struct RampKeyValuePair<TKey, TValue> : ICloneable
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public RampKeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public object Clone()
        {
            TKey newKey = default(TKey);
            TValue newValue = default(TValue);

            if (typeof(TKey).GetInterface(nameof(ICloneable)) != null) newKey = (TKey)((ICloneable)Key).Clone();
            else newKey = Key;

            if (typeof(TValue).GetInterface(nameof(ICloneable)) != null) newValue = (TValue)((ICloneable)Value).Clone();
            else newValue = Value;

            return new RampKeyValuePair<TKey, TValue>(newKey, newValue);
        }
    }


    public struct RampJoinData : ICloneable
    {
        public RampColumn ExistingTableColumn { get; set; }
        public RampColumn NewTableColumn { get; set; }
        public TableJoinType Type { get; set; }
        public string Alias { get; set; }

        public RampJoinData(RampColumn existingTableColumn, RampColumn newTableColumn, TableJoinType type, string alias)
        {
            ExistingTableColumn = existingTableColumn;
            NewTableColumn = newTableColumn;
            Type = type;
            Alias = alias;
        }

        public object Clone()
        {
            return new RampJoinData()
            {
                ExistingTableColumn = this.ExistingTableColumn,
                NewTableColumn = this.NewTableColumn,
                Type = this.Type,
                Alias = this.Alias
            };
        }
    }

    public struct RampUnionData : ICloneable
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

        public object Clone()
        {
            return new RampUnionData()
            {
                SubQuery = this.SubQuery,
                Alias = this.Alias,
                Parameters = (object[])Parameters.Clone()
            };
        }
    }

    public struct RampParameterType : ICloneable
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

        public object Clone()
        {
            return new RampParameterType()
            {
                Column = this.Column,
                ParamColumn = this.ParamColumn,
                Alias = this.Alias,
                Parameterized = this.Parameterized,
                Value = this.Value
            };
        }
    }

    public class RampWhereData : IWhereQuerySegment
    {
        public RampColumn Column { get; set; }
        public object[] Values { get; set; }
        public WhereType WhereType { get; set; }
        public LikeWildcard LikeWildcard { get; set; }
        public bool Parameterize { get; set; }

        public RampWhereData(RampColumn column, object[] values, WhereType type, LikeWildcard wildcard, bool parameterize)
        {
            Column = column;
            Values = values;
            WhereType = type;
            LikeWildcard = wildcard;
            Parameterize = parameterize;
        }

        public object Clone()
        {
            return new RampWhereData(Column, (object[])Values.Clone(), WhereType, LikeWildcard, Parameterize);
        }
    }

    public class RampConditionConnector : IWhereQuerySegment, IHavingQuerySegment
    {
        public ConditionConnectorType ConnectorType { get; set; } = ConditionConnectorType.None;

        public object Clone()
        {
            return new RampConditionConnector()
            {
                ConnectorType = this.ConnectorType
            };
        }
    }


}
