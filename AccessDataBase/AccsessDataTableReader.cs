using StudingWorkloadCalculator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Diagnostics;
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

                var teacher = new Teacher(id, name, lastName, familyName, gender.GetGender(), jobExperience, jobTitle, qualification, subjectsString);
                teachers.Add(teacher);
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

        public static void SaveTeacher(Teacher teacher, bool newItem)
        {
            OleDbCommand dbCommand = null;

            if (newItem)
            {
                dbCommand = new OleDbCommand("INSERT INTO Преподаватели ([fam],[imya],[otch],[dolgnost],[pol],[ped_stag],[kvalifikacia],[spec_dip]) VALUES (?,?,?,?,?,?,?,?)", DbConnection.cn);
                dbCommand.Parameters.Add("@fam", OleDbType.VarChar, 255).Value = teacher.LastName;
                dbCommand.Parameters.Add("@imya", OleDbType.VarChar, 255).Value = teacher.FirstName;
                dbCommand.Parameters.Add("@otch", OleDbType.VarChar, 255).Value = teacher.FamilyName;
                dbCommand.Parameters.Add("@dolgnost", OleDbType.VarChar, 255).Value = teacher.JobTitle;
                dbCommand.Parameters.Add("@pol", OleDbType.VarChar, 255).Value = teacher.Gender;
                dbCommand.Parameters.Add("@ped_stag", OleDbType.Integer, 255).Value = teacher.JobExperience;
                dbCommand.Parameters.Add("@kvalifikacia", OleDbType.VarChar, 255).Value = teacher.Qualification;
                dbCommand.Parameters.Add("@spec_dip", OleDbType.VarChar, 255).Value = teacher.SubjectsToString;
            }
            else
            {
                
            }

            SaveChange(dbCommand);
        }

        public static void SaveSubjectWithWorkLoad(SubjectWithWorkload subjectWithWorkload, bool newItem)
        {
            OleDbCommand dbCommand = null;

            if (newItem)
            {
                dbCommand = new OleDbCommand("INSERT INTO УчПлан ([group],[nazvani],[theoria],[lpz],[kp],[kol_1sem],[kol_2sem],[max]) VALUES (?,?,?,?,?,?,?,?)", DbConnection.cn);
                dbCommand.Parameters.Add("@group", OleDbType.VarChar, 255).Value = subjectWithWorkload.Group;
                dbCommand.Parameters.Add("@nazvani", OleDbType.VarChar, 255).Value = subjectWithWorkload.Name;
                dbCommand.Parameters.Add("@theoria", OleDbType.Integer, 255).Value = subjectWithWorkload.Theory;
                dbCommand.Parameters.Add("@lpz", OleDbType.Integer, 255).Value = subjectWithWorkload.Ipz;
                dbCommand.Parameters.Add("@kp", OleDbType.Integer, 255).Value = subjectWithWorkload.Kr;
                dbCommand.Parameters.Add("@kol_1sem", OleDbType.Integer, 255).Value = subjectWithWorkload.FirstSemester;
                dbCommand.Parameters.Add("@kol_2sem", OleDbType.Integer, 255).Value = subjectWithWorkload.SecondSemester;
                dbCommand.Parameters.Add("@max", OleDbType.Integer, 255).Value = subjectWithWorkload.Total;
            }
            else
            {
              
            }

            SaveChange(dbCommand);
        }

        public static void SaveSpecialization(Specialization specialiation, bool newItem)
        {
            OleDbCommand dbCommand = null;

            if (newItem)
            {
                dbCommand = new OleDbCommand("INSERT INTO Специальность ([kod_spec],[naimenov],[srok_obuch],[kvalifik],[ochnaya]) VALUES (?,?,?,?,?)", DbConnection.cn);
                dbCommand.Parameters.Add("@kod_spec", OleDbType.VarChar, 255).Value = specialiation.Code;
                dbCommand.Parameters.Add("@naimenov", OleDbType.VarChar, 255).Value = specialiation.Name;
                dbCommand.Parameters.Add("@srok_obuch", OleDbType.Integer, 255).Value = specialiation.StudyPeriod;
                dbCommand.Parameters.Add("@kvalifik", OleDbType.VarChar, 255).Value = specialiation.Qualification;
                dbCommand.Parameters.Add("@ochnaya", OleDbType.VarBinary, 255).Value = specialiation.Intramural;
            }
            else
            {

            }

            SaveChange(dbCommand);
        }

        public static void SaveGroup(Group group, bool newItem)
        {
            OleDbCommand dbCommand = null;

            if (newItem)
            {
                dbCommand = new OleDbCommand("INSERT INTO Группы ([kod_grup],[spec],[kolvo_stud],[kurs],[kl_r],[god_postup],[god_okonch], [budget]) VALUES (?,?,?,?,?,?,?,?)", DbConnection.cn);
                dbCommand.Parameters.Add("@kod_grup", OleDbType.VarChar, 255).Value = group.Code;
                dbCommand.Parameters.Add("@spec", OleDbType.VarChar, 255).Value = group.SpecializationName;
                dbCommand.Parameters.Add("@kolvo_stud", OleDbType.Integer, 255).Value = group.AmountOfStudents;
                dbCommand.Parameters.Add("@kurs", OleDbType.Integer, 255).Value = group.Grade;
                dbCommand.Parameters.Add("@kl_r", OleDbType.VarChar, 255).Value = group.Teacher;
                dbCommand.Parameters.Add("@god_postup", OleDbType.Date, 255).Value = group.Start;
                dbCommand.Parameters.Add("@god_okonch", OleDbType.Date, 255).Value = group.End;
                dbCommand.Parameters.Add("@budget", OleDbType.VarBinary, 255).Value = group.IsBudged;
            }
            else
            {

            }

            SaveChange(dbCommand);
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

        private static void SaveChange(OleDbCommand dbCommand)
        {
            DbConnection.myCommand = dbCommand;

            if (DbConnection.myCommand.Connection.State != ConnectionState.Open)
            {
                DbConnection.myCommand.Connection.Open();
            }

            try
            {
                var r = DbConnection.myCommand.ExecuteNonQuery();
                DbConnection.myCommand.Connection.Close();
                Trace.WriteLine("Data updated");
            }
            catch (Exception ex)
            {
                DbConnection.myCommand.Connection.Close();
                Trace.WriteLine($"Error occured during data table updating.\n {ex}");
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
