﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StudingWorkloadCalculator.Models
{
    public class Specialization : PropertyChangedNotifier
    {
        private string code;
        private bool intramural;
        private string name;
        private int studyPeriod;
        private ObservableCollection<Subject> subjects;
        private string qualification;

        public Specialization(string code,
                              bool intramural,
                              string name,
                              int studyPeriod,
                              string qualification) : this(code, intramural, name, studyPeriod, null, qualification) { }

        public Specialization(string code,
                              bool intramural,
                              string name,
                              int studyPeriod,
                              IEnumerable<Subject> subjects,
                              string qualification)
        {
            Code = code;
            Intramural = intramural;
            Name = name;
            StudyPeriod = studyPeriod;
            Subjects = subjects == null
                ? new ObservableCollection<Subject>()
                : new ObservableCollection<Subject>(subjects);

            Qualification = qualification;
        }

        public string Code
        {
            get => code;
            set
            {
                if (code != value)
                {
                    code = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Intramural
        {
            get => intramural;
            set
            {
                if (intramural != value)
                {
                    intramural = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public int StudyPeriod
        {
            get => studyPeriod;
            set
            {
                if (studyPeriod != value)
                {
                    studyPeriod = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Subject> Subjects
        {
            get => subjects;
            set
            {
                if (subjects != value)
                {
                    subjects = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Qualification
        {
            get => qualification;
            set
            {
                if (qualification != value)
                {
                    qualification = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}