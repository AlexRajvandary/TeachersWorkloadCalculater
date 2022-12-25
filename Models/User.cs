using StudingWorkloadCalculator.UserControls;

namespace StudingWorkloadCalculator.Models
{
    public class User : PropertyChangedNotifier
    {
        private string name;
        private string password;
        private bool specialRights;

        public User(int id, string name, string password, bool specialRights)
        {
            Id = id;
            Name = name;
            Password = password;
            SpecialRights = specialRights;
        }

        public int Id { get; }

        [DataGridColumnGenerator("Логин")]
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        [DataGridColumnGenerator("Пароль")]
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        [DataGridColumnGenerator("Привилегия")]
        public bool SpecialRights
        {
            get => specialRights;
            set
            {
                specialRights = value;
                OnPropertyChanged();
            }
        }
    }
}
