using RampSql.QueryConnectors;
using RampSql.QuerySections;
using RampSql.Schema;

namespace RampSql.QueryBuilder
{
    public class RampQueryInitiator : IRampQueryInitiator
    {
        private RampQueryData data;
        public RampQueryInitiator()
        {
            data = new RampQueryData();
        }

        public SelectQuery Select(IRampColumn column)
        {
            data.OperationType = OperationType.Select;
            data.Target = (IRampValue)column.ParentTable;
            return new SelectQuery(data).Column(column);
        }
        public SelectQuery SelectFrom(IRampTable table)
        {
            data.OperationType = OperationType.Select;
            data.Target = table;
            return new SelectQuery(data);
        }
        public SelectQuery SelectFrom(IRampQuery subquery)
        {
            data.OperationType = OperationType.Select;
            data.Target = subquery;
            return new SelectQuery(data);
        }
        public SelectQuery SelectAllFrom(IRampTable table)
        {
            data.OperationType = OperationType.Select;
            data.Target = table;
            return new SelectQuery(data).All();
        }


        public SelectQuery Count(IRampTable table)
        {
            data.OperationType = OperationType.Select;
            data.Target = table;
            return new SelectQuery(data).Count();
        }
        public SelectQuery Count(IRampColumn column)
        {
            data.OperationType = OperationType.Select;
            data.Target = (IRampValue)column.ParentTable;
            return new SelectQuery(data).Count(column);
        }

        //public JoinQuery SearchFrom(IRampTable table)
        //{
        //    data.OperationType = OperationType.Select;
        //    data.SelectTarget = null;
        //    return new SelectQuery(data);
        //}

        public InsertQuery InsertInto(IRampTable table)
        {
            data.OperationType = OperationType.Insert;
            data.Target = table;
            return new InsertQuery(data);
        }

        public UpdateQuery Update(IRampTable table)
        {
            data.OperationType = OperationType.Update;
            data.Target = table;
            return new UpdateQuery(data);
        }

        public InsertQuery Upsert(IRampTable table)
        {
            data.OperationType = OperationType.Upsert;
            data.Target = table;
            return new InsertQuery(data);
        }

        public WhereSelector DeleteFrom(IRampTable table)
        {
            data.OperationType = OperationType.Delete;
            data.Target = table;
            return new WhereSelector(data);
        }

        //public IRampQuery Union(UnionType unionType, params IRampQuery[] queries) { return null; }

        //public object Free() { return null; }

        public RampQueryData GetData() => data;
    }
}
