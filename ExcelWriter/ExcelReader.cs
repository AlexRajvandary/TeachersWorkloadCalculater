using OfficeOpenXml;
using StudingWorkloadCalculator.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace StudingWorkloadCalculator.ExcelWriter
{
    public class ExcelReader
    {
        public static IEnumerable<T>? ReadExcel<T>(string path, int startRow = 1, int startColumn = 2)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (typeof(T) == typeof(Teacher))
            {
                return (IEnumerable<T>?)ReadExcelTeachers(path, startRow, startColumn);
            }
            else if (typeof(T) == typeof(Student))
            {
                return (IEnumerable<T>?)ReadExcelStudents(path, startRow, startColumn);
            }
            else if (typeof(T) == typeof(SubjectWithWorkload))
            {
                return (IEnumerable<T>?)ReadExcelSubjects(path, startRow, startColumn);
            }
            else if (typeof(T) == typeof(Specialization))
            {
                return (IEnumerable<T>?)ReadExcelSpecialization(path, startRow, startColumn);
            }
            else if (typeof(T) == typeof(Group))
            {
                return (IEnumerable<T>?)ReadExcelGroups(path, startRow, startColumn);
            }
            else
            {
                return null;
            }
        }

        private static IEnumerable<Teacher>? ReadExcelTeachers(string path, int startRow, int startColumn)
        {
            var teachers = new List<Teacher>();
            var existingFile = new FileInfo(path);
            using var package = new ExcelPackage(existingFile);
            var worksheet = package.Workbook.Worksheets[0];
            int colCount = worksheet.Dimension.End.Column;
            int rowCount = worksheet.Dimension.End.Row;
            for (int row = startRow; row <= rowCount; row++)
            {
                try
                {
                    var id = worksheet.Cells[row, startColumn].GetValue<int>();
                    var name = worksheet.Cells[row, startColumn + 2].GetValue<string>();
                    var surename = worksheet.Cells[row, startColumn + 1].GetValue<string>();
                    var familyName = worksheet.Cells[row, startColumn + 3].GetValue<string>();
                    var rawGender = worksheet.Cells[row, startColumn + 5].GetValue<string>();
                    var jobExpiriece = worksheet.Cells[row, startColumn + 6].GetValue<int>();
                    var jobTytle = worksheet.Cells[row, startColumn + 4].GetValue<string>();
                    var qualification = worksheet.Cells[row, startColumn + 6].GetValue<string>();
                    var subject = worksheet.Cells[row, startColumn + 7].GetValue<string>();

                    var teacher = new Teacher(id, name, surename, familyName, rawGender.GetGender(), jobExpiriece, jobTytle, qualification, subject);
                    teachers.Add(teacher);
                }
                catch
                {
                    return null;
                }
            }

            return teachers;
        }

        private static IEnumerable<Student>? ReadExcelStudents(string path, int startRow, int startColumn)
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

        private static IEnumerable<SubjectWithWorkload>? ReadExcelSubjects(string path, int startRow, int startColumn)
        {
            var subjects = new List<SubjectWithWorkload>();
            var existingFile = new FileInfo(path);
            using var package = new ExcelPackage(existingFile);
            var worksheet = package.Workbook.Worksheets[0];
            int colCount = worksheet.Dimension.End.Column;
            int rowCount = worksheet.Dimension.End.Row;
            for (int row = startRow; row <= rowCount; row++)
            {
                try
                {
                    var code = worksheet.Cells[row, startColumn].GetValue<int>();
                    var group = worksheet.Cells[row, startColumn + 1].GetValue<string>();
                    var name = worksheet.Cells[row, startColumn + 2].GetValue<string>();
                    var theory = worksheet.Cells[row, startColumn + 3].GetValue<int>();
                    var ipz = worksheet.Cells[row, startColumn + 4].GetValue<int>();
                    var kr = worksheet.Cells[row, startColumn + 5].GetValue<int>();
                    var firstSemestr = worksheet.Cells[row, startColumn + 6].GetValue<int>();
                    var secondSemestr = worksheet.Cells[row, startColumn + 7].GetValue<int>();

                    var subject = new SubjectWithWorkload(code, group, name, theory, ipz, kr, firstSemestr, secondSemestr);
                    subjects.Add(subject);
                }
                catch
                {
                    return null;
                }
            }

            return subjects;
        }

        private static IEnumerable<Specialization>? ReadExcelSpecialization(string path, int startRow, int startColumn)
        {
            var specializations = new List<Specialization>();
            var existingFile = new FileInfo(path);
            using var package = new ExcelPackage(existingFile);
            var worksheet = package.Workbook.Worksheets[0];
            int colCount = worksheet.Dimension.End.Column;
            int rowCount = worksheet.Dimension.End.Row;
            for (int row = startRow; row <= rowCount; row++)
            {
                try
                {
                    var id = worksheet.Cells[row, startColumn].GetValue<int>();
                    var code = worksheet.Cells[row, startColumn + 1].GetValue<string>();
                    var intramuralString = worksheet.Cells[row, startColumn + 2].GetValue<string>();
                    var intramural = intramuralString.ToLower() == "очно";
                    var name = worksheet.Cells[row, startColumn + 3].GetValue<string>();
                    var studyPeriod = worksheet.Cells[row, startColumn + 4].GetValue<string>();
                    var qualification = worksheet.Cells[row, startColumn + 5].GetValue<string>();

                    var specialization = new Specialization(id, code, name, studyPeriod, qualification, intramural);
                    specializations.Add(specialization);
                }
                catch
                {
                    return null;
                }
            }

            return specializations;
        }

        private static IEnumerable<Group>? ReadExcelGroups(string path, int startRow, int startColumn)
        {
            var groups = new List<Group>();
            var existingFile = new FileInfo(path);
            using var package = new ExcelPackage(existingFile);
            var worksheet = package.Workbook.Worksheets[0];
            int colCount = worksheet.Dimension.End.Column;
            int rowCount = worksheet.Dimension.End.Row;
            for (int row = startRow; row <= rowCount; row++)
            {
                try
                {
                    var id = worksheet.Cells[row, startColumn].GetValue<int>();
                    var code = worksheet.Cells[row, startColumn + 1].GetValue<string>();
                    var specialization = worksheet.Cells[row, startColumn + 2].GetValue<string>();
                    var amountOfStudents = worksheet.Cells[row, startColumn + 3].GetValue<int>();
                    var grade = worksheet.Cells[row, startColumn + 4].GetCellValue<int>();
                    var teacherName = worksheet.Cells[row, startColumn + 5].GetValue<string>();
                    var start = worksheet.Cells[row, startColumn + 6].GetValue<DateTime>();
                    var end = worksheet.Cells[row, startColumn + 7].GetValue<DateTime>();
                    var budget = worksheet.Cells[row, startColumn + 8].GetValue<bool>();
                    
                    var group = new Group(id, code, specialization, amountOfStudents, grade, teacherName, start, end, budget);
                    groups.Add(group);
                }
                catch
                {
                    return null;
                }
            }

            return groups;
        }
    }
}
