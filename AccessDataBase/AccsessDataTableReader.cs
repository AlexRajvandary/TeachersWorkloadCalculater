using StudingWorkloadCalculator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

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
                dbCommand = new OleDbCommand("INSERT INTO Преподаватели ([fam],[imya],[otch],[dolgnost],[pol],[ped_stag],[kvalifikacia],[spec_dip]) VALUES (@fam, @imya, @otch, @dolgnost, @pol, @ped_stag, @kvalifikacia, @spec_dip)", DbConnection.cn);
                dbCommand.Parameters.AddWithValue("@fam", teacher.LastName);
                dbCommand.Parameters.AddWithValue("@imya", teacher.FirstName);
                dbCommand.Parameters.AddWithValue("@otch", teacher.FamilyName);
                dbCommand.Parameters.AddWithValue("@dolgnost", teacher.JobTitle);
                dbCommand.Parameters.AddWithValue("@pol", teacher.Gender.ToString());
                dbCommand.Parameters.AddWithValue("@ped_stag", teacher.JobExperience);
                dbCommand.Parameters.AddWithValue("@kvalifikacia", teacher.Qualification);
                dbCommand.Parameters.AddWithValue("@spec_dip", teacher.SubjectsToString);
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
                dbCommand = new OleDbCommand("INSERT INTO УчПлан ([grup],[nazvani],[teoria],[lpz],[kp],[kol_1sem],[kol_2sem],[max]) VALUES (?,?,?,?,?,?,?,?)", DbConnection.cn);
                dbCommand.Parameters.AddWithValue("@grup", subjectWithWorkload.Group);
                dbCommand.Parameters.AddWithValue("@nazvani", subjectWithWorkload.Name);
                dbCommand.Parameters.AddWithValue("@teoria", subjectWithWorkload.Theory);
                dbCommand.Parameters.AddWithValue("@lpz", subjectWithWorkload.Ipz);
                dbCommand.Parameters.AddWithValue("@kp", subjectWithWorkload.Kr);
                dbCommand.Parameters.AddWithValue("@kol_1sem", subjectWithWorkload.FirstSemester);
                dbCommand.Parameters.AddWithValue("@kol_2sem", subjectWithWorkload.SecondSemester);
                dbCommand.Parameters.AddWithValue("@max", subjectWithWorkload.Total);
            }
            else
            {
                 dbCommand = new OleDbCommand(@"UPDATE УчПлан
                                                    SET [grup] = @grup,
                                                        [nazvani] = @nazvani,
                                                        [teoria] = @teoria,
                                                        [lpz] = @lpz,
                                                        [kp] = @kp,
                                                        [kol_1sem] = @kol_1sem,
                                                        [kol_2sem] = @kol_2sem,
                                                        [max] = @max
                                                    WHERE kod_discip = " + subjectWithWorkload.Code, DbConnection.cn);

                dbCommand.Parameters.AddWithValue("@grup", subjectWithWorkload.Group);
                dbCommand.Parameters.AddWithValue("@nazvani", subjectWithWorkload.Name);
                dbCommand.Parameters.AddWithValue("@teoria", subjectWithWorkload.Theory );
                dbCommand.Parameters.AddWithValue("@lpz", subjectWithWorkload.Ipz);
                dbCommand.Parameters.AddWithValue("@kp", subjectWithWorkload.Kr);
                dbCommand.Parameters.AddWithValue("@kol_1sem", subjectWithWorkload.FirstSemester);
                dbCommand.Parameters.AddWithValue("@kol_2sem", subjectWithWorkload.SecondSemester);
                dbCommand.Parameters.AddWithValue("@max", subjectWithWorkload.Total);
            }

            SaveChange(dbCommand);
        }

        public static void SaveSpecialization(Specialization specialiation, bool newItem)
        {
            OleDbCommand dbCommand = null;

            if (newItem)
            {
                dbCommand = new OleDbCommand("INSERT INTO Специальность ([kod_spec],[naimenov],[srok_obuch],[kvalifik],[ochnaya]) VALUES (@kod_spec, @naimenov, @srok_obuch, @kvalifik, @ochnaya)", DbConnection.cn);
                dbCommand.Parameters.AddWithValue("@kod_spec", specialiation.Code);
                dbCommand.Parameters.AddWithValue("@naimenov", specialiation.Name);
                dbCommand.Parameters.AddWithValue("@srok_obuch", specialiation.StudyPeriod);
                dbCommand.Parameters.AddWithValue("@kvalifik", specialiation.Qualification);
                dbCommand.Parameters.AddWithValue("@ochnaya", specialiation.Intramural ? 1 : 0);
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
                dbCommand = new OleDbCommand("INSERT INTO Группы ([kod_grup],[spec],[kolvo_stud],[kurs],[kl_r],[god_postup],[god_okonch], [budget]) VALUES (@kod_grup, @spec, @kolvo_stud, @kurs, @kl_r, @god_postup, @god_okonch, @budget)", DbConnection.cn);
                dbCommand.Parameters.AddWithValue("@kod_grup", group.Code);
                dbCommand.Parameters.AddWithValue("@spec", group.SpecializationName);
                dbCommand.Parameters.AddWithValue("@kolvo_stud", group.AmountOfStudents);
                dbCommand.Parameters.AddWithValue("@kurs", group.Grade);
                dbCommand.Parameters.AddWithValue("@kl_r", group.Teacher);
                dbCommand.Parameters.AddWithValue("@god_postup", group.Start.ToString("dd/MM/yy"));
                dbCommand.Parameters.AddWithValue("@god_okonch", group.End.ToString("dd/MM/yy"));
                dbCommand.Parameters.AddWithValue("@budget", group.IsBudged ? 1 : 0);
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
