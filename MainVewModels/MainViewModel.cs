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
        private DataPresenterViewModel<Specialization> specializationViewModel;
        private DataPresenterViewModel<Subject> subjectViewModel;
        private DataPresenterViewModel<Student> studentViewModel;
        private DataPresenterViewModel<Teacher> teacherViewModel;

        public MainViewModel()
        {
            specializationViewModel = new DataPresenterViewModel<Specialization>();
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

        public DataPresenterViewModel<Specialization> SpecializationViewModel
        {
            get => specializationViewModel;
            set
            {
                specializationViewModel = value;
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

        public  DataPresenterViewModel<Student> SpecializationsViewModel
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
