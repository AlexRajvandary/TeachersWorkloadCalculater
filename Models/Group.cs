﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;

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
        private ObservableCollection<Student> students;
        private Teacher teacher;

        public Group(int amountOfStudents,
                     DateTime end,
                     int grade,
                     int id,
                     bool isBudget,
                     Specialization specialization,
                     DateTime start,
                     IEnumerable<Student> students,
                     Teacher teacher)
        {
            if (amountOfStudents < 0)
            {
                throw new ArgumentException("The amount of students cannot be less than zero.", nameof(amountOfStudents));
            }

            if (start > end)
            {
                throw new ArgumentException("The start date cannot be later than the end date.", nameof(start));
            }

            if (grade < 1)
            {
                throw new ArgumentException("The grade cannot be less than 1.", nameof(grade));
            }

            ArgumentNullException.ThrowIfNull(specialization);
            ArgumentNullException.ThrowIfNull(teacher);

            AmountOfStudents = amountOfStudents;
            End = end;
            Grade = grade;
            Id = id;
            IsBudged = isBudget;
            Specialization = specialization;
            Start = start;
            Students = new ObservableCollection<Student>(students);
            Teacher = teacher;
        }

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

        public ObservableCollection<Student> Students
        {
            get { return students; }
            set
            {
                if (students != value)
                {
                    students = value;
                    OnPropertyChanged();
                }
            }
        }

        public Teacher Teacher
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
