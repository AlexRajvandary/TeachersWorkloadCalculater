using System;

namespace StudingWorkloadCalculator.UserControls
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataGridColumnGeneratorAttribute : Attribute
    {
        public DataGridColumnGeneratorAttribute(bool generateColumn)
        {
            GenerateColumn = generateColumn;
        }

        public DataGridColumnGeneratorAttribute(string name)
        {
            ColumnName = name;
            GenerateColumn = true;
        }

        public string ColumnName { get; private set; }

        public bool GenerateColumn { get; private set; }
    }
}
