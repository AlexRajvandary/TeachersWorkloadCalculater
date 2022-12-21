using StudingWorkloadCalculator.UserControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StudingWorkloadCalculator.Models
{
    public class Specialization : PropertyChangedNotifier, IRepository<Specialization>
    {
        private string code;
        private bool intramural;
        private string name;
        private int studyPeriod;
        private ObservableCollection<SubjectWithWorkload> subjectsWithWorkloads;
        private string qualification;

        private Specialization(string code,
                              bool intramural,
                              string name,
                              int studyPeriod,
                              IEnumerable<SubjectWithWorkload> subjectsWithWorkloads,
                              string qualification)
        {
            Code = code;
            Intramural = intramural;
            Name = name;
            StudyPeriod = studyPeriod;
            SubjectsWithWorkloads = subjectsWithWorkloads == null
                ? new ObservableCollection<SubjectWithWorkload>()
                : new ObservableCollection<SubjectWithWorkload>(subjectsWithWorkloads);

            Qualification = qualification;
        }

        [DataGridColumnGenerator("Код предмета")]
        public string Code
        {
            get => code;
            set
            {
                if (code != value)
                {
                    code = value;
                    OnPropertyChanged();
                }
            }
        }

        [DataGridColumnGenerator("Очно")]
        public bool Intramural
        {
            get => intramural;
            set
            {
                if (intramural != value)
                {
                    intramural = value;
                    OnPropertyChanged();
                }
            }
        }

        [DataGridColumnGenerator("Название")]
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        [DataGridColumnGenerator("Время обучения")]
        public int StudyPeriod
        {
            get => studyPeriod;
            set
            {
                if (studyPeriod != value)
                {
                    studyPeriod = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<SubjectWithWorkload> SubjectsWithWorkloads
        {
            get => subjectsWithWorkloads;
            set
            {
                if (subjectsWithWorkloads != value)
                {
                    subjectsWithWorkloads = value;
                    OnPropertyChanged();
                }
            }
        }

        [DataGridColumnGenerator("Квалификация")]
        public string Qualification
        {
            get => qualification;
            set
            {
                if (qualification != value)
                {
                    qualification = value;
                    OnPropertyChanged();
                }
            }
        }

        public static Specialization GetSpecialization(string code,
                                                       bool intramural,
                                                       string name,
                                                       int studyPeriod,
                                                       string qualification)
        {
            return GetSpecialization(code, intramural, name, studyPeriod, null, qualification);
        }

        public static Specialization GetSpecialization(string code,
                                                       bool intramural,
                                                       string name,
                                                       int studyPeriod,
                                                       IEnumerable<SubjectWithWorkload> subjectsWithWorkloads,
                                                       string qualification)
        {
            if (IRepository<Specialization>.Instances.TryGetValue(code, out var specialization))
            {
                specialization.Intramural = intramural;
                specialization.Name = name;
                specialization.StudyPeriod = studyPeriod;
                specialization.SubjectsWithWorkloads = new ObservableCollection<SubjectWithWorkload>(subjectsWithWorkloads);
                specialization.Qualification = qualification;
                return specialization;
            }
            else
            {
                var newSpecilization = new Specialization(code, intramural, name, studyPeriod, subjectsWithWorkloads, qualification);
                IRepository<Specialization>.Instances.Add(code, newSpecilization);
                return newSpecilization;
            }
        }
    }
}
