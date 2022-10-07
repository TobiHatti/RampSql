namespace RampSQL.Schema
{
    public interface IRampTable
    {
        string TableName { get; set; }
        string TableAlias { get; set; }
        bool UseAlias { get; set; }
    }
}
