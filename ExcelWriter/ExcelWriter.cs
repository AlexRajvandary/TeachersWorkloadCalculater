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
using System.Windows.Media;
using System.Windows.Shapes;

namespace StudingWorkloadCalculator.ExcelWriter
{
    public class ExcelWriter
    {
        public static void WriteExcelFile<T>(string path, IEnumerable<T> items)
        {
            var itemslist = items?.ToList();
            var itemCount = itemslist?.Count ?? 0;

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
                    worksheet.Cells[j, i + 1].Value = properties[i].GetValue(itemslist[j - 2]).ToString();
                }
            }

            if (File.Exists(path))
                File.Delete(path);
            
            FileStream objFileStrm = File.Create(path);
            objFileStrm.Close();

            File.WriteAllBytes(path, package.GetAsByteArray());

            package.Dispose();
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

            worksheet.Cells["B3"].Value = "ГАПОУ \"Бугульминский машиностроительный техникум\"";
            worksheet.Cells["B3:B6"].Merge = true;
            worksheet.Cells["B3:B6"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            worksheet.Cells["B3:B6"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
            worksheet.Cells["M3"].Value = "Утверждаю";
            worksheet.Cells["M4"].Value = "Директор техникума";
            worksheet.Cells["M4:N4"].Merge = true;
            worksheet.Cells["M5"].Value = "____________Хабипов И.И.";
            worksheet.Cells["M5:N5"].Merge = true;
            worksheet.Cells["O6"].Value = "2020 г.";
            worksheet.Cells["C6"].Value = "ПЕДАГОГИЧЕСКАЯ НАГРУЗКА";
            worksheet.Cells["C6:F6"].Merge = true;
            worksheet.Cells["C7"].Value = "преподавателя техникума на 2020-2021  учебный год";
            worksheet.Cells["C7:F7"].Merge = true;
            worksheet.Cells["C8"].Value = teachersName;
            worksheet.Cells["C8:F8"].Merge = true;

            worksheet.Cells["M3"].Style.Font.Bold = true;
            worksheet.Cells["M4"].Style.Font.Bold = true;
            worksheet.Cells["M5"].Style.Font.Bold = true;
            worksheet.Cells["O6"].Style.Font.Bold = true;
            worksheet.Cells["D6"].Style.Font.Bold = true;
            worksheet.Cells["C7"].Style.Font.Bold = true;
            worksheet.Cells["E8"].Style.Font.Bold = true;

            var properties = typeof(SubjectWithWorkload).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var columnNames = properties.Select(property => (GetPropertyDescriptor(property).Attributes.OfType<DataGridColumnGeneratorAttribute>().FirstOrDefault()?.ColumnName, property)).Where(i => i.ColumnName != null).ToArray();
            var columnAmount = columnNames.Length;

            worksheet.Cells[11, 1].Value = "№ п/п";
            worksheet.Cells[11, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);

            for (var i = 0; i < columnAmount; i++)
            {
                worksheet.Cells[11, 2 + i].Value = columnNames[i].ColumnName;
                worksheet.Cells[11, 2 + i].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
            }

            for (var i = 0; i < columnAmount + 1; i++)
            {
                for (var j = 2; j - 2 < itemCount; j++)
                {
                    if(i == 0)
                    {
                        worksheet.Cells[10 + j, i + 1].Value = j - 1;
                        worksheet.Cells[10 + j, i + 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    }
                    else
                    {
                        worksheet.Cells[10 + j, i + 1].Value = columnNames[i - 1].property.GetValue(subjects[j - 2]).ToString();
                    }

                    if (i + 2 == columnAmount)
                    {
                        worksheet.Cells[10 + j, i + 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    }
                }
            }

            worksheet.Cells[12 + itemCount, 3].Value = "Итого";
            worksheet.Cells[12 + itemCount, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);

            worksheet.Cells[12 + itemCount, 4].Value = teacherWorkLoad.Workload.Theory.ToString();
            worksheet.Cells[12 + itemCount, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);

            worksheet.Cells[12 + itemCount, 5].Value = teacherWorkLoad.Workload.FirstSemester.ToString();
            worksheet.Cells[12 + itemCount, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);

            worksheet.Cells[12 + itemCount, 6].Value = teacherWorkLoad.Workload.SecondSemester.ToString();
            worksheet.Cells[12 + itemCount, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);

            worksheet.Cells[12 + itemCount, 7].Value = teacherWorkLoad.Workload.Kr.ToString();
            worksheet.Cells[12 + itemCount, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);

            worksheet.Cells[12 + itemCount, 8].Value = teacherWorkLoad.Workload.Ipz.ToString();
            worksheet.Cells[12 + itemCount, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);

            worksheet.Cells[12 + itemCount, 9].Value = teacherWorkLoad.Workload.Total.ToString();
            worksheet.Cells[12 + itemCount, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);

            worksheet.Cells[14 + itemCount, 3].Value = "Всего оплачиваемых часов за год";
            worksheet.Cells[14 + itemCount, 3, 14 + itemCount, 5].Merge = true;
            worksheet.Cells[14 + itemCount, 3].Style.Font.Bold = true;
            worksheet.Cells[14 + itemCount, 6].Value = teacherWorkLoad.Workload.Total;
            worksheet.Cells[14 + itemCount, 6].Style.Font.Bold = true;

            worksheet.Cells[16 + itemCount, 3].Value = "Преподаватель";
            worksheet.Cells[16 + itemCount, 3, 16 + itemCount, 5].Merge = true;
            worksheet.Cells[16 + itemCount, 6].Value = teachersName;

            worksheet.Cells[18 + itemCount, 3].Value = "Зам.директора по УР:";
            worksheet.Cells[18 + itemCount, 3, 18 + itemCount, 5].Merge = true;
            worksheet.Cells[18 + itemCount, 6].Value ="Жакупова О.В.";

            worksheet.Cells["A1:Z100"].AutoFitColumns();
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
