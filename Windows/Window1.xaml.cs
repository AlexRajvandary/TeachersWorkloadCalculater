using Microsoft.Win32;
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
            MainViewModel.SpecializationsViewModel.PropertyChanged += StudentsViewModel_PropertyChanged;
            MainViewModel.SpecializationViewModel.PropertyChanged += SpecializationViewModel_PropertyChanged;
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
                    ShowTabItem("Students", dataPresenterViewmodel);
                }
            }
        }

        private void TeachersViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is DataPresenterViewModel<Teacher> dataPresenterViewmodel)
            {
                if (e.PropertyName == nameof(dataPresenterViewmodel.Data))
                {
                    ShowTabItem("Teacher", dataPresenterViewmodel);
                }
            }
        }

        private void SubjectViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is DataPresenterViewModel<Subject> dataPresenterViewmodel)
            {
                if (e.PropertyName == nameof(dataPresenterViewmodel.Data))
                {
                    ShowTabItem("Subjects", dataPresenterViewmodel);
                }
            }
        }

        private void SpecializationViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is DataPresenterViewModel<Specialization> dataPresenterViewmodel)
            {
                if (e.PropertyName == nameof(dataPresenterViewmodel.Data))
                {
                    ShowTabItem("Speialization", dataPresenterViewmodel);
                }
            }
        }

        private void ShowTabItem<T>(string title, DataPresenterViewModel<T> dataPresenterViewModel)
        {
            AddTabItem(title, out var tabItem);
            var editingDatatable = new EditingDataTable();
            editingDatatable.SetBinding(EditingDataTable.ItemsSourceProperty, new Binding() { Source = dataPresenterViewModel, Path = new PropertyPath(nameof(dataPresenterViewModel.Data)) });
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
            ShowTabItem("Teachers", MainViewModel.SpecializationsViewModel);
        }

        private void StudentsCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            ShowTabItem("Students", MainViewModel.SpecializationsViewModel);
        }

        private void GroupsCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            ShowTabItem("Groups", MainViewModel.SpecializationsViewModel);
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
            ShowTabItem("Specializations", MainViewModel.SpecializationsViewModel);
        }

        private void TeachersCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            RemoveTabItem("Teachers");
        }

        private void StudentsCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            RemoveTabItem("Students");
        }

        private void SpecialiationsCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            RemoveTabItem("Specializations");
        }

        private void GroupsCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            RemoveTabItem("Groups");
        }

        private void LoadDataFromExcelButtonClicked(object sender, RoutedEventArgs e)
        {
            var window = new ExcelFileMapper();
            window.Owner= this;
            window.DataContext = MainViewModel;
            window.Show();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SaveToExcel(object sender, RoutedEventArgs e)
        {

        }
    }
}
