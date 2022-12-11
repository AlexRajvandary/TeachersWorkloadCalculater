using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudingWorkloadCalculator.Models
{
    public class Student : Person
    {
        private Group group;
        private Specialization specialization;

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

        public Group Group
        {
            get { return group; }
            set
            {
                if (group != value)
                {
                    group = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
