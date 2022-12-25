using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StudingWorkloadCalculator.Models
{
    public class Group : PropertyChangedNotifier
    {
        private int amountOfStudents;
        private string end;
        private int grade;
        private string id;
        private bool isBudget;
        private string start;
        private string teacher;

        public Group(int id,
                     int code,
                     string specialization,
                     int amountOfStudents,
                     int grade,
                     string teacherFullName,
                     string start,
                     string end,
                     bool isBudget
                     )
        {
            AmountOfStudents = amountOfStudents;
            End = end;
            Grade = grade;
            Id = id;
            IsBudged = isBudget;
            SpecializationName = specialization;
            Start = start;
            Teacher = teacherFullName;
        }

        public int AmountOfStudents
        {
            get => amountOfStudents;
            set
            {
                if (amountOfStudents != value && value >= 0)
                {
                    amountOfStudents = value;
                    OnPropertyChanged();
                }
            }
        }

        public string End
        {
            get => end;
            set
            {
                if (end != value)
                {
                    end = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Grade
        {
            get => grade;
            set
            {
                if (grade != value)
                {
                    grade = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Id
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsBudged
        {
            get => isBudget;
            set
            {
                if (isBudget != value)
                {
                    isBudget = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name { get; set; }

        public string SpecializationName { get; set; }

        public string Start
        {
            get => start;
            set
            {
                if (start != value)
                {
                    start = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Teacher
        {
            get => teacher;
            set
            {
                if (teacher != value)
                {
                    teacher = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
