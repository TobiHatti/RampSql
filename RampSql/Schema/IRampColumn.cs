using RampSql.QueryBuilder;

namespace RampSql.Schema
{
    public interface IRampColumn : IRampValue
    {
        public string RealQuotedName { get; }
        public RampTable ParentTable { get; }
    }
}
