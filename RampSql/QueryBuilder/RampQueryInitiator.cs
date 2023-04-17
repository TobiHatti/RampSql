using RampSql.Schema;

namespace RampSql.QueryBuilder
{
    public class RampQueryInitiator<Schema> : QueryHead, IRampQueryInitiator where Schema : IRampSchema
    {
        private Schema schema;
        public RampQueryInitiator() : base(new RampQueryData()) { }

        public void SetSchema(Schema schema)
        {
            this.schema = schema;
        }

        public SelectQuery Select(IRampColumn column) => Select(column, null);
        public SelectQuery Select(IRampColumn column, string alias)
        {
            data.OperationType = OperationType.Select;
            data.Target = column.ParentTable;
            column.AsAlias(alias);
            return new SelectQuery(data).Column(column);
        }
        public SelectQuery SelectFrom(IRampTable table) => SelectFrom(table, null);
        public SelectQuery SelectFrom(IRampTable table, string alias)
        {
            data.OperationType = OperationType.Select;
            data.Target = RampTable.SwitchBranch(table);
            data.Target.AsAlias(alias);
            return new SelectQuery(data);
        }

        public SelectQuery SelectFrom(Func<Schema, RampQueryInitiator<Schema>, IRampQuery> query) => SelectFrom(query, null);
        public SelectQuery SelectFrom(Func<Schema, RampQueryInitiator<Schema>, IRampQuery> query, string alias)
        {
            data.OperationType = OperationType.Select;

            Schema subSchema = RampSchema.CreateSub(schema);
            RampQueryInitiator<Schema> initiator = new RampQueryInitiator<Schema>();
            initiator.SetSchema(subSchema);
            data.Target = query(subSchema, initiator);
            data.Target.AsAlias(alias);

            return new SelectQuery(data);
        }
        public SelectQuery SelectAllFrom(IRampTable table) => SelectAllFrom(table, null);
        public SelectQuery SelectAllFrom(IRampTable table, string alias)
        {
            data.OperationType = OperationType.Select;
            data.Target = RampTable.SwitchBranch(table);
            data.Target.AsAlias(alias);
            return new SelectQuery(data).All();
        }


        public SelectQuery Count(IRampTable table) => Count(table, null);
        public SelectQuery Count(IRampTable table, string alias)
        {
            data.OperationType = OperationType.Select;
            data.Target = RampTable.SwitchBranch(table);
            data.Target.AsAlias(alias);
            return new SelectQuery(data).Count();
        }
        public SelectQuery Count(IRampColumn column) => Count(column, null);
        public SelectQuery Count(IRampColumn column, string alias)
        {
            data.OperationType = OperationType.Select;
            data.Target = column.ParentTable;
            column.AsAlias(alias);
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
            data.Target = RampTable.SwitchBranch(table);
            return new InsertQuery(data);
        }

        public UpdateQuery Update(IRampTable table)
        {
            data.OperationType = OperationType.Update;
            data.Target = RampTable.SwitchBranch(table);
            return new UpdateQuery(data);
        }

        public InsertQuery Upsert(IRampTable table)
        {
            data.OperationType = OperationType.Upsert;
            data.Target = RampTable.SwitchBranch(table);
            return new InsertQuery(data);
        }

        public WhereSelector DeleteFrom(IRampTable table)
        {
            data.OperationType = OperationType.Delete;
            data.Target = RampTable.SwitchBranch(table);
            return new WhereSelector(data);
        }

        //public IRampQuery Union(UnionType unionType, params IRampQuery[] queries) { return null; }

        //public object Free() { return null; }
    }
}
