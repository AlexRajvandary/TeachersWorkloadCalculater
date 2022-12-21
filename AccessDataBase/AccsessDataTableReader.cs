using StudingWorkloadCalculator.MainVewModels;
using StudingWorkloadCalculator.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace StudingWorkloadCalculator.AccessDataBase
{
    public class AccsessDataTableReader
    {
        public static void ReadDb(MainViewModel mainViewModel, string path)
        {
            var connectionString = "Driver={Microsoft Access Driver (*.mdb, *.accdb)};" +  $"Dbq={path};" + " Uid = Admin; Pwd =; ";

            var teachers = new List<Teacher>();
            var students = new List<Student>();
            var groups = new List<Group>();
            var specializatons = new List<Specialization>();
            var subjects = new List<Subject>();

            var people = new List<Person>();
            OdbcCommand getTeachers = new OdbcCommand("SELECT * FROM Преподаватели");
            OdbcCommand getStudents = new OdbcCommand("SELECT * FROM Студенты");
            OdbcCommand getGroups = new OdbcCommand("SELECT * FROM Группы");
            OdbcCommand getSpecializations = new OdbcCommand("SELECT * FROM Специальность");
            OdbcCommand getSubjects = new OdbcCommand("SELECT * FROM Преподаватели");

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                getTeachers.Connection = connection;
                connection.Open();
                using (var reader = getTeachers.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(2);
                        var surname = reader.GetString(3);
                        var familyName = reader.GetString(4);
                        var gender = reader.GetString(6).GetGender();
                        var job = reader.GetString(5);
                        var experience = reader.GetInt32(7);

                        teachers.Add(new Teacher(name, surname, familyName, gender, experience ,job));
                    }
                };

                getStudents.Connection = connection;
                using(var reader = getStudents.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(2);
                        var surename = reader.GetString(3);
                        var familyName = reader.GetString(4);
                        var gender = reader.GetString(5);
                        var specializationCode = reader.GetString(6);
                        var groupName = reader.GetString(7);

                        students.Add(new Student(name, surename, familyName, gender.GetGender(), specializationCode, groupName));
                    }
                }
            }
        }
    }
}
