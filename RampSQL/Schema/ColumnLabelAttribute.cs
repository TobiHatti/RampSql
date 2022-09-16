using System;

namespace RampSQL.Schema
{
    public class ColumnLabelAttribute : Attribute
    {
        public string Label { get; set; } = string.Empty;
        public ColumnLabelAttribute(string label)
        {
            Label = label;
        }
    }
}
