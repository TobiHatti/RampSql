namespace RampSql.QueryBuilder.QueryRenderer
{
    public interface IRampRenderInstruction
    {
        public string Render(RampRenderEngine engine);
    }
}
