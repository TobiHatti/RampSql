using System;

namespace RampSQL.Query
{
    public interface IRampQuery
    {

    }

    public interface IQuerySection : IRampQuery
    {
        object[] GetParameters();
    }

    public interface IWhereQuerySection
    {

    }

    public interface IWhereQuerySegment : ICloneable
    {

    }

    public interface IHavingQuerySegment : ICloneable
    {

    }
}
