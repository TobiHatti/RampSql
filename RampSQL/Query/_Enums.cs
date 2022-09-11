namespace RampSQL.Query
{
    public enum TableJoinType
    {
        Inner,
        Left,
        Right
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }

    public enum OperationType
    {
        Unknown,
        Select,
        Insert,
        Update,
        Delete,
        Union
    }

    public enum ConditionConnectorType
    {
        And,
        Or,
        None,
        SectionEnd
    }

    public enum WhereType
    {
        Is,
        IsNot,
        IsLike,
        IsNotLike,
        SectionStart
    }

    public enum LikeWildcard
    {
        Unspecified,
        MatchStart,
        MatchEnd,
        MatchBoth,
        MatchAny
    }

    public enum UnionType
    {
        Union,
        UnionAll
    }
}
