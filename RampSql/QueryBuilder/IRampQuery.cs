namespace RampSql.QueryBuilder
{
    public interface IRampQuery : IRampValue
    {
        public RampQueryData GetData();
    }
}
