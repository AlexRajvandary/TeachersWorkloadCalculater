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
        public static DependencyProperty SelectedPathProperty = DependencyProperty.Register("SelectedPath", typeof(string), typeof(EnterPathToSafe));

        public EnterPathToSafe()
        {
            InitializeComponent();
        }

        public string SelectedPath 
        {
            get { return (string)GetValue(SelectedPathProperty); }
            set { SetValue(SelectedPathProperty, value); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
