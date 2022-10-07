using RampSQL.Schema;
using System;

namespace RampSQL.Query
{
    public class QueryEngine : IQuerySection
    {
        private QueryData data;

        public string AliasDeclaration { get => data.AliasDeclaration; }
        public string RealName { get => data.RealName; }
        public string AliasName { get => data.AliasName; }
        public void SetAlias(string alias) => data.SetAlias(alias);

        public QueryEngine()
        {
            data = new QueryData();
            RampTable.ResetAliases();
        }
        public QueryEngine(QueryData data)
        {
            this.data = data;
        }

        public SelectQuery SelectFrom(RampTable table) => Select<SelectQuery>(table, null, false);
        public SelectQuery SelectFrom(RampTable table, string alias) => Select<SelectQuery>(table, alias, false);
        public SelectQuery SelectFrom(IQuerySection subQuery, string alias) => Select<SelectQuery>(subQuery, alias, false);
        public SelectQuery SelectFrom(string query) => Select<SelectQuery>(new RampStringSelector(query, null), null, false);
        public SelectQuery SelectFrom(string query, string alias) => Select<SelectQuery>(new RampStringSelector(query, alias), null, false);

        public SelectQuery SelectAllFrom(RampTable table) => SelectAllFrom(table, null);
        public SelectQuery SelectAllFrom(RampTable table, string alias) => SelectFrom(table, alias).All();

        public SelectQuery Select(RampColumn column) => SelectFrom(column.ParentTable).Column(column, null);
        public SelectQuery Select(RampColumn column, string alias) => SelectFrom(column.ParentTable).Column(column, alias);


        public JoinQuery SelectDistinct(RampColumn column) => SelectDistinct(column, null);
        public JoinQuery SelectDistinct(RampColumn column, string alias)
        {
            data.QueryType = OperationType.Select;
            data.SelectDistinct = true;
            //data.SelectTargetTable = column.ParentTable.ToString();
            //data.SelectTableAlias = alias;
            data.SelectColumns.Add(new RampParameterType(column, alias));
            return new JoinQuery(data);
        }


        private QuerySection Select<QuerySection>(IRampSelectable select, string alias, bool distinct)
        {
            data.QueryType = OperationType.Select;
            data.SelectDistinct = distinct;
            data.SelectTarget = select;
            if (!string.IsNullOrEmpty(alias)) select.SetAlias(alias);
            return (QuerySection)Activator.CreateInstance(typeof(QuerySection), data);
        }


        public SelectQuery Count(RampTable table) => SelectFrom(table).Count();
        public SelectQuery Count(RampColumn column) => Count(column, null);
        public SelectQuery Count(RampColumn column, string alias) => SelectFrom(column.ParentTable).Count(column, alias);


        public JoinQuery SearchFrom(RampTable table)
        {
            data.QueryType = OperationType.Search;
            //data.SelectTargetTable = table.ToString();
            //data.SelectTableAlias = null;
            return new JoinQuery(data);
        }

        public InsertKeyValueQuery InsertInto(RampTable table)
        {
            data.QueryType = OperationType.Insert;
            data.TargetTable = table;
            return new InsertKeyValueQuery(data);
        }

        public UpdateKeyValueQuery Update(RampTable table)
        {
            data.QueryType = OperationType.Update;
            data.TargetTable = table;
            return new UpdateKeyValueQuery(data);
        }

        public InsertKeyValueQuery Upsert(RampTable table)
        {
            data.QueryType = OperationType.Upsert;
            data.TargetTable = table;
            return new InsertKeyValueQuery(data);
        }

        public WhereSelector DeleteFrom(RampTable table)
        {
            data.QueryType = OperationType.Delete;
            data.TargetTable = table;
            return new WhereSelector(data);
        }

        public UnionQuery Union(UnionType unionType)
        {
            data.QueryType = OperationType.Union;
            data.UnionType = unionType;
            return new UnionQuery(data);
        }

        public WhereExtSelector Skip()
        {
            return new WhereExtSelector(data);
        }

        public object[] GetParameters() => data.GetParameters();
        public override string ToString() => data.RenderQuery();
        public IQuerySection Clone() => new QueryEngine((QueryData)data.Clone());
    }
}

