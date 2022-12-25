using StudingWorkloadCalculator.MainVewModels;
using System.IO;
using System.Windows;

namespace StudingWorkloadCalculator.Windows
{
    /// <summary>
    /// Interaction logic for TeacherWorkLoad.xaml
    /// </summary>
    public partial class TeacherWorkLoad : System.Windows.Window
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
                MessageBox.Show($"Отчет сохранён.\n{Path.GetFullPath(path)} ", "Отчёт сохранён.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
