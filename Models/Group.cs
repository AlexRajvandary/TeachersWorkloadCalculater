using System;

namespace StudingWorkloadCalculator.Models
{
    public class Group : PropertyChangedNotifier
    {
        private int amountOfStudents;
        private DateTime end;
        private int grade;
        private int id;
        private bool isBudget;
        private Specialization specialization;
        private DateTime start;
        private Person teacher;


        public int AmountOfStudents
        {
            get { return amountOfStudents; }
            set
            {
                if (amountOfStudents != value && value >= 0)
                {
                    amountOfStudents = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime End
        {
            get { return end; }
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
            get { return grade; }
            set
            {
                if (grade != value)
                {
                    grade = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Id
        {
            get { return id; }
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
            get { return isBudget; }
            set
            {
                if (isBudget != value)
                {
                    isBudget = value;
                    OnPropertyChanged();
                }
            }
        }

        public Specialization Specialization
        {
            get { return specialization; }
            set
            {
                if (specialization != value)
                {
                    specialization = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Start
        {
            get { return start; }
            set
            {
                if (start != value)
                {
                    start = value;
                    OnPropertyChanged();
                }
            }
        }

        public Person Teacher
        {
            get { return teacher; }
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
