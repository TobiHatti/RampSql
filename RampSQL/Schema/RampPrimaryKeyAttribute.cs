using System;

namespace RampSQL.Schema
{
    public enum PrimaryKeyType
    {
        AutoIncrement,
        Guid,
        Custom
    }

    public class RampPrimaryKeyAttribute : Attribute
    {
        public PrimaryKeyType PrimaryKeyType { get; set; }
        public RampPrimaryKeyAttribute(PrimaryKeyType pkType)
        {
            PrimaryKeyType = pkType;
        }
    }
}
