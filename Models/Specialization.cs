namespace StudingWorkloadCalculator.Models
{
    public class Specialization : PropertyChangedNotifier
    {
        private string code;
        private string name;
        private int studyPeriod;
        private string qualification;

        public Specialization() { }

        public Specialization(string code, string name, int studyPeriod, string qualification)
        {
            Code = code;
            Name = name;
            StudyPeriod = studyPeriod;
            Qualification = qualification;
        }

        public string Code
        {
            get { return code; }
            set
            {
                if (code != value)
                {
                    code = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public int StudyPeriod
        {
            get { return studyPeriod; }
            set
            {
                if (studyPeriod != value)
                {
                    studyPeriod = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Qualification
        {
            get { return qualification; }
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
