using OfficeOpenXml;
using StudingWorkloadCalculator.Models;
using System.Collections.Generic;
using System.IO;

namespace StudingWorkloadCalculator.ExcelWriter
{
    public class ExcelReader
    {
        public static IEnumerable<T>? ReadExcel<T>(string path, int startRow = 1, int startColumn = 1)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (typeof(T) == typeof(Teacher))
            {
                return (IEnumerable<T>?)ReadExcelTeachers(path, startRow, startColumn);
            }
            else if(typeof(T) == typeof(Student))
            {
                return (IEnumerable<T>?)ReadExcelStudents(path, startRow, startColumn);
            }
            else
            {
                return null;
            }
        }

        private static IEnumerable<Teacher>? ReadExcelTeachers(string path, int startRow = 1, int startColumn = 2)
        {
            var teachers = new List<Teacher>();
            var existingFile = new FileInfo(path);
            using var package = new ExcelPackage(existingFile);
            var worksheet = package.Workbook.Worksheets[1];
            int colCount = worksheet.Dimension.End.Column;
            int rowCount = worksheet.Dimension.End.Row;
            for (int row = startRow; row <= rowCount; row++)
            {
                try
                {
                    var name = worksheet.Cells[row, startColumn].GetValue<string>();
                    var surename = worksheet.Cells[row, startColumn + 1].GetValue<string>();
                    var familyName = worksheet.Cells[row, startColumn + 2].GetValue<string>();
                    var rawGender = worksheet.Cells[row, startColumn + 3].GetValue<string>();
                    var jobExpiriece = worksheet.Cells[row, startColumn + 4].GetValue<int>();
                    var jobTytle = worksheet.Cells[row, startColumn + 5].GetValue<string>();

                    var teacher = new Teacher(name, surename, familyName, rawGender.GetGender(), jobExpiriece, jobTytle);
                    teachers.Add(teacher);
                }
                catch
                {
                    return null;
                }
            }

            return teachers;
        }

        private static IEnumerable<Student>? ReadExcelStudents(string path, int startRow = 1, int startColumn = 2)
        {
            var students = new List<Student>();
            var existingFile = new FileInfo(path);
            using var package = new ExcelPackage(existingFile);
            var worksheet = package.Workbook.Worksheets[0];
            int colCount = worksheet.Dimension.End.Column;
            int rowCount = worksheet.Dimension.End.Row;
            for (int row = startRow; row <= rowCount; row++)
            {
                try
                {
                    var name = worksheet.Cells[row, startColumn].GetValue<string>();
                    var surename = worksheet.Cells[row, startColumn + 1].GetValue<string>();
                    var familyName = worksheet.Cells[row, startColumn + 2].GetValue<string>();
                    var rawGender = worksheet.Cells[row, startColumn + 3].GetValue<string>();
                    var specialization = worksheet.Cells[row, startColumn + 4].GetValue<string>();
                    var group = worksheet.Cells[row, startColumn + 5].GetValue<string>();

                    var student = new Student(name, surename, familyName, rawGender.GetGender(), specialization, group);
                    students.Add(student);
                }
                catch
                {
                    return null;
                }
            }

            return students;
        }
    }
}
