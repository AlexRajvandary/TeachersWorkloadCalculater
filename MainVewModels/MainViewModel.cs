using StudingWorkloadCalculator.AccessDataBase;
using StudingWorkloadCalculator.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System;

namespace StudingWorkloadCalculator.MainVewModels
{
    public class MainViewModel : PropertyChangedNotifier
    {
        private DataPresenterViewModel<Specialization> specializationsViewModel;
        private DataPresenterViewModel<SubjectWithWorkload> subjectViewModel;
        private DataPresenterViewModel<Teacher> teacherViewModel;
        private DataPresenterViewModel<Group> groupsViewModel;
        private double rate;
        private TeachersWorkloadViewModel teachersWorkload;
        private User user;

        public class Subjectcompaprer : IEqualityComparer<string>
        {
            public bool Equals(string? x, string? y)
            {
                return x != null && y != null && x.ToLower().Contains(y.ToLower()) || y.ToLower().Contains(x.ToLower());
            }

            public int GetHashCode([DisallowNull] string obj)
            {
                return HashCode.Combine(obj, obj);
            }
        }

        public MainViewModel()
        {
            specializationsViewModel = new DataPresenterViewModel<Specialization>(AccsessDataTableReader.SaveSpecialization, AccsessDataTableReader.DeleteSpecialization);
            subjectViewModel = new DataPresenterViewModel<SubjectWithWorkload>(AccsessDataTableReader.SaveSubjectWithWorkLoad, AccsessDataTableReader.DeleteSubjectWithWorkLoad);
            teacherViewModel = new DataPresenterViewModel<Teacher>(AccsessDataTableReader.SaveTeacher, AccsessDataTableReader.DeleteTeacher);
            groupsViewModel = new DataPresenterViewModel<Group>(AccsessDataTableReader.SaveGroup, AccsessDataTableReader.DeleteGroup);
        }

        public User User
        {
            get => user;
            set
            {
                user = value;
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

        public TeachersWorkloadViewModel TeachersWorkload
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

        public TeachersWorkloadViewModel? CalculateWorkLoad()
        {
            var teacher = TeachersViewModel.SelectedItem;
            if (teacher != null)
            {
                var teachersWorkLoad = new Workload(0, 0, 0, 0, 0);
                var subjects = SubjectViewModel.Data.Where(subject => teacher.Subject.Contains(subject.Name, new Subjectcompaprer()));
                foreach (var subject in subjects)
                {
                    teachersWorkLoad += new Workload(subject.Theory, subject.Ipz, subject.Kr, subject.FirstSemester, subject.SecondSemester);
                }

                TeachersWorkload = new TeachersWorkloadViewModel(TeachersViewModel.SelectedItem, teachersWorkLoad, subjects, Rate);
                return TeachersWorkload;
            }
            else
            {
                return null;
            }
        }
    }
}
