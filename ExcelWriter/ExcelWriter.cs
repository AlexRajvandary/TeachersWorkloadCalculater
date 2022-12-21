using OfficeOpenXml;
using StudingWorkloadCalculator.UserControls;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace StudingWorkloadCalculator.ExcelWriter
{
    public class ExcelWriter
    {
        public static void WriteExcelFile<T>(string path, IEnumerable<T> items)
        {
            var itemslist = items?.ToList();
            var itemCount = itemslist?.Count ?? 0;
            var wasCreated = false;
            if (!File.Exists(path))
            {
                File.Create(path);
            }

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var package = new ExcelPackage(path);
            var worksheet = package.Workbook.Worksheets.Count > 0 ? package.Workbook.Worksheets[0] : package.Workbook.Worksheets.Add(typeof(T).Name);
            var properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            var columnNames = properties.Select(property => (GetPropertyDescriptor(property).Attributes.OfType<DataGridColumnGeneratorAttribute>().FirstOrDefault()?.ColumnName, property)).Where(i => i.ColumnName != null).ToArray();
            var columnAmount = columnNames.Length;

            for (var i = 0; i < columnAmount; i++)
            {
                worksheet.Cells[1, 1 + i].Value = columnNames[i].ColumnName;
            }

            for (var i = 0; i < columnAmount; i++)
            {
                for(var j = 2; j -2 < itemCount; j++)
                {
                    worksheet.Cells[j, i++].Value = properties[i].GetValue(itemslist[j - 2]).ToString();
                }
            }

            package.Save();
        }

        public static PropertyDescriptor GetPropertyDescriptor(PropertyInfo PropertyInfo)
        {
            return TypeDescriptor.GetProperties(PropertyInfo.DeclaringType).Find(PropertyInfo.Name, true);
        }
    }
}
