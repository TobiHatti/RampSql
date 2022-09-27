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
        In,
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

    public enum SqlFunction
    {
        // Aggregate Functions 
        AVG,
        COUNT,
        FIRST,
        LAST,
        MAX,
        MIN,
        SUM,

        // Scalar Functions
        UCASE,
        LCASE,
        MID,
        LEN,
        ROUND,
        NOW,
        FORMAT,
    }

    public enum MySqlFunction
    {
        // MySQL String Functions
        ASCII,
        CHAR_LENGTH,
        CHARACTER_LENGTH,
        CONCAT,
        CONCAT_WS,
        FIELD,
        FIND_IN_SET,
        FORMAT,
        INSERT,
        INSTR,
        LCASE,
        LEFT,
        LENGTH,
        LOCATE,
        LOWER,
        LPAD,
        LTRIM,
        MID,
        POSITION,
        REPEAT,
        REPLACE,
        REVERSE,
        RIGHT,
        RPAD,
        RTRIM,
        SPACE,
        STRCMP,
        SUBSTR,
        SUBSTRING,
        SUBSTRING_INDEX,
        TRIM,
        UCASE,
        UPPER,

        // MySQL Numeric Functions
        ABS,
        ACOS,
        ASIN,
        ATAN,
        ATAN2,
        AVG,
        CEIL,
        CEILING,
        COS,
        COT,
        COUNT,
        DEGREES,
        DIV,
        EXP,
        FLOOR,
        GREATEST,
        LEAST,
        LN,
        LOG10,
        LOG2,
        MAX,
        MIN,
        MOD,
        PI,
        POW,
        POWER,
        RADIANTS,
        RAND,
        ROUND,
        SIGN,
        SIN,
        SQRT,
        SUM,
        TAN,
        TRUNCATE,

        // MySQL Date Functions
        ADDDATE,
        ADDTIME,
        CURDATE,
        CURRENT_DATE,
        CURRENT_TIME,
        CURRENT_TIMESTAMP,
        CURTIME,
        DATE,
        DATEDIFF,
        DATE_ADD,
        DATE_FORMAT,
        DATE_SUB,
        DAY,
        DAYNAME,
        DAYOFMONTH,
        DAYOFWEEK,
        DAYOFYEAR,
        EXTRACT,
        FROM_DAYS,
        HOUR,
        LAST_DAY,
        LOCALTIME,
        LOCALTIMESTAMP,
        MAKEDATE,
        MAKETIME,
        MICROSECOND,
        MINUTE,
        MONTH,
        MONTHNAME,
        NOW,
        PERIOD_ADD,
        PERIOD_DIFF,
        QUARTER,
        SECOND,
        SEC_TO_TIME,
        SEC_TO_DATE,
        SUBTIME,
        SYSDATE,
        TIME,
        TIME_FORMAT,
        TIME_TO_SEC,
        TIMEDIFF,
        TIMESTAMP,
        TO_DAYS,
        WEEK,
        WEEKDAY,
        WEEKOFYEAR,
        YEAR,
        YEARWEEK,

        // MySQL Advanced functions
        BIN,
        BINARY,
        CASE,
        CAST,
        COALESCE,
        CONNECTION_ID,
        CONV,
        CONVERT,
        CURRENT_USER,
        DATABASE,
        IF,
        IFNULL,
        ISNULL,
        LAST_INSERT_ID,
        NULLIF,
        SESSION_USER,
        SYSTEM_USER,
        USER,
        VERSION
    }
}
