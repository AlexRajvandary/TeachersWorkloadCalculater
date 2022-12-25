using StudingWorkloadCalculator.AccessDataBase;
using StudingWorkloadCalculator.Models;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;

namespace StudingWorkloadCalculator.MainVewModels
{
    public class MainViewModel : PropertyChangedNotifier
    {
        private PermissionRights permissionRights;
        private DataPresenterViewModel<Specialization> specializationsViewModel;
        private DataPresenterViewModel<SubjectWithWorkload> subjectViewModel;
        private DataPresenterViewModel<Student> studentViewModel;
        private DataPresenterViewModel<Teacher> teacherViewModel;
        private DataPresenterViewModel<Group> groupsViewModel;
        private double rate;
        private TeachersWorkload teachersWorkload;

        public MainViewModel()
        {
            specializationsViewModel = new DataPresenterViewModel<Specialization>();
            subjectViewModel = new DataPresenterViewModel<SubjectWithWorkload>();
            studentViewModel = new DataPresenterViewModel<Student>();
            teacherViewModel = new DataPresenterViewModel<Teacher>();
            groupsViewModel = new DataPresenterViewModel<Group>();
        }

        public PermissionRights PermissionRights
        {
            get => permissionRights;
            set
            {
                permissionRights = value;
                OnPropertyChanged();
            }
        }

        public DataPresenterViewModel<Specialization> SpecializationsViewModel
        {
            get => specializationsViewModel;
            set
            {
                specializationsViewModel = value;
                OnPropertyChanged();
            }
        }

        public DataPresenterViewModel<SubjectWithWorkload> SubjectViewModel
        {
            get => subjectViewModel;
            set
            {
                subjectViewModel = value;
                OnPropertyChanged();
            }
        }

        public DataPresenterViewModel<Student> StudentsViewModel
        {
            get => studentViewModel;
            set
            {
                studentViewModel = value;
                OnPropertyChanged();
            }
        }

        public DataPresenterViewModel<Teacher> TeachersViewModel
        {
            get => teacherViewModel;
            set
            {
                teacherViewModel = value;
                OnPropertyChanged();
            }
        }

        public DataPresenterViewModel<Group> GroupsViewModel
        {
            get => groupsViewModel;
            set
            {
                groupsViewModel = value;
                OnPropertyChanged();
            }
        }

        public double Rate
        {
            get => rate;
            set
            {
                if (rate != value)
                {
                    rate = value;
                    OnPropertyChanged();
                }
            }
        }

        public TeachersWorkload TeachersWorkload
        {
            get => teachersWorkload;
            set
            {
                if (teachersWorkload != value)
                {
                    teachersWorkload = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Auth(string login, string password)
        {
            DbConnection.OpenConnection();
            string strSQL = "SELECT * FROM Пользователь WHERE Логин = '" + login +"'" +
                   " AND Пароль = '" + password + "'";
            DbConnection.myCommand = new OleDbCommand(strSQL, DbConnection.cn);
            object value = DbConnection.myCommand.ExecuteScalar();
            return value != null;
        }

        public TeachersWorkload? CalculateWorkLoad(IEnumerable<SubjectWithWorkload> subjects)
        {
            if (TeachersViewModel.SelectedItem != null)
            {
                var teachersWorkLoad = new Workload(0, 0, 0, 0, 0);
                foreach (var subject in subjects)
                {
                    teachersWorkLoad += new Workload(subject.Theory, subject.Ipz, subject.Kr, subject.FirstSemester, subject.SecondSemester);
                }

                return new TeachersWorkload(TeachersViewModel.SelectedItem, teachersWorkLoad, Rate);
            }
            else
            {
                return null;
            }
        }
    }
}
