namespace StudingWorkloadCalculator.Models
{
    public class Student : Person
    {
        private Group group;
        private Specialization specialization;

        public Student(string firstName,
                       string lastName,
                       string familyName,
                       Gender gender,
                       Specialization specialization,
                       Group group) : base(firstName, lastName, familyName, gender)
        {
            Specialization = specialization;
            Group = group;
        }

        public Specialization Specialization
        {
            get => specialization; 
            set
            {
                if (specialization != value)
                {
                    specialization = value;
                    OnPropertyChanged();
                }
            }
        }

        public Group Group
        {
            get => group;
            set
            {
                if (group != value)
                {
                    group = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
