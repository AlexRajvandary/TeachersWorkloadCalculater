﻿using StudingWorkloadCalculator.UserControls;
using System.Collections.ObjectModel;
using System.Linq;

namespace StudingWorkloadCalculator.Models
{
    public class Teacher : Person
    {
        private int jobExperience;
        private string jobTitle;
        private string[] subject;
        private ObservableCollection<SubjectWithWorkload> subjects;
        private string subjectsToString;
        private string qualification;

        public Teacher(): base("Имя", "Фамилия", "Отчесвто", Gender.Undefined)
        {
            JobTitle = "Должность";
            Qualification = "Квалификация";
            SubjectsToString = "Предметы";
        }

        public Teacher(int id,
                       string name,
                       string lastName,
                       string familyName,
                       Gender gender,
                       int jobExperiance,
                       string jobTitle,
                       string qualification,
                       string subjects) : base(name, lastName, familyName, gender)
        {
            Id = id;
            JobExperience = jobExperiance;
            JobTitle = jobTitle;
            Qualification = qualification;
            Subject = subjects.Contains(',') || subjects.Contains(';') ? subjects.Split(new char[] { ',', ';' }, System.StringSplitOptions.RemoveEmptyEntries) : new string[] { subjects };
            SubjectsToString = subjects;
        }

        [DataGridColumnGenerator("Стаж")]
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

        public int Id { get; }

        [DataGridColumnGenerator("Специальность")]
        public string JobTitle
        {
            get => jobTitle;
            set
            {
                if (jobTitle != value)
                {
                    jobTitle = value;
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
                qualification = value;
                OnPropertyChanged();
            }
        }

        public string[] Subject
        {
            get => subject;
            set
            {
                subject = value;
                OnPropertyChanged();
            }
        }

        [DataGridColumnGenerator("Предметы")]
        public string SubjectsToString
        {
            get => subjectsToString;
            set
            {
                if (subjectsToString != value)
                {
                    subjectsToString = value;
                    OnPropertyChanged();
                }
            }
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {FamilyName}";
        }
    }
}
