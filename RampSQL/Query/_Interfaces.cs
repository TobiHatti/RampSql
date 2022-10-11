using System;

namespace RampSQL.Query
{

    public interface IQuerySection
    {
        object[] GetParameters();
        IQuerySection Clone();
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
