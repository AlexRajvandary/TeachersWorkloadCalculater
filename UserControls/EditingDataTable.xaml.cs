using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StudingWorkloadCalculator.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class EditingDataTable : UserControl
    {
        public static DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(EditingDataTable), new PropertyMetadata(null));
        public static DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(EditingDataTable), new PropertyMetadata(null));
        public static DependencyProperty DeleteItemCommandProperty = DependencyProperty.Register("DeleteItemCommand", typeof(ICommand), typeof(EditingDataTable), new PropertyMetadata(null));
        public static DependencyProperty AddItemCommandProperty = DependencyProperty.Register("AddItemCommand", typeof(ICommand), typeof(EditingDataTable), new PropertyMetadata(null));
        public static DependencyProperty EditedItemProperty = DependencyProperty.Register("EditedItem", typeof(object), typeof(EditingDataTable), new PropertyMetadata(null));

        public event Action<object> ItemEditEnded;

        public EditingDataTable()
        {
            InitializeComponent();
            DataContext = this;
        }

        public object EditedItem
        {
            get { return GetValue(EditedItemProperty); }
            set { SetValue(EditedItemProperty, value); }
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public ICommand AddItemCommand
        {
            get { return (ICommand)(GetValue(AddItemCommandProperty)); }
            set { SetValue(AddItemCommandProperty, value); }
        }

        public ICommand DeleteItemCommand
        {
            get { return (ICommand)(GetValue(DeleteItemCommandProperty)); }
            set { SetValue(DeleteItemCommandProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            var attribute = propertyDescriptor.Attributes.OfType<DataGridColumnGeneratorAttribute>().FirstOrDefault();
            if (!attribute?.GenerateColumn ?? true)
            {
                e.Cancel = true;
            }
            else
            {
                e.Column.Header = string.IsNullOrWhiteSpace(attribute?.ColumnName) ? propertyDescriptor.DisplayName : attribute.ColumnName;
            }
        }

        private void ItemsTable_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            ItemEditEnded?.Invoke(e.Row.Item);
        }
    }
}
