using StudingWorkloadCalculator.MainVewModels;
using StudingWorkloadCalculator.Models;
using StudingWorkloadCalculator.UserControls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace StudingWorkloadCalculator.Windows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window, INotifyPropertyChanged
    {
        private bool displayStudents;

        public Window1()
        {
            InitializeComponent();
            MainViewModel = new MainViewModel();
            MainViewModel.StudentsViewModel.PropertyChanged += StudentsViewModel_PropertyChanged;
            MainViewModel.SpecializationsViewModel.PropertyChanged += SpecializationViewModel_PropertyChanged;
            MainViewModel.SubjectViewModel.PropertyChanged += SubjectViewModel_PropertyChanged;
            MainViewModel.TeachersViewModel.PropertyChanged += TeachersViewModel_PropertyChanged;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel MainViewModel { get; set; }

        public bool DisplayStudents
        {
            get => displayStudents;
            set
            {
                displayStudents = value;
                OnPropertyChanged();
            }
        }

        public bool DisplayTeachers { get; set; }

        public bool DisplayGroups { get; set; }

        public bool DisplaySpecializations { get; set; }

        private void StudentsViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is DataPresenterViewModel<Student> dataPresenterViewmodel)
            {
                if (e.PropertyName == nameof(dataPresenterViewmodel.Data))
                {
                    ShowStudents();
                }
            }
        }

        private void TeachersViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is DataPresenterViewModel<Teacher> dataPresenterViewmodel)
            {
                if (e.PropertyName == nameof(dataPresenterViewmodel.Data))
                {
                    ShowTeachers();
                }
            }
        }

        private void SubjectViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is DataPresenterViewModel<Subject> dataPresenterViewmodel)
            {
                if (e.PropertyName == nameof(dataPresenterViewmodel.Data))
                {
                    ShowSubjects();
                }
            }
        }

        private void SpecializationViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is DataPresenterViewModel<Specialization> dataPresenterViewmodel)
            {
                if (e.PropertyName == nameof(dataPresenterViewmodel.Data))
                {
                    ShowSpecializations();
                }
            }
        }

        private void ShowStudents()
        {
            AddTabItem("Студенты", out var tabItem);
            var editingDatatable = new EditingDataTable();
            editingDatatable.SetBinding(EditingDataTable.ItemsSourceProperty, new Binding() { Source = MainViewModel.StudentsViewModel, Path = new PropertyPath(nameof(MainViewModel.StudentsViewModel.Data)) });
            tabItem.Content = editingDatatable;
            DisplayStudents = true;
        }

        private void ShowTeachers()
        {
            AddTabItem("Преподаватели", out var tabItem);
            var editingDatatable = new EditingDataTable();
            editingDatatable.SetBinding(EditingDataTable.ItemsSourceProperty, new Binding() { Source = MainViewModel.TeachersViewModel, Path = new PropertyPath(nameof(MainViewModel.TeachersViewModel.Data)) });
            tabItem.Content = editingDatatable;
            DisplayStudents = true;
        }

        private void ShowSpecializations()
        {
            AddTabItem("Специализации", out var tabItem);
            var editingDatatable = new EditingDataTable();
            editingDatatable.SetBinding(EditingDataTable.ItemsSourceProperty, new Binding() { Source = MainViewModel.SpecializationsViewModel, Path = new PropertyPath(nameof(MainViewModel.SpecializationsViewModel.Data)) });
            tabItem.Content = editingDatatable;
            DisplayStudents = true;
        }

        private void ShowSubjects()
        {
            AddTabItem("Предметы", out var tabItem);
            var editingDatatable = new EditingDataTable();
            editingDatatable.SetBinding(EditingDataTable.ItemsSourceProperty, new Binding() { Source = MainViewModel.SubjectViewModel, Path = new PropertyPath(nameof(MainViewModel.SubjectViewModel.Data)) });
            tabItem.Content = editingDatatable;
            DisplayStudents = true;
        }

        private static bool TryGetTabItemExists(string name, TabControl tabControl, out TabItem? item)
        {
            foreach (var tabItem in tabControl.Items)
            {
                var i = tabItem as TabItem;
                if (i?.Header as string == name)
                {
                    item = i;
                    return true;
                }
            }

            item = null;
            return false;
        }

        private bool AddTabItem(string header, out TabItem tabItem)
        {
            if (!TryGetTabItemExists(header, Tabs, out var existingTabItem))
            {
                var newTabItem = new TabItem() { Header = header };
                Tabs.Items.Add(newTabItem);
                tabItem = newTabItem;
                return true;
            }
            else
            {
                tabItem = existingTabItem;
                return false;
            }
        }

        private void TeachersCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            ShowTeachers();
        }

        private void StudentsCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            ShowStudents();
        }

        private void GroupsCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            ShowSubjects();
        }

        private void RemoveTabItem(string header)
        {
            if (TryGetTabItemExists(header, Tabs, out var tabItem))
            {
                Tabs.Items.Remove(tabItem);
            }
        }

        private void SpecializationCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            ShowSpecializations();
        }

        private void TeachersCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            RemoveTabItem("Учителя");
        }

        private void StudentsCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            RemoveTabItem("Студенты");
        }

        private void SpecialiationsCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            RemoveTabItem("Специализации");
        }

        private void GroupsCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            RemoveTabItem("Предметы");
        }

        private void LoadDataFromExcelButtonClicked(object sender, RoutedEventArgs e)
        {
            var window = new ExcelFileMapper();
            window.Owner = this;
            window.DataContext = MainViewModel;
            window.Show();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SaveToExcel(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MainViewModel.StudentsViewModel.DataSourcePath))
            {
                ExcelWriter.ExcelWriter.WriteExcelFile(MainViewModel.StudentsViewModel.DataSourcePath, MainViewModel.StudentsViewModel.Data);
            }
            else
            {
                ExcelWriter.ExcelWriter.WriteExcelFile("Students.xlsx", MainViewModel.StudentsViewModel.Data);
            }

            if (!string.IsNullOrEmpty(MainViewModel.SpecializationsViewModel.DataSourcePath))
            {
                ExcelWriter.ExcelWriter.WriteExcelFile(MainViewModel.SpecializationsViewModel.DataSourcePath, MainViewModel.SpecializationsViewModel.Data);
            }
            else
            {
                ExcelWriter.ExcelWriter.WriteExcelFile("Specializations.xlsx", MainViewModel.SpecializationsViewModel.Data);
            }

            if (!string.IsNullOrEmpty(MainViewModel.SubjectViewModel.DataSourcePath))
            {
                ExcelWriter.ExcelWriter.WriteExcelFile(MainViewModel.SubjectViewModel.DataSourcePath, MainViewModel.SubjectViewModel.Data);
            }
            else
            {
                ExcelWriter.ExcelWriter.WriteExcelFile("Subjects.xlsx", MainViewModel.SubjectViewModel.Data);
            }

            if (!string.IsNullOrEmpty(MainViewModel.TeachersViewModel.DataSourcePath))
            {
                ExcelWriter.ExcelWriter.WriteExcelFile(MainViewModel.TeachersViewModel.DataSourcePath, MainViewModel.TeachersViewModel.Data);
            }
            else
            {
                ExcelWriter.ExcelWriter.WriteExcelFile("Teachers.xlsx", MainViewModel.TeachersViewModel.Data);
            }
        }
    }
}
