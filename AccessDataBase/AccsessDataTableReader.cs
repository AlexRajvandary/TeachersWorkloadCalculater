using StudingWorkloadCalculator.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Xml.Linq;

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

        public static IEnumerable<User> GetUsers()
        {
            var data = GetData("SELECT * FROM Пользователь;");
            var users = new List<User>();

            foreach (var row in data)
            {
                var id = (int)row[0];
                var name = (string)row[1];
                var password = (string)row[2];
                var specialRights = (bool)row[3];

                users.Add(new User(id, name, password, specialRights));
            }

            return users;
        }

        public static void SaveStudent(Student student, bool newItem)
        {
            string quary;

            if (newItem)
            {

            }
            else
            {

            }

        }

        public static void SaveTeacher(Teacher teacher, bool newItem)
        {
            string quary;

            if (newItem)
            {

            }
            else
            {

            }

        }

        public static void SaveSubjectWithWorkLoad(SubjectWithWorkload subjectWithWorkload, bool newItem)
        {
            string quary;

            if (newItem)
            {
                quary = "INSERT INTO УчПлан ( [grup], [nazvani], [teoria], [lpz], [kp], " +
                  $"[kol_1sem], [kol_2sem],[max]) VALUES ({subjectWithWorkload.Group}, {subjectWithWorkload.Name}," +
                  $" {subjectWithWorkload.Theory}, {subjectWithWorkload.Ipz}, {subjectWithWorkload.Kr}, {subjectWithWorkload.FirstSemester}, {subjectWithWorkload.SecondSemester}, {subjectWithWorkload.Total})";
            }
            else
            {
                quary = $"UPDATE УчПлан SET [grup]={subjectWithWorkload.Group}, [nazvani]={subjectWithWorkload.Name}, [teoria]={subjectWithWorkload.Theory}, [lpz]=@{subjectWithWorkload.Ipz}, [kp]={subjectWithWorkload.Kr}, " +
               $"[kol_1sem]={subjectWithWorkload.FirstSemester}, [kol_2sem]={subjectWithWorkload.SecondSemester}, [max]={subjectWithWorkload.Total} WHERE kod_discip = " + subjectWithWorkload.Code;
            }

            SaveChange(quary);
        }

        public static void SaveSpecialization(Specialization specialiation, bool newItem)
        {
            string quary;

            if (newItem)
            {

            }
            else
            {

            }
        }

        public static void SaveGroup(Group group, bool newItem)
        {
            string quary;

            if (newItem)
            {

            }
            else
            {

            }
        }

        public static void SaveUser(User user, bool newItem)
        {
            string quary;

            if (newItem)
            {

            }
            else
            {

            }
        }

        private static void SaveChange(string quary)
        {
            DbConnection.myCommand = new System.Data.OleDb.OleDbCommand();
            DbConnection.myCommand.Connection = DbConnection.cn;
            DbConnection.myCommand.CommandText = quary;

            if (DbConnection.myCommand.Connection.State == ConnectionState.Open)
            {
                try
                {
                    DbConnection.myCommand.ExecuteNonQuery();
                    DbConnection.myCommand.Connection.Close();
                    Trace.WriteLine("Data updated");
                }
                catch
                {
                    DbConnection.myCommand.Connection.Close();
                    Trace.WriteLine("Error occured during data table updating.");
                }
            }
            else
            {
                Trace.WriteLine("Error occured during data table updating.");
            }
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
