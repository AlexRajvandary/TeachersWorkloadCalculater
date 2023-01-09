using StudingWorkloadCalculator.UserControls;

namespace StudingWorkloadCalculator.Models
{
    public class Student : Person
    {
        private Group group;
        private Specialization specialization;

        public Student()
        {

        }

        public Student(string firstName,
                      string lastName,
                      string familyName,
                      Gender gender,
                      string specializationCode,
                      string groupName) : base(firstName, lastName, familyName, gender)
        {
            SpecializationCode = specializationCode;
            GroupName = groupName;
            if(IRepository<Group>.Instances.TryGetValue(groupName, out var group))
            {
                Group = group;
            }

            if (IRepository<Specialization>.Instances.TryGetValue(specializationCode, out var specialization))
            {
                Specialization = specialization;
            }
        }

        public Student(string firstName,
                       string lastName,
                       string familyName,
                       Gender gender,
                       Specialization specialization,
                       Group group) : base(firstName, lastName, familyName, gender)
        {
            Specialization = specialization;
            Group = group;
        }

        [DataGridColumnGenerator(false)]
        public Specialization Specialization
        {
            get => specialization; 
            set
            {
                if (specialization != value)
                {
                    specialization = value;
                    OnPropertyChanged();

                    if(specialization.Code != SpecializationCode)
                    {
                        specialization.Code = SpecializationCode;
                    }
                }
            }
        }

        [DataGridColumnGenerator("Специализация")]
        public string SpecializationCode { get; private set; }

        [DataGridColumnGenerator(false)]
        public Group Group
        {
            get => group;
            set
            {
                if (group != value)
                {
                    group = value;
                    OnPropertyChanged();
                }
            }
        }

        [DataGridColumnGenerator("Группа")]
        public string GroupName { get; set; }
    }
}
