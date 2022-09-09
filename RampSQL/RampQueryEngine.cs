using System.Collections.Generic;
using System.Text;

namespace RampSQL
{
    public interface IRampQuerySection
    {

    }

    public enum TableJoinType
    {
        Inner,
        Left,
        Right
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }

    internal enum OperationType
    {
        Unknown,
        Select,
        Insert,
        Update,
        Delete
    }

    internal enum WhereConnectorType
    {
        And,
        Or,
        None
    }

    internal enum WhereType
    {
        Is,
        IsNot,
        IsLike,
        IsNotLike
    }

    public enum LikeWildcard
    {
        Unspecified,
        MatchStart,
        MatchEnd,
        MatchBoth,
        MatchAny
    }

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

    public class RampOmniQuery : IRampQuerySection
    {
        protected IRampQuerySection parent;

        public RampOmniQuery(IRampQuerySection subQuery)
        {
            parent = subQuery;
        }
    }

    public class RampResultSelectorQuery : RampOmniQuery
    {
        public RampResultSelectorQuery(IRampQuerySection parent = null) : base(parent) { }

        public RampResultQuery Result
        {
            get => new RampResultQuery(this);
        }
    }

    public class RampOrderSelectorQuery : RampResultSelectorQuery
    {
        public RampOrderSelectorQuery(IRampQuerySection parent = null) : base(parent) { }

        public RampOrderQuery Order
        {
            get => new RampOrderQuery(this);
        }
    }

    public class RampWhereSelectorQuery : RampOrderSelectorQuery
    {
        public RampWhereSelectorQuery(IRampQuerySection parent = null) : base(parent) { }

        public RampWhereQuery Where
        {
            get => new RampWhereQuery(this, true);
        }
    }


    public class RampResultQuery : RampResultSelectorQuery
    {
        private ulong limit;
        private int offset;
        public RampResultQuery(IRampQuerySection parent = null) : base(parent) { }

        public RampResultQuery Limit(ulong limitCount)
        {
            this.limit = limitCount;
            return this;
        }

        public RampResultQuery Limit(ulong limitCount, int offset)
        {
            this.limit = limitCount;
            this.offset = offset;
            return this;
        }

        public RampResultQuery Shift(int offset)
        {
            this.offset = offset;
            return this;
        }

        public override string ToString()
        {
            StringBuilder query = new StringBuilder();
            if (limit != 0 || offset != 0)
            {
                // Max bigint for mysql. ugly but works
                if (limit == 0) limit = 18446744073709551615;

                query.Append($"LIMIT {offset},{limit}");
            }

            return $"{parent} {query}";
        }
    }


    public class RampOrderQuery : RampOrderSelectorQuery
    {
        private List<KeyValuePair<RampColumn, SortDirection>> sortOrder = new List<KeyValuePair<RampColumn, SortDirection>>();

        public RampOrderQuery(IRampQuerySection parent = null) : base(parent) { }

        public RampOrderQuery By(RampColumn column, SortDirection direction)
        {
            sortOrder.Add(new KeyValuePair<RampColumn, SortDirection>(column, direction));
            return this;
        }

        public override string ToString()
        {
            StringBuilder query = new StringBuilder();

            if (sortOrder.Count > 0) query.Append("ORDER BY ");
            bool first = true;
            foreach (KeyValuePair<RampColumn, SortDirection> order in sortOrder)
            {
                if (!first) query.Append(", ");
                query.Append($"{order.Key} ");
                switch (order.Value)
                {
                    case SortDirection.Ascending:
                        query.Append("ASC ");
                        break;
                    case SortDirection.Descending:
                        query.Append("DESC ");
                        break;
                }
                first = false;
            }

            return $"{parent} {query}";
        }
    }

    public class RampQueryEngine : RampWhereSelectorQuery
    {
        // General
        private OperationType operationType = OperationType.Unknown;
        private RampTable targetTable;

        // Insert/Update specific
        private List<KeyValuePair<RampColumn, object>> keyValPairs = new List<KeyValuePair<RampColumn, object>>();

        public RampQueryEngine(IRampQuerySection parent = null) : base(parent) { }

        public RampQueryEngine InsertInto(RampTable table)
        {
            operationType = OperationType.Insert;
            targetTable = table;
            return this;
        }

        public RampQueryEngine Update(RampTable table)
        {
            operationType = OperationType.Update;
            targetTable = table;
            return this;
        }

