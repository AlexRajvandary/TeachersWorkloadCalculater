namespace StudingWorkloadCalculator.Models
{
    public class Teacher : Person
    {
        private int experience;
        private string job;

        public Teacher(string name, string lastName, string familyName, Gender gender, int expiriance, string job) : base(familyName, name, lastName, gender)
        {
            Experience = expiriance;
            Job = job;
        }

        public int Experience
        {
            get { return experience; }
            set
            {
                if (experience != value)
                {
                    experience = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Job
        {
            get { return job; }
            set
            {
                if (job != value)
                {
                    job = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
