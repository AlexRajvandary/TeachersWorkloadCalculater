using StudingWorkloadCalculator.MainVewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace StudingWorkloadCalculator.Windows
{
    /// <summary>
    /// Interaction logic for EnterPathToSafe.xaml
    /// </summary>
    public partial class EnterPathToSafe : Window
    {
        public event EventHandler<string> PasswordUpdated;

        public EnterPathToSafe()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PasswordBox.Text))
            {
                PasswordUpdated?.Invoke(this, PasswordBox.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Пароль не может быть пустой строкой или пробелом.");
            }
        }
    }
}