        public RampQueryEngine DeleteFrom(RampTable table)
        {
            operationType = OperationType.Delete;
            targetTable = table;
            return this;
        }

        public RampSubQuery SelectFrom(RampTable table)
        {
            operationType = OperationType.Select;
            targetTable = table;
            return new RampSubQuery(this, table);
        }

        public RampQueryEngine ValuePair(RampColumn column, object value)
        {
            keyValPairs.Add(new KeyValuePair<RampColumn, object>(column, value));
            return this;
        }

        public RampQueryEngine GetLastInsertID()
        {
            return this;
        }

        private string CreateUpdateSubquery()
        {
            StringBuilder query = new StringBuilder();
            List<object> queryParams = new List<object>();
            bool first = true;
            foreach (KeyValuePair<RampColumn, object> entry in keyValPairs)
            {
                if (!first) query.Append(", ");
                query.Append($"{entry.Key} = ? ");
                queryParams.Add(entry.Value);
                first = false;
            }

            return query.ToString();
        }

        private string CreateInsertSubquery()
        {
            StringBuilder columnQuery = new StringBuilder();
            StringBuilder paramQuery = new StringBuilder();
            List<object> queryParams = new List<object>();
            bool first = true;
            foreach (KeyValuePair<RampColumn, object> entry in keyValPairs)
            {
                if (!first)
                {
                    columnQuery.Append(", ");
                    paramQuery.Append(", ");
                }
                columnQuery.Append($"{entry.Key} ");
                paramQuery.Append($"? ");
                queryParams.Add(entry.Value);
                first = false;
            }

            return $"({columnQuery}) VALUES ({paramQuery}) ";
        }

        public override string ToString()
        {
            StringBuilder query = new StringBuilder();
            if (parent != null) query.Append(parent);

            switch (operationType)
            {
                case OperationType.Update:
                    query.Append($"UPDATE {targetTable} SET {CreateUpdateSubquery()}");

                    break;
                case OperationType.Insert:
                    query.Append($"INSERT INTO {targetTable} {CreateInsertSubquery()}");
                    break;
                case OperationType.Delete:
                    query.Append($"DELETE FROM {targetTable} ");
                    break;
            }

            return query.ToString();
        }
    }

    public class RampSubQuery : RampWhereSelectorQuery
    {
        // Select specific
        private RampTable targetTable;
        private List<RampColumn> targetColumns = new List<RampColumn>();
        private List<KeyValuePair<string, string>> aliases = new List<KeyValuePair<string, string>>();
        private List<RampJoinData> joins = new List<RampJoinData>();
        private bool selectAll = false;

        public RampSubQuery(IRampQuerySection parent, RampTable table) : base(parent)
        {
            targetTable = table;
        }



        public RampSubQuery Columns(params RampColumn[] columns)
        {
            targetColumns.AddRange(columns);
            return this;
        }
        public RampSubQuery All()
        {
            selectAll = true;
            return this;
        }

        public RampSubQuery Alias(RampColumn column, string aliasName)
        {
            aliases.Add(new KeyValuePair<string, string>(column.ToString(), aliasName));
            return this;
        }

        public RampSubQuery Alias(string statement, string aliasName)
        {
            aliases.Add(new KeyValuePair<string, string>(statement, aliasName));
            return this;
        }

        public RampSubQuery Join(RampColumn existingTableColumn, RampColumn newTableColumn, TableJoinType join)
        {
            joins.Add(new RampJoinData(existingTableColumn, newTableColumn, join));
            return this;
        }

        public RampSubQuery LeftJoin(RampColumn existingTableColumn, RampColumn newTableColumn)
        {
            return Join(existingTableColumn, newTableColumn, TableJoinType.Left);
        }

        public RampSubQuery RightJoin(RampColumn existingTableColumn, RampColumn newTableColumn)
        {
            return Join(existingTableColumn, newTableColumn, TableJoinType.Right);
        }

        public RampSubQuery InnerJoin(RampColumn existingTableColumn, RampColumn newTableColumn)
        {
            return Join(existingTableColumn, newTableColumn, TableJoinType.Inner);
        }

        public override string ToString()
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            if (selectAll) query.Append("* ");
            if (selectAll && targetColumns.Count != 0) query.Append(", ");
            bool first = true;
            foreach (RampColumn column in targetColumns)
            {
                if (!first) query.Append(", ");
                query.Append(column);
                first = false;
            }

