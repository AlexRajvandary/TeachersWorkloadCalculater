using StudingWorkloadCalculator.ExcelWriter;
using StudingWorkloadCalculator.Models;
using System.Collections.ObjectModel;
using System.Printing;
using System.Security.Policy;

namespace StudingWorkloadCalculator.MainVewModels
{
    public class MainViewModel : PropertyChangedNotifier
    {
        private PermissionRights permissionRights;
        private DataPresenterViewModel<Specialization> specializationsViewModel;
        private DataPresenterViewModel<Subject> subjectViewModel;
        private DataPresenterViewModel<Student> studentViewModel;
        private DataPresenterViewModel<Teacher> teacherViewModel;

        public MainViewModel()
        {
            specializationsViewModel = new DataPresenterViewModel<Specialization>();
            subjectViewModel = new DataPresenterViewModel<Subject>();
            studentViewModel = new DataPresenterViewModel<Student>();
            teacherViewModel = new DataPresenterViewModel<Teacher>();
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

        public DataPresenterViewModel<Subject> SubjectViewModel
        {
            get => subjectViewModel;
            set
            {
                subjectViewModel = value;
                OnPropertyChanged();
            }
        }

        public  DataPresenterViewModel<Student> StudentsViewModel
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

        public async void Auth()
        {

        }
    }
}
