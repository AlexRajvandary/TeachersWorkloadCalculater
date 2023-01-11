using Microsoft.Win32;
using StudingWorkloadCalculator.MainVewModels;
using System.Windows;

namespace StudingWorkloadCalculator.Windows
{
    /// <summary>
    /// Interaction logic for ExcelFileMapper.xaml
    /// </summary>
    public partial class ExcelFileMapper : Window
    {
        private const string filter = "Excel Worksheets 2003(*.xls)|*.xls|Excel Worksheets 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";

        public ExcelFileMapper()
        {
            InitializeComponent();
            DataContextChanged += ExcelFileMapper_DataContextChanged;
        }

        public MainViewModel MainViewModel { get; set; }

        private void ExcelFileMapper_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is MainViewModel mainViewModel)
            {
                MainViewModel = mainViewModel;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ExcelChoseFileDialog(out var path))
            {
                MainViewModel.TeachersViewModel.DataSourcePath = path;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (ExcelChoseFileDialog(out var path))
            {
                MainViewModel.SubjectViewModel.DataSourcePath = path;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (ExcelChoseFileDialog(out var path))
            {
                MainViewModel.SpecializationsViewModel.DataSourcePath = path;
            }
        }

        private static bool ExcelChoseFileDialog(out string selectedFilePath)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = filter
            };
            var result = openFileDialog.ShowDialog();
            selectedFilePath = openFileDialog.FileName;
            return result ?? false;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (ExcelChoseFileDialog(out var path))
            {
                MainViewModel.GroupsViewModel.DataSourcePath = path;
            }
        }
    }
}
