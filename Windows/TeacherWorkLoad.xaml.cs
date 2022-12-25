using StudingWorkloadCalculator.MainVewModels;
using StudingWorkloadCalculator.Models;
using StudingWorkloadCalculator.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for TeacherWorkLoad.xaml
    /// </summary>
    public partial class TeacherWorkLoad : Window
    {
        public TeacherWorkLoad(MainViewModel mv)
        {
            InitializeComponent();
            DataContext = mv;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).CalculateWorkLoad();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(DataContext is MainViewModel mainViewModel)
            {
                if(mainViewModel.TeachersWorkload is null)
                {
                    Button_Click(null, null);
                }

                var path = ExcelWriter.ExcelWriter.GenerateReport(mainViewModel.TeachersWorkload);
                Process.Start(System.IO.Path.GetFullPath(path));
            }
        }
    }
}
