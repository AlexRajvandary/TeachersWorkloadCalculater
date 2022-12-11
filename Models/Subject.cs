using System;

namespace StudingWorkloadCalculator.Models
{
    public class Subject : PropertyChangedNotifier, IEquatable<Subject>
    {
        private int code;
        private string name;

        public Subject(int code, string name)
        {
            if(code < 0)
            {
                throw new ArgumentException("The code of the subject cannot be less than zero", nameof(code));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The name of the subject cannot be null or a whitespace string.", nameof(name));
            }

            this.code = code;
            this.name = name;
        }

        public int Code
        {
            get => code;
            set
            {
                if (code != value && value >= 0)
                {
                    code = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if(name != value && !string.IsNullOrWhiteSpace(value))
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Equals(Subject? other)
        {
            return other != null && (ReferenceEquals(this, other) || Code == other.Code);
        }
    }
}
