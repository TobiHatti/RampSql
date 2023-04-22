namespace RampSql.QueryBuilder
{
    public interface IRampFunction : IRampValue
    {
        public void As(string alias);
        public RampRenderEngine GetRenderer();
    }
}
