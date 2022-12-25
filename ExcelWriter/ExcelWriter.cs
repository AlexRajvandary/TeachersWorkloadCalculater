using OfficeOpenXml;
using StudingWorkloadCalculator.Models;
using StudingWorkloadCalculator.UserControls;
using StudingWorkloadCalculator.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Shapes;

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
                for (var j = 2; j - 2 < itemCount; j++)
                {
                    worksheet.Cells[j, i++].Value = properties[i].GetValue(itemslist[j - 2]).ToString();
                }
            }

            package.Save();
        }

        public static string GenerateReport(TeachersWorkloadViewModel teacherWorkLoad)
        {
            var itemCount = teacherWorkLoad.Subjects.Count();
            var subjects = teacherWorkLoad.Subjects.ToList();
            var teachersName = teacherWorkLoad.Teacher.ToString();
            var path = teachersName + DateTime.Now.Date.ToShortDateString() + ".xlsx";

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var package = new ExcelPackage();

            var worksheet = package.Workbook.Worksheets.Add("Нагрузка");

            worksheet.Cells["M3"].Value = "Утверждаю";
            worksheet.Cells["M4"].Value = "Директор техникума";
            worksheet.Cells["M5"].Value = "____________Хабипов И.И.";
            worksheet.Cells["O6"].Value = "2020 г.";
            worksheet.Cells["D6"].Value = "ПЕДАГОГИЧЕСКАЯ НАГРУЗКА";
            worksheet.Cells["C7"].Value = "преподавателя техникума на 2020-2021  учебный год";
            worksheet.Cells["E8"].Value = teachersName;

            var properties = typeof(SubjectWithWorkload).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            var columnNames = properties.Select(property => (GetPropertyDescriptor(property).Attributes.OfType<DataGridColumnGeneratorAttribute>().FirstOrDefault()?.ColumnName, property)).Where(i => i.ColumnName != null).ToArray();
            var columnAmount = columnNames.Length;

            for (var i = 0; i < columnAmount; i++)
            {
                worksheet.Cells[11, 1 + i].Value = columnNames[i].ColumnName;
            }

            for (var i = 0; i < columnAmount; i++)
            {
                for (var j = 2; j - 2 < itemCount; j++)
                {
                    worksheet.Cells[9 + j, i + 12].Value = properties[i].GetValue(subjects[j - 2]).ToString();
                }
            }

            if (File.Exists(path))
                File.Delete(path);

            FileStream objFileStrm = File.Create(path);
            objFileStrm.Close();

            File.WriteAllBytes(path, package.GetAsByteArray());

            package.Dispose();
            return path;
        }

        public static PropertyDescriptor GetPropertyDescriptor(PropertyInfo PropertyInfo)
        {
            return TypeDescriptor.GetProperties(PropertyInfo.DeclaringType).Find(PropertyInfo.Name, true);
        }
    }
}
