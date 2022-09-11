﻿using RampSQL.Schema;

namespace RampSQL.Query
{
    public class QueryEngine : IQuerySection
    {
        private QueryData data;

        public SelectQuery SelectFrom(RampTable table) => SelectFrom(table, null);
        public SelectQuery SelectFrom(RampTable table, string alias) => SelectFrom(table.ToString(), alias);
        public SelectQuery SelectFrom(QueryEngine subQuery, string alias) => SelectFrom(subQuery.ToString(), alias);
        public SelectQuery SelectFrom(string query) => SelectFrom(query, null);
        public SelectQuery SelectFrom(string query, string alias)
        {
            data = new QueryData();

            data.QueryType = OperationType.Select;
            data.SelectTargetTable = query;
            data.SelectTableAlias = alias;
            return new SelectQuery(data);
        }

        public InsertKeyValueQuery InsertInto(RampTable table)
        {
            data = new QueryData();

            data.QueryType = OperationType.Insert;
            data.TargetTable = table;
            return new InsertKeyValueQuery(data);
        }

        public UpdateKeyValueQuery Update(RampTable table)
        {
            data = new QueryData();

            data.QueryType = OperationType.Update;
            data.TargetTable = table;
            return new UpdateKeyValueQuery(data);
        }

        public WhereSelector DeleteFrom(RampTable table)
        {
            data = new QueryData();

            data.QueryType = OperationType.Delete;
            data.TargetTable = table;
            return new WhereSelector(data);
        }

        public UnionQuery Union(UnionType unionType)
        {
            data = new QueryData();

            data.QueryType = OperationType.Union;
            data.UnionType = unionType;
            return new UnionQuery(data);
        }

        public object[] GetParameters() => data.GetParameters();
        public override string ToString() => data.RenderQuery();
    }
}

