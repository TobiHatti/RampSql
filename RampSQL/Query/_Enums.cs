namespace RampSQL.Query
{
    public enum TableJoinType
    {
        Inner,
        Left,
        Right,
        FullOuter
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }

    internal enum OperationType
    {
        Unknown,
        Select,
        Insert,
        Update,
        Delete,
        Union
    }

    internal enum WhereConnectorType
    {
        And,
        Or,
        None
    }

    internal enum WhereType
    {
        Is,
        IsNot,
        IsLike,
        IsNotLike
    }

    public enum LikeWildcard
    {
        Unspecified,
        MatchStart,
        MatchEnd,
        MatchBoth,
        MatchAny
    }
}
