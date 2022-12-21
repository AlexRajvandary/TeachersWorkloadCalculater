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
            var itemslist = items.ToList();
            if (!File.Exists(path))
            {
                File.Create(path);
            }

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var package = new ExcelPackage(path);
            var worksheet = package.Workbook.Worksheets[0];
            var properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            var columnNames = properties.Select(property => GetPropertyDescriptor(property).Attributes.OfType<DataGridColumnGeneratorAttribute>().FirstOrDefault().ColumnName).ToArray();
            var columnAmount = properties.Length;

            for (var i = 0; i < columnAmount; i++)
            {
                worksheet.Cells[1, i++].SetCellValue(0, 0, columnNames[0]);
            }

            for (var i = 0; i < columnAmount; i++)
            {
                for(var j = 2; j -2 < items.Count(); j++)
                {
                    worksheet.Cells[j, i++].SetCellValue(0, 0, properties[i].GetValue(itemslist[j - 2]).ToString());
                }
            }
        }

        public static PropertyDescriptor GetPropertyDescriptor(PropertyInfo PropertyInfo)
        {
            return TypeDescriptor.GetProperties(PropertyInfo.DeclaringType).Find(PropertyInfo.Name, true);
        }
    }
}
