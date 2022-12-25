using StudingWorkloadCalculator.UserControls;

namespace StudingWorkloadCalculator.Models
{
    public class SubjectWithWorkload : PropertyChangedNotifier
    {
        private string group;
        private string name;
        private int code;
        private int secondSemester;
        private int firstSemester;
        private int kr;
        private int ipz;
        private int total;
        private int theory;

        public SubjectWithWorkload(int code, string group, string name, int theory, int ipz, int kr, int firstSemestr, int secondSemestr)
        {
            Group = group;
            Name = name;
            Code = code;
            FirstSemester = firstSemestr;
            SecondSemester = secondSemestr;
            Kr = kr;
            Ipz = ipz;
            Theory = theory;
        }

        [DataGridColumnGenerator("Группа")]
        public string Group
        {
            get => group;
            set
            {
                if(group != value)
                {
                    group = value;
                    OnPropertyChanged();
                }
            }
        }

        [DataGridColumnGenerator("Код предмета")]
        public int Code
        {
            get => code;
            set
            {
                if (code != value && value >= 0)
                {
                    code = value;
                    OnPropertyChanged();
                }
            }
        }

        [DataGridColumnGenerator("Дисциплина")]
        public string Name
        {
            get => name;
            set
            {
                if (name != value && !string.IsNullOrWhiteSpace(value))
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        [DataGridColumnGenerator("Теория")]
        public int Theory
        {
            get => theory;
            set
            {
                if (theory != value && value >= 0)
                {
                    theory = value;
                    OnPropertyChanged();
                    RecalculateTotal();
                }
            }
        }

        [DataGridColumnGenerator("Всего")]
        public int Total
        {
            get => total;
            set
            {
                if (total != value)
                {
                    total = value;
                    OnPropertyChanged();
                }
            }
        }

        [DataGridColumnGenerator(true)]
        public int Ipz
        {
            get => ipz;
            set
            {
                if (ipz != value && value >= 0)
                {
                    ipz = value;
                    OnPropertyChanged();
                    RecalculateTotal();
                }
            }
        }

        [DataGridColumnGenerator(true)]
        public int Kr
        {
            get => kr;
            set
            {
                if (kr != value && value >= 0)
                {
                    kr = value;
                    OnPropertyChanged();
                    RecalculateTotal();
                }
            }
        }

        [DataGridColumnGenerator("Первый семестр")]
        public int FirstSemester
        {
            get => firstSemester;
            set
            {
                if (firstSemester != value && value >= 0)
                {
                    firstSemester = value;
                    OnPropertyChanged();
                    RecalculateTotal();
                }
            }
        }

        [DataGridColumnGenerator("Второй семестр")]
        public int SecondSemester
        {
            get => secondSemester;
            set
            {
                if (secondSemester != value && value >= 0)
                {
                    secondSemester = value;
                    OnPropertyChanged();
                    RecalculateTotal();
                }
            }
        }

        private void RecalculateTotal()
        {
            Total = Theory + Ipz + Kr + FirstSemester + SecondSemester;
        }
    }
}
