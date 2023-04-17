namespace RampSql.QueryBuilder
{
    public interface IRampQuery : IRampValue, IRampTarget
    {
        public RampQueryData GetData();
    }
}
