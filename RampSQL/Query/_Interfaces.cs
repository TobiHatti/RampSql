using System;

namespace RampSQL.Query
{
    public interface IRampSelectable
    {
        string AliasDeclaration { get; }
        string RealName { get; }
        string AliasName { get; }
        void SetAlias(string alias);
    }

    public interface IQuerySection : IRampSelectable
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
