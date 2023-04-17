namespace RampSql.QueryBuilder
{
    public interface IRampFunction : IRampValue
    {
        public string DeclaringStatement { get; }
    }
}
