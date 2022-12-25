using System.Collections.Generic;

namespace StudingWorkloadCalculator.Models
{
    public class TeachersWorkloadViewModel : PropertyChangedNotifier
    {
        private double payment;
        private IEnumerable<SubjectWithWorkload> subjects;

        public TeachersWorkloadViewModel(Teacher teacher, Workload workload, IEnumerable<SubjectWithWorkload> subjects, double rate)
        {
            Teacher = teacher;
            Workload = workload;
            Workload.PropertyChanged += UpdatePayment;
            Rate = rate;
            Subjects = subjects;
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

        public bool IsBudged { get; set; }

        public double Rate { get; set; }

        public void UpdatePayment(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Payment = Workload.Total * Rate;
        }
    }
}
