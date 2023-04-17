namespace RampSql.QueryBuilder
{
    public class InsertResultQuery : QueryHead, IRampQuery
    {
        internal InsertResultQuery(RampQueryData data) : base(data) { }

        public InsertResultQuery GetLastID()
        {
            data.ReturnInsertID = true;
            return this;
        }
    }
}
