using StudingWorkloadCalculator.UserControls;
using System;

namespace StudingWorkloadCalculator.Models
{
    public class Person : PropertyChangedNotifier
    {
        private string familyName;
        private string firstName;
        private string lastName;
        private Gender gender;

        public Person()
        {

        }

        public Person(string firstName,
                      string lastName,
                      string familyName,
                      Gender gender)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("The firstname cannot be null, an empty string or a whitespace character.", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("The lastname cannot be null, an empty string or a whitespace character.", nameof(lastName));
            }

            ArgumentNullException.ThrowIfNull(gender);

            FamilyName = familyName;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
        }

        [DataGridColumnGenerator("Фамилия")]
        public string LastName
        {
            get => lastName;
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        [DataGridColumnGenerator("Имя")]
        public string FirstName
        {
            get => firstName;
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        [DataGridColumnGenerator("Отчество")]
        public string FamilyName
        {
            get => familyName;
            set
            {
                if (familyName != value)
                {
                    familyName = value;
                    OnPropertyChanged();
                }
            }
        }

        [DataGridColumnGenerator("Пол")]
        public Gender Gender
        {
            get => gender;
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
