using StudingWorkloadCalculator.AccessDataBase;
using StudingWorkloadCalculator.MainVewModels;
using StudingWorkloadCalculator.Models;
using StudingWorkloadCalculator.UserControls;
using System.ComponentModel;
using System.Data;
using System.Linq;
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
        private bool displayTeachers;
        private bool displayGroups;
        private bool displaySpecializations;

        public Window1(MainViewModel mainViewModel)
        {
            InitializeComponent();
            MainViewModel = mainViewModel;
            MainViewModel.SpecializationsViewModel.PropertyChanged += SpecializationViewModel_PropertyChanged;
            MainViewModel.SubjectViewModel.PropertyChanged += SubjectViewModel_PropertyChanged;
            MainViewModel.TeachersViewModel.PropertyChanged += TeachersViewModel_PropertyChanged;
            MainViewModel.GroupsViewModel.PropertyChanged += GroupsViewModel_PropertyChanged;
            LoadData();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel MainViewModel { get; set; }

        public bool DisplayTeachers
        {
            get => displayTeachers;
            set
            {
                displayTeachers = value;
                OnPropertyChanged();
            }
        }

        public bool DisplaySubjects
        {
            get => displayGroups;
            set
            {
                displayGroups = value;
                OnPropertyChanged();
            }
        }

        public bool DisplaySpecializations
        {
            get => displaySpecializations;
            set
            {
                displaySpecializations = value;
                OnPropertyChanged();
            }
        }

        public bool DisplayGroups
        {
            get => displayGroups;
            set
            {
                displayGroups = value;
                OnPropertyChanged();
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

        private void GroupsViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is DataPresenterViewModel<Teacher> dataPresenterViewmodel)
            {
                if (e.PropertyName == nameof(dataPresenterViewmodel.Data))
                {
                    ShowGroups();
                }
            }
        }

        private void ShowTeachers()
        {
            AddTabItem("Преподаватели", out var tabItem);
            var editingDatatable = new EditingDataTable();
            editingDatatable.ItemEditEnded += MainViewModel.TeachersViewModel.UpdateItem;
            editingDatatable.Filter += MainViewModel.TeachersViewModel.FilterData;
            editingDatatable.SetBinding(EditingDataTable.ItemsSourceProperty, new Binding() { Source = MainViewModel.TeachersViewModel, Path = new PropertyPath(nameof(MainViewModel.TeachersViewModel.Data)) });
            editingDatatable.SetBinding(EditingDataTable.SelectedItemProperty, new Binding() { Source = MainViewModel.TeachersViewModel, Path = new PropertyPath(nameof(MainViewModel.TeachersViewModel.SelectedItem)), Mode = BindingMode.OneWayToSource });
            editingDatatable.SetBinding(EditingDataTable.DeleteItemCommandProperty, new Binding() { Source = MainViewModel.TeachersViewModel, Path = new PropertyPath(nameof(MainViewModel.TeachersViewModel.DeleteItemCommand)) });
            editingDatatable.SetBinding(EditingDataTable.AddItemCommandProperty, new Binding() { Source = MainViewModel.TeachersViewModel, Path = new PropertyPath(nameof(MainViewModel.TeachersViewModel.AddItemCommand)) });
            tabItem.Content = editingDatatable;
            DisplayTeachers = true;
        }

        private void ShowSpecializations()
        {
            AddTabItem("Специализации", out var tabItem);
            var editingDatatable = new EditingDataTable();
            editingDatatable.ItemEditEnded += MainViewModel.SpecializationsViewModel.UpdateItem;
            editingDatatable.Filter += MainViewModel.SpecializationsViewModel.FilterData;
            editingDatatable.SetBinding(EditingDataTable.ItemsSourceProperty, new Binding() { Source = MainViewModel.SpecializationsViewModel, Path = new PropertyPath(nameof(MainViewModel.SpecializationsViewModel.Data)) });
            editingDatatable.SetBinding(EditingDataTable.SelectedItemProperty, new Binding() { Source = MainViewModel.SpecializationsViewModel, Path = new PropertyPath(nameof(MainViewModel.SpecializationsViewModel.SelectedItem)), Mode = BindingMode.OneWayToSource });
            editingDatatable.SetBinding(EditingDataTable.DeleteItemCommandProperty, new Binding() { Source = MainViewModel.SpecializationsViewModel, Path = new PropertyPath(nameof(MainViewModel.SpecializationsViewModel.DeleteItemCommand)) });
            editingDatatable.SetBinding(EditingDataTable.AddItemCommandProperty, new Binding() { Source = MainViewModel.SpecializationsViewModel, Path = new PropertyPath(nameof(MainViewModel.SpecializationsViewModel.AddItemCommand)) });
            tabItem.Content = editingDatatable;
            DisplaySpecializations = true;
        }

        private void ShowSubjects()
        {
            AddTabItem("Предметы", out var tabItem);
            var editingDatatable = new EditingDataTable();
            editingDatatable.ItemEditEnded += MainViewModel.SubjectViewModel.UpdateItem;
            editingDatatable.Filter += MainViewModel.SubjectViewModel.FilterData;
            editingDatatable.SetBinding(EditingDataTable.ItemsSourceProperty, new Binding() { Source = MainViewModel.SubjectViewModel, Path = new PropertyPath(nameof(MainViewModel.SubjectViewModel.Data)) });
            editingDatatable.SetBinding(EditingDataTable.SelectedItemProperty, new Binding() { Source = MainViewModel.SubjectViewModel, Path = new PropertyPath(nameof(MainViewModel.SubjectViewModel.SelectedItem)), Mode = BindingMode.OneWayToSource });
            editingDatatable.SetBinding(EditingDataTable.DeleteItemCommandProperty, new Binding() { Source = MainViewModel.SubjectViewModel, Path = new PropertyPath(nameof(MainViewModel.SubjectViewModel.DeleteItemCommand)) });
            editingDatatable.SetBinding(EditingDataTable.AddItemCommandProperty, new Binding() { Source = MainViewModel.SubjectViewModel, Path = new PropertyPath(nameof(MainViewModel.SubjectViewModel.AddItemCommand)) });
            tabItem.Content = editingDatatable;
            DisplaySubjects = true;
        }

        private void ShowGroups()
        {
            AddTabItem("Группы", out var tabItem);
            var editingDatatable = new EditingDataTable();
            editingDatatable.ItemEditEnded += MainViewModel.GroupsViewModel.UpdateItem;
            editingDatatable.Filter += MainViewModel.GroupsViewModel.FilterData;
            editingDatatable.SetBinding(EditingDataTable.ItemsSourceProperty, new Binding() { Source = MainViewModel.GroupsViewModel, Path = new PropertyPath(nameof(MainViewModel.GroupsViewModel.Data))});
            editingDatatable.SetBinding(EditingDataTable.SelectedItemProperty, new Binding() { Source = MainViewModel.GroupsViewModel, Path = new PropertyPath(nameof(MainViewModel.GroupsViewModel.SelectedItem)), Mode = BindingMode.OneWayToSource });
            editingDatatable.SetBinding(EditingDataTable.DeleteItemCommandProperty, new Binding() { Source = MainViewModel.GroupsViewModel, Path = new PropertyPath(nameof(MainViewModel.GroupsViewModel.DeleteItemCommand)) });
            editingDatatable.SetBinding(EditingDataTable.AddItemCommandProperty, new Binding() { Source = MainViewModel.GroupsViewModel, Path = new PropertyPath(nameof(MainViewModel.GroupsViewModel.AddItemCommand)) });
            tabItem.Content = editingDatatable;
            DisplayGroups = true;
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

        private void GroupsCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            ShowGroups();
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
            RemoveTabItem("Преподаватели");
        }

        private void SpecialiationsCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            RemoveTabItem("Специализации");
        }

        private void GroupsCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            RemoveTabItem("Группы");
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

            if (!string.IsNullOrEmpty(MainViewModel.GroupsViewModel.DataSourcePath))
            {
                ExcelWriter.ExcelWriter.WriteExcelFile(MainViewModel.GroupsViewModel.DataSourcePath, MainViewModel.GroupsViewModel.Data);
            }
            else
            {
                ExcelWriter.ExcelWriter.WriteExcelFile("Groups.xlsx", MainViewModel.GroupsViewModel.Data);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemClick(object sender, RoutedEventArgs e)
        {
            var newPasswordWindow = new EnterPathToSafe();
            newPasswordWindow.PasswordUpdated += NewPasswordWindow_PasswordUpdated;
            newPasswordWindow.ShowDialog();
        }

        private void NewPasswordWindow_PasswordUpdated(object? sender, string newPassword)
        {
            MainViewModel.User.Password = newPassword;
            AccsessDataTableReader.SaveUser(MainViewModel.User);
            MessageBox.Show("Пароль успешно сохранён!");
        }

        private void CalculateWorkLoad(object sender, RoutedEventArgs e)
        {
            if (MainViewModel.TeachersViewModel.SelectedItem == null)
            {
                MessageBox.Show("Выберите учителя для рассчёта нагрузки");
                return;
            }

            if (MainViewModel.SubjectViewModel.Data == null)
            {
                MessageBox.Show("Загрузите предметы для рассчёта нагрузки");
                return;
            }
            var window = new TeacherWorkLoad(MainViewModel);
            window.Show();
        }

        private void LoadData()
        {
            MainViewModel.TeachersViewModel.AddData(AccsessDataTableReader.GetTeachers());
            MainViewModel.SubjectViewModel.AddData(AccsessDataTableReader.GetSubjectsWithWorkLoads());
            MainViewModel.SpecializationsViewModel.AddData(AccsessDataTableReader.GetSpecializations());
            MainViewModel.GroupsViewModel.AddData(AccsessDataTableReader.GetGroups());  
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            RemoveTabItem("Предметы");
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ShowSubjects();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var infoWindow = new Info();
            infoWindow.Show();
        }
    }
}
