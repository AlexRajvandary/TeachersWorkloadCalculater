using StudingWorkloadCalculator.MainVewModels;
using StudingWorkloadCalculator.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace StudingWorkloadCalculator.AccessDataBase
{
    public class AccsessDataTableReader
    {
        private const string ConnectionString = "Driver={Microsoft Access Driver (*.mdb, *.accdb)}; Dbq=C:\\Users\\sorush\\Documents\\nameOfDatabase.accdb; Uid = Admin; Pwd =; ";

        public void ReadDb(MainViewModel mainViewModel)
        {
            var teachers = new List<Teacher>();
            var students = new List<Student>();
            var groups = new List<Group>();
            var specializatons = new List<Specialization>();
            var subjects = new List<Subject>();

            var people = new List<Person>();
            OdbcCommand getTeachers = new OdbcCommand(query);

            using (OdbcConnection connection = new OdbcConnection(ConnectionString))
            {
                getteachers.Connection = connection;
                connection.Open();
                using (var reader = getteachers.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var person = new Person();
                        person.Name = reader.SafeGetString(0);
                        person.Height = reader.SafeGetDouble(1);
                        person.IsEmployed = reader.SafeGetBool(2);
                        people.Add(person);
                    }
                };
            }
            return people;
        }
    }
}
