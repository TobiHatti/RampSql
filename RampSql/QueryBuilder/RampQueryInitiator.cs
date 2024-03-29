﻿using RampSql.Schema;

namespace RampSql.QueryBuilder
{
    public class RampQueryInitiator<Schema> : QueryHead, IRampQueryInitiator where Schema : RampSchema<Schema>
    {
        public RampQueryInitiator() : base(new RampQueryData()) { }

        public void SetSchema(Schema schema)
        {
            data.Schema = schema;
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
            data.Target = RampTableData.SwitchBranch(table);
            data.Target.AsAlias(alias);
            return new SelectQuery(data);
        }

        public SelectQuery SelectAllFrom(Func<Schema, RampQueryInitiator<Schema>, IRampQuery> query, string alias) => SelectFrom(query, alias).All();
        public SelectQuery SelectFrom(Func<Schema, RampQueryInitiator<Schema>, IRampQuery> query, string alias)
        {
            RampQueryInitiator<Schema> initiator = new RampQueryInitiator<Schema>();
            Schema subSchema = RampSchemaData.CreateSub((Schema)data.Schema);
            subSchema.SetParentSchema((Schema)data.Schema);
            data.OperationType = OperationType.Select;
            data.Target = query(subSchema, initiator);
            data.Target.AsAlias(alias);
            subSchema.Alias = alias;
            RampSchemaData.SwitchBranch(subSchema).Alias = alias;
            (data.Schema as Schema).RegisterSubSchema(subSchema);
            initiator.SetSchema(subSchema);
            return new SelectQuery(data);
        }
        public SelectQuery SelectAllFrom(IRampTable table) => SelectAllFrom(table, null);
        public SelectQuery SelectAllFrom(IRampTable table, string alias)
        {
            data.OperationType = OperationType.Select;
            data.Target = RampTableData.SwitchBranch(table);
            data.Target.AsAlias(alias);
            return new SelectQuery(data).All();
        }


        public SelectQuery Count(IRampTable table) => Count(table, null);
        public SelectQuery Count(IRampTable table, string alias)
        {
            data.OperationType = OperationType.Select;
            data.Target = RampTableData.SwitchBranch(table);
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
            data.Target = RampTableData.SwitchBranch(table);
            return new InsertQuery(data);
        }

        public UpdateQuery Update(IRampTable table)
        {
            data.OperationType = OperationType.Update;
            data.Target = RampTableData.SwitchBranch(table);
            return new UpdateQuery(data);
        }

        public InsertQuery Upsert(IRampTable table)
        {
            data.OperationType = OperationType.Upsert;
            data.Target = RampTableData.SwitchBranch(table);
            return new InsertQuery(data);
        }

        public WhereSelector DeleteFrom(IRampTable table)
        {
            data.OperationType = OperationType.Delete;
            data.Target = RampTableData.SwitchBranch(table);
            return new WhereSelector(data);
        }

        public IRampQuery Sub(Func<Schema, RampQueryInitiator<Schema>, IRampQuery> query)
        {
            Schema db = RampSchemaData.CreateSub((Schema)data.Schema);
            RampQueryInitiator<Schema> initiator = new RampQueryInitiator<Schema>();
            initiator.SetSchema(db);
            return query(db, initiator);
        }

        public WhereExtSelector Union(UnionType unionType, params IRampQuery[] queries)
        {
            data.OperationType = OperationType.Union;
            data.Union = queries;
            data.UnionType = unionType;
            return new WhereExtSelector(data);
        }

        public IRampFunction Func(SqlFunction function, params IRampValue[] parameters) => Func(function.ToString(), parameters);
        public IRampFunction Func(MySqlFunction function, params IRampValue[] parameters) => Func(function.ToString(), parameters);
        public IRampFunction Func(string function, params IRampValue[] parameters) => new RampFunction(new RampFunctionElement(data, function, parameters));
    }
}
