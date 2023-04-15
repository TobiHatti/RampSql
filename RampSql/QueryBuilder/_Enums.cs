namespace RampSql.QueryBuilder
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
        Search,
        Union,
        Upsert
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
        IsNull,
        IsNotNull,
        In,
        SectionStart
    }

    public enum LikeWildcard
    {
        NoParameter,
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
