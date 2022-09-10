using RampSQL.Schema;

namespace RampSQL.Query
{
    public class QueryEngine : IQuerySection
    {
        private OperationType type = OperationType.Unknown;
        private RampTable table;

        public SelectQuery Select
        {
            get
            {
                type = OperationType.Select;
                return new SelectQuery(this);
            }
        }

        public InsertKeyValueQuery InsertInto(RampTable table)
        {
            type = OperationType.Insert;
            this.table = table;
            return new InsertKeyValueQuery(this);
        }

        public UpdateKeyValueQuery Update(RampTable table)
        {
            type = OperationType.Update;
            this.table = table;
            return new UpdateKeyValueQuery(this);
        }

        public WhereSelector DeleteFrom(RampTable table)
        {
            type = OperationType.Delete;
            this.table = table;
            return new WhereSelector(this);
        }

        public UnionQuery Union
        {
            get
            {
                type = OperationType.Union;
                return new UnionQuery(this);
            }
        }

        public override string ToString()
        {
            switch (type)
            {
                case OperationType.Select:
                    return $"SELECT ";
                case OperationType.Insert:
                    return $"INSERT INTO {table} ";
                case OperationType.Update:
                    return $"UPDATE {table} ";
                case OperationType.Delete:
                    return $"DELETE FROM {table} ";
                case OperationType.Union:
                    return $"";
            }
            return string.Empty;
        }
    }
}
