using System.Collections.Generic;

namespace StudingWorkloadCalculator.Models
{
    public class TeachersWorkloadViewModel : PropertyChangedNotifier
    {
        private int limit;
        private double payment;
        private IEnumerable<SubjectWithWorkload> subjects;

        public TeachersWorkloadViewModel()
        {

        }

        public TeachersWorkloadViewModel(Teacher teacher, Workload workload, IEnumerable<SubjectWithWorkload> subjects, double rate, int limit)
        {
            Teacher = teacher;
            Workload = workload;
            Workload.PropertyChanged += UpdatePayment;
            Rate = rate;
            Subjects = subjects;
            Limit = limit;
        }

        public Teacher Teacher { get; set; }

        public Workload Workload { get; set; }

        public IEnumerable<SubjectWithWorkload> Subjects 
        {
            get => subjects;
            set
            {
                subjects = value;
                OnPropertyChanged();
            }
        }

        public double Payment 
        {
            get => payment;
            set
            {
                payment = value;
                OnPropertyChanged();
            }
        }

        public int Limit
        {
            get => limit;
            set
            {
                limit = value;
                OnPropertyChanged();
            }
        }

        public bool IsBudged { get; set; }

        public double Rate { get; set; }

        public void UpdatePayment(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Payment = Workload.Total * Rate;
        }
    }
}
