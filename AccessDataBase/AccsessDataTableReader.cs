using StudingWorkloadCalculator.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace StudingWorkloadCalculator.AccessDataBase
{
    public class AccsessDataTableReader
    {
        public static IEnumerable<Teacher> GetTeachers()
        {
            var data = GetData("SELECT * FROM Преподаватели;");
            var teachers = new List<Teacher>();
            foreach (var row in data)
            {
                var id = (int)row[0];
                var lastName = row[1] as string ?? string.Empty;
                var name = row[2] as string ?? string.Empty;
                var familyName = row[3] as string ?? string.Empty;
                var jobTitle = row[4] as string ?? string.Empty;
                var gender = row[5] as string ?? string.Empty;
                var jobExperience = (byte)row[6];
                var qualification = row[7] as string ?? string.Empty;
                var subjectsString = row[8] as string ?? string.Empty;

                teachers.Add(new Teacher(id, name, lastName, familyName, gender.GetGender(), jobExperience, jobTitle, qualification, subjectsString));
            }

            return teachers;
        }

        public static IEnumerable<SubjectWithWorkload> GetSubjectsWithWorkLoads()
        {
            var data = GetData("SELECT * FROM УчПлан;");
            var subjectsWithWorkload = new List<SubjectWithWorkload>();
            foreach (var row in data)
            {
                var code = (int)row[0];
                var group = row[1] as string ?? string.Empty;
                var name = row[2] as string ?? string.Empty;
                var theory = (int)row[3];
                var ipz = (int)row[4];
                var kr = (int)row[5];
                var firstSem = (int)row[6];
                var secondSem = (int)row[7];

                subjectsWithWorkload.Add(new SubjectWithWorkload(code, group, name, theory, ipz, kr, firstSem, secondSem));
            }

            return subjectsWithWorkload;
        }

        public static IEnumerable<Specialization> GetSpecializations()
        {
            var data = GetData("SELECT * FROM Специальность;");
            var specializations = new List<Specialization>();

            foreach (var row in data)
            {
                var id = (int)row[0];
                var code = row[1] as string ?? string.Empty;
                var name = row[2] as string ?? string.Empty;
                var studyPeriod = row[3] as string ?? string.Empty;
                var qualification = (string)row[4];
                var intramural = (bool)row[5];

                specializations.Add(new Specialization(id, code, name, studyPeriod, qualification, intramural));
            }

            return specializations;
        }

        public static IEnumerable<Group> GetGroups()
        {
            var data = GetData("SELECT * FROM Группы;");
            var groups = new List<Group>();

            foreach (var row in data)
            {
                var id = (int)row[0];
                var code = (string)row[1];
                var specialization = (string)row[2];
                var amountOfStudents = (int)row[3];
                var grade = (byte)row[4];
                var teacher = (string)row[5];
                var start = (DateTime)row[6];
                var end = (DateTime)row[7];
                var isBudged = (bool)row[8];

                groups.Add(new Group(id, code, specialization, amountOfStudents, grade, teacher, start, end, isBudged));
            }

            return groups;
        }

        private static EnumerableRowCollection<DataRow> GetData(string sqlQuary)
        {
            DbConnection.myCommand = new System.Data.OleDb.OleDbCommand(sqlQuary, DbConnection.cn);
            var reader = DbConnection.myCommand.ExecuteReader();
            var dt = new DataTable();
            dt.Load(reader);
            var data = dt.AsEnumerable();
            reader.Close();

            return data;
        }
    }
}
