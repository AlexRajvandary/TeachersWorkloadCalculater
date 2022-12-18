using StudingWorkloadCalculator.ExcelWriter;
using StudingWorkloadCalculator.Models;
using System.Collections.ObjectModel;

namespace StudingWorkloadCalculator.MainVewModels
{
    public class MainViewModel : PropertyChangedNotifier
    {
        private PermissionRights permissionRights;
        private ObservableCollection<Student> students;
        private ObservableCollection<Teacher> teachers;
        private string studentsPath;

        public PermissionRights PermissionRights
        {
            get => permissionRights;
            set
            {
                permissionRights = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Student> Students
        {
            get => students;
            set
            {
                if(students != value)
                {
                    students = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StudentPath
        {
            get => studentsPath;
            set
            {
                studentsPath = value;
                GetStudentsFromExcel();
            }
        }

        public ObservableCollection<Teacher> Teachers
        {
            get => teachers;
            set
            {
                if(teachers != value)
                {
                    teachers = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TeacherPath { get; set; }

        public User User { get; private set; }

        public async void Auth()
        {

        }

        public void GetTeachersFromExcel()
        {

        }

        public void GetStudentsFromExcel()
        {
            var data = ExcelReader.ReadExcel<Student>(StudentPath);
            Students = new ObservableCollection<Student>(data);
        }
    }
}
