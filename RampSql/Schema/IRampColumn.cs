using RampSql.QueryBuilder;

namespace RampSql.Schema
{
    public interface IRampColumn : IRampValue
    {
        public string RealQuotedName { get; }
        public RampTableData ParentTable { get; }
    }
}
