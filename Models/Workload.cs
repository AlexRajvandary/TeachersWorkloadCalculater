using System;

namespace StudingWorkloadCalculator.Models
{
    public class Workload : PropertyChangedNotifier
    {
        private int ipz;
        private int kr;
        private int firstSemester;
        private int secondSemester;
        private int theory;
        private int total;

        public Workload(int theory, int ipz, int kr, int firstSemester, int secondSemester)
        {
            if(theory < 0 || ipz < 0 || kr < 0 || firstSemester < 0 || secondSemester < 0)
            {
                throw new ArgumentException("Value cannot be less than zero.");
            }

            this.theory = theory;
            this.ipz = ipz;
            this.kr = kr;
            this.firstSemester = firstSemester;
            this.secondSemester = secondSemester;
            RecalculateTotal();
        }

        public int Theory
        {
            get => theory;
            set
            {
                if(theory != value && value >= 0)
                {
                    theory = value;
                    OnPropertyChanged();
                    RecalculateTotal();
                }
            }
        }

        public int FirstSemester
        {
            get => firstSemester;
            set
            {
                if (firstSemester != value && value >= 0)
                {
                    firstSemester = value;
                    OnPropertyChanged();
                    RecalculateTotal();
                }
            }
        }

        public int SecondSemester
        {
            get => secondSemester;
            set
            {
                if (secondSemester != value && value >= 0)
                {
                    secondSemester = value;
                    OnPropertyChanged();
                    RecalculateTotal();
                }
            }
        }

        public int Kr
        {
            get => kr;
            set
            {
                if(kr != value && value >= 0)
                {
                    kr = value;
                    OnPropertyChanged();
                    RecalculateTotal();
                }
            }
        }

        public int Ipz
        {
            get => ipz;
            set
            {
                if (ipz != value && value >= 0)
                {
                    ipz = value;
                    OnPropertyChanged();
                    RecalculateTotal();
                }
            }
        }

        public int Total
        {
            get => total;
            set
            {
                if (total != value)
                {
                    total = value;
                    OnPropertyChanged();
                }
            }
        }

        private void RecalculateTotal()
        {
            Total = Theory + Ipz + Kr + FirstSemester + SecondSemester;
        }

        public static Workload operator +(Workload left, Workload right)
        {
            return new Workload(left.Theory + right.Theory,
                                left.Ipz + right.Ipz,
                                left.kr + right.Kr,
                                left.FirstSemester + right.FirstSemester, 
                                left.SecondSemester + right.SecondSemester);
        }
    }
}
