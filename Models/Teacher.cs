namespace StudingWorkloadCalculator.Models
{
    public class Teacher : Person
    {
        private int jobExperience;
        private string jobTitle;

        public Teacher(string name, string lastName, string familyName, Gender gender, int jobExperiance, string jobTitle) : base(familyName, name, lastName, gender)
        {
            JobExperience = jobExperiance;
            JobTitle = jobTitle;
        }

        public int JobExperience
        {
            get { return jobExperience; }
            set
            {
                if (jobExperience != value)
                {
                    jobExperience = value;
                    OnPropertyChanged();
                }
            }
        }

        public string JobTitle
        {
            get { return jobTitle; }
            set
            {
                if (jobTitle != value)
                {
                    jobTitle = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
