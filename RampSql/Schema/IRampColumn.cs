using RampSql.QueryBuilder;

namespace RampSql.Schema
{
    public interface IRampColumn : IRampValue
    {
        public RampTable ParentTable { get; }
    }
}
