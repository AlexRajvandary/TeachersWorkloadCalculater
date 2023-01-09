using StudingWorkloadCalculator.UserControls;

namespace StudingWorkloadCalculator.Models
{
    public class Specialization : PropertyChangedNotifier
    {
        private string code;
        private bool intramural;
        private string name;
        private string studyPeriod;
        private string qualification;

        public Specialization()
        {

        }

        public Specialization(int id,
                              string code,
                              string name,
                              string studyPeriod,
                              string qualification,
                              bool intramural)
        {
            Id = id;
            Code = code;
            Intramural = intramural;
            Name = name;
            StudyPeriod = studyPeriod;
            Qualification = qualification;
        }

        public int Id
        {
            get;
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
        public string StudyPeriod
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
    }
}
