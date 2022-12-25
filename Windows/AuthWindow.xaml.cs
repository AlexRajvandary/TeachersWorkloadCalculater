using StudingWorkloadCalculator.MainVewModels;
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
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mv = new MainViewModel();
            if (mv is not null && !string.IsNullOrWhiteSpace(LoginTextBox.Text) && !string.IsNullOrEmpty(PasswordTextBox.Password))
            {
                if(mv.Auth(LoginTextBox.Text, PasswordTextBox.Password))
                {
                    var mainWindow = new Window1(mv);
                    mainWindow.Show();
                    this.Close();
                }
            }
        }
    }
}
