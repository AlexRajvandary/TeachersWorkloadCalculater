namespace StudingWorkloadCalculator.Models
{
    public class SubjectWithWorkload : PropertyChangedNotifier
    {
        private Subject subject;
        private Workload workload;

        public SubjectWithWorkload(Subject subject, Workload workload)
        {
            this.subject = subject;
            this.workload = workload;
        }

        public Subject Subject
        {
            get => subject;
            set
            {
                if (subject != null)
                {
                    subject = value;
                    OnPropertyChanged();
                }
            }
        }

        public Workload Workload
        {
            get => workload;
            set
            {
                if (workload != null)
                {
                    workload = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
