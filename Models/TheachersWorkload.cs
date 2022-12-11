namespace StudingWorkloadCalculator.Models
{
    public class TheachersWorkload : PropertyChangedNotifier
    {
        public TheachersWorkload(Teacher teacher, Workload workload, Subject subject) : this(teacher, workload, subject, 1) { }

        public TheachersWorkload(Teacher teacher, Workload workload, Subject subject, double rate)
        {
            Teacher = teacher;
            Workload = workload;
            Subject = subject;
            Workload.PropertyChanged += UpdatePayment;
            Rate = rate;
        }

        public Teacher Teacher { get; set; }

        public Workload Workload { get; set; }

        public Subject Subject { get; set; }

        public double Payment { get; set; }

        public bool isBudged { get; set; }

        public double Rate { get; set; }

        private void UpdatePayment(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Payment = Workload.Total * Rate;
        }
    }
}