            if ((selectAll && aliases.Count != 0) || (targetColumns.Count != 0 && aliases.Count != 0)) query.Append(",");
            first = true;
            foreach (KeyValuePair<string, string> alias in aliases)
            {
                if (!first) query.Append(", ");
                query.Append($"{alias.Key} AS {alias.Value} ");
                first = false;
            }

            query.Append($"FROM {targetTable} ");

            foreach (RampJoinData join in joins)
            {
                switch (join.Type)
                {
                    case TableJoinType.Left:
                        query.Append("LEFT JOIN ");
                        break;
                    case TableJoinType.Right:
                        query.Append("RIGHT JOIN ");
                        break;
                    case TableJoinType.Inner:
                        query.Append("INNER JOIN ");
                        break;
                }
                query.Append($"{join.NewTableColumn.ParentTable} ON {join.ExistingTableColumn} = {join.NewTableColumn} ");
            }

            return query.ToString();
        }
    }


    public class RampWhereConnector : RampWhereSelectorQuery
    {
        private WhereConnectorType type = WhereConnectorType.None;
        public RampWhereConnector(IRampQuerySection subQuery) : base(subQuery) { }

        public RampWhereQuery And
        {
            get
            {
                type = WhereConnectorType.And;
                return new RampWhereQuery(this, false);
            }
        }

        public RampWhereQuery Or
        {
            get
            {
                type = WhereConnectorType.Or;
                return new RampWhereQuery(this, false);
            }
        }
        public override string ToString()
        {
            switch (type)
            {
                case WhereConnectorType.And:
                    return $"{parent} AND ";
                case WhereConnectorType.Or:
                    return $"{parent} OR ";
                default:
                    return parent.ToString();
            }
        }
    }

    public class RampWhereQuery : RampWhereSelectorQuery
    {
        private List<RampWhereData> where = new List<RampWhereData>();
        private bool initiator;
        public RampWhereQuery(IRampQuerySection subQuery, bool isInitiator) : base(subQuery)
        {
            initiator = isInitiator;
        }

        public RampWhereConnector Is(RampColumn column, object value)
        {
            where.Add(new RampWhereData(column, value, WhereType.Is, LikeWildcard.Unspecified));
            return new RampWhereConnector(this);
        }

        public RampWhereConnector Not(RampColumn column, object value)
        {
            where.Add(new RampWhereData(column, value, WhereType.IsNot, LikeWildcard.Unspecified));
            return new RampWhereConnector(this);
        }

        public RampWhereConnector Like(RampColumn column, object value, LikeWildcard likeWildcard = LikeWildcard.Unspecified)
        {
            where.Add(new RampWhereData(column, value, WhereType.IsLike, likeWildcard));
            return new RampWhereConnector(this);
        }

        public RampWhereConnector NotLike(RampColumn column, object value, LikeWildcard likeWildcard = LikeWildcard.Unspecified)
        {
            where.Add(new RampWhereData(column, value, WhereType.IsNotLike, likeWildcard));
            return new RampWhereConnector(this);
        }

        public override string ToString()
        {
            StringBuilder query = new StringBuilder();
            List<object> queryParams = new List<object>();

            if (initiator) query.Append("WHERE ");
            foreach (RampWhereData data in where)
            {
                switch (data.WhereType)
                {
                    case WhereType.Is:
                        query.Append($"{data.Column} = ? ");
                        break;
                    case WhereType.IsNot:
                        query.Append($"NOT {data.Column} = ? ");
                        break;
                    case WhereType.IsLike:
                        query.Append($"{data.Column} LIKE ");
                        break;
                    case WhereType.IsNotLike:
                        query.Append($"{data.Column} NOT LIKE ");
                        break;
                }

                switch (data.LikeWildcard)
                {
                    case LikeWildcard.MatchStart:
                        query.Append("CONCAT(?,'%') ");
                        break;
                    case LikeWildcard.MatchEnd:
                        query.Append("CONCAT('%',?) ");
                        break;
                    case LikeWildcard.MatchBoth:
                        query.Append("? ");
                        break;
                    case LikeWildcard.MatchAny:
                        query.Append("CONCAT('%',?,'%') ");
                        break;
                }

                queryParams.Add(data.Value);
            }

            return $"{parent} {query}";
        }
    }
}
