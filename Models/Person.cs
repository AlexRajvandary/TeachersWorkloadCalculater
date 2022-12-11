namespace StudingWorkloadCalculator.Models
{
    public class Person : PropertyChangedNotifier
    {
        private string familyName;
        private string firstName;
        private string lastName;
        private Gender gender;

        public Person() { }

        public Person(string familyName, string firstName, string lastName, Gender gender)
        {
            FamilyName = familyName;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
        }

        public string FamilyName
        {
            get { return familyName; }
            set
            {
                if (familyName != value)
                {
                    familyName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public Gender Gender
        {
            get { return gender; }
            set
            {
                if (gender != value)
                {
                    gender = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
