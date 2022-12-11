using System;

namespace StudingWorkloadCalculator.Models
{
    public class Teacher : Person
    {
        private int jobExperience;
        private string jobTitle;

        public Teacher(string name, 
                       string lastName, 
                       string familyName,
                       Gender gender,
                       int jobExperiance,
                       string jobTitle) : base(familyName, name, lastName, gender)
        {
            if(jobExperiance < 0)
            {
                throw new ArgumentException("The job experience cannot be less than 0.", nameof(jobExperiance));
            }

            if (string.IsNullOrWhiteSpace(jobTitle))
            {
                throw new ArgumentException("The job title cannot be null, an empty string or a whitespace character.", nameof(jobTitle));
            }

            JobExperience = jobExperiance;
            JobTitle = jobTitle;
        }

        public int JobExperience
        {
            get => jobExperience; 
            set
            {
                if (jobExperience != value && value >= 0)
                {
                    jobExperience = value;
                    OnPropertyChanged();
                }
            }
        }

        public string JobTitle
        {
            get => jobTitle;
            set
            {
                if (jobTitle != value && !string.IsNullOrWhiteSpace(jobTitle))
                {
                    jobTitle = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
