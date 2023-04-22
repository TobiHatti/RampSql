namespace RampSql.QueryBuilder
{
    public interface IRampQuery : IRampValue, IRampTarget
    {
        public RampQueryData GetData();
        public string GetQuery();
        public IRampQuery GetRampQuery();
        public object[] GetParameters();
        public RampBuilder GetBuilder();
        public RampRenderEngine GetRenderer();
    }
}
