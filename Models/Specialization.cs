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

        public Specialization() { }

        public Specialization(string code,
                              bool intramural,
                              string name,
                              int studyPeriod,
                              IEnumerable<Subject> subjects,
                              string qualification)
        {
            Code = code;
            Name = name;
            StudyPeriod = studyPeriod;
            Subjects = new ObservableCollection<Subject>(subjects);
            Qualification = qualification;
        }

        public string Code
        {
            get { return code; }
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
            get { return intramural; }
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
            get { return name; }
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
            get { return studyPeriod; }
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
            get { return qualification; }
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
