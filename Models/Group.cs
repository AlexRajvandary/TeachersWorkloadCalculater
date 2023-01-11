using StudingWorkloadCalculator.UserControls;
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
        private DateTime start;
        private string teacher;

        public Group()
        {
            Code = "Код группы";
            SpecializationName = "Специальность";
            Teacher = "ФИО Учителя";
        }

        public Group(int id,
                     string code,
                     string specialization,
                     int amountOfStudents,
                     int grade,
                     string teacherFullName,
                     DateTime start,
                     DateTime end,
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
            Code = code;
        }

        [DataGridColumnGenerator("Количество студентов")]
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

        [DataGridColumnGenerator("Конец обучения")]
        public DateTime End
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

        [DataGridColumnGenerator("Курс")]
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

        public int Id
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

        [DataGridColumnGenerator("Код специализации")]
        public string Code { get; }

        [DataGridColumnGenerator("Бюджет")]
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

        [DataGridColumnGenerator("Специализация")]
        public string SpecializationName { get;}

        [DataGridColumnGenerator("Начало обучения")]
        public DateTime Start
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

        [DataGridColumnGenerator("Классный руководитель")]
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
