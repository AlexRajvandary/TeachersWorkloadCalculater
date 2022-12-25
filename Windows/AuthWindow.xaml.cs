using StudingWorkloadCalculator.AccessDataBase;
using StudingWorkloadCalculator.MainVewModels;
using StudingWorkloadCalculator.Models;
using StudingWorkloadCalculator.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            var mv = new MainViewModel();
            var user = LoginComboBox.SelectedItem as User;
            if (mv is not null && !string.IsNullOrWhiteSpace(user.Name) && !string.IsNullOrEmpty(PasswordTextBox.Password))
            {
                if(user.Password == PasswordTextBox.Password)
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
