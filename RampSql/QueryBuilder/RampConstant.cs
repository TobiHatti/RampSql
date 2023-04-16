namespace RampSql.QueryBuilder
{
    public class RampConstant : IRampConstant
    {
        public object Value { get; set; }
        public object Alias { get; set; }

        public RampConstant(object value, object alias)
        {
            Value = value;
            Alias = alias;
        }
    }
}
