using StudingWorkloadCalculator.AccessDataBase;
using StudingWorkloadCalculator.MainVewModels;
using StudingWorkloadCalculator.Models;
using StudingWorkloadCalculator.Windows;
using System.Collections.Generic;
using System.Windows;

namespace StudingWorkloadCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public static DependencyProperty UsersProperty = DependencyProperty.Register("Users", typeof(IEnumerable<User>), typeof(AuthWindow));

        public AuthWindow()
        {
            InitializeComponent();
            LoginComboBox.Items.Clear();
            DbConnection.OpenConnection();
            Users = AccsessDataTableReader.GetUsers();
        }

        public IEnumerable<User> Users
        {
            get
            {
                return (IEnumerable<User>)GetValue(UsersProperty);
            }
            set
            {
                SetValue(UsersProperty, value);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (LoginComboBox.SelectedItem == null & string.IsNullOrWhiteSpace(PasswordTextBox.Password))
            {
                MessageBox.Show("Выберите пользователя и введите пароль.");
            }

            if (LoginComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя");
            }

            if (string.IsNullOrWhiteSpace(PasswordTextBox.Password))
            {
                MessageBox.Show("Введите пароль.");
            }

            var mv = new MainViewModel();
            var user = LoginComboBox.SelectedItem as User;
            if (mv is not null && !string.IsNullOrEmpty(PasswordTextBox.Password))
            {
                if (user.Password == PasswordTextBox.Password)
                {
                    mv.User = user;
                    var mainWindow = new Window1(mv);
                    mainWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неверный пароль.");
                }
            }
        }
    }
}
