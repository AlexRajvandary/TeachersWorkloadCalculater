namespace StudingWorkloadCalculator.Models
{
    public class TeachersWorkload : PropertyChangedNotifier
    {
        public TeachersWorkload(Teacher teacher, Workload workload) : this(teacher, workload, 1) { }

        public TeachersWorkload(Teacher teacher, Workload workload, double rate)
        {
            Teacher = teacher;
            Workload = workload;
            Workload.PropertyChanged += UpdatePayment;
            Rate = rate;
        }

        public Teacher Teacher { get; set; }

        public Workload Workload { get; set; }

        public double Payment { get; set; }

        public bool IsBudged { get; set; }

        public double Rate { get; set; }

        private void UpdatePayment(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Payment = Workload.Total * Rate;
        }
    }
}
