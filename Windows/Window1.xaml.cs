using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using StudingWorkloadCalculator.MainVewModels;
using StudingWorkloadCalculator.Models;
using StudingWorkloadCalculator.UserControls;
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
using System.Windows.Shapes;

namespace StudingWorkloadCalculator.Windows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            MainViewModel = new MainViewModel();
            MainViewModel.StudentsViewModel.PropertyChanged += StudentsViewModel_PropertyChanged;
        }

        private void StudentsViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(sender is DataPresenterViewModel<Student> dataPresenterViewmodel)
            {
                if (e.PropertyName == nameof(dataPresenterViewmodel.Data))
                {
                    AddTabItem("Students", out var tabItem);
                    var editingDatatable = new EditingDataTable();
                    editingDatatable.SetBinding(EditingDataTable.ItemsSourceProperty, new Binding() { Source = MainViewModel.StudentsViewModel, Path = new PropertyPath(nameof(MainViewModel.StudentsViewModel.Data)) });
                    tabItem.Content = editingDatatable;
                }
            }
        }

        public MainViewModel MainViewModel { get; set; }

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
            AddTabItem("Teachers", out _);
        }

        private void StudentsCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            AddTabItem("Students", out _);
        }

        private void GroupsCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            AddTabItem("Groups", out _);
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
            AddTabItem("Specializations", out _);
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

        private void LoadDataFromExcel(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files(.xls)|*.xls| Excel Files(.xlsx)| *.xlsx | Excel Files(*.xlsm) | *.xlsm|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                MainViewModel.StudentsViewModel.DataSourcePath = openFileDialog.FileName;
            }
        }
    }
}
