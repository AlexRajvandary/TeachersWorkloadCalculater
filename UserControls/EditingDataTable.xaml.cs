using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace StudingWorkloadCalculator.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class EditingDataTable : UserControl
    {
        public static DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(EditingDataTable), new PropertyMetadata(null, (o, e) =>
        {
            if (o is EditingDataTable table)
            {
                table.tableEntriesCollectionViewSource.Source = table.ItemsSource;
            }
        }));
        public static DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(EditingDataTable), new PropertyMetadata(null));
        public static DependencyProperty DeleteItemCommandProperty = DependencyProperty.Register("DeleteItemCommand", typeof(ICommand), typeof(EditingDataTable), new PropertyMetadata(null));
        public static DependencyProperty AddItemCommandProperty = DependencyProperty.Register("AddItemCommand", typeof(ICommand), typeof(EditingDataTable), new PropertyMetadata(null));
        public static DependencyProperty EditedItemProperty = DependencyProperty.Register("EditedItem", typeof(object), typeof(EditingDataTable), new PropertyMetadata(null));

        public event Action<object> ItemEditEnded;
        public event Func<(string, string)[], Func<object, bool>> Filter;
        private bool _handle = true;
        private CollectionViewSource tableEntriesCollectionViewSource;
        private Dictionary<string, string> filterColumns = new Dictionary<string, string>();

        public EditingDataTable()
        {
            InitializeComponent();
            DataContext = this;
            tableEntriesCollectionViewSource = (CollectionViewSource)editingDataTable.TryFindResource("TableEntries");
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
            if (_handle)
            {
                _handle = false;
                ItemsTable.CommitEdit();
                ItemEditEnded?.Invoke(e.Row.Item);
                _handle = true;
            }
        }

        private void GenerateFilterPanel()
        {
            var columnNames = ItemsTable.Columns.Select(column => column.Header.ToString());

            int i = 0;
            foreach (var fieldName in columnNames)
            {
                var textBlock = new TextBlock() { Text = fieldName, Width = 100, Height = 40, TextWrapping = TextWrapping.WrapWithOverflow };
                var textBox = new TextBox() { Name = fieldName.Replace(" ","").Replace(".",""), Width = 100, Height = 20 };
                textBox.LostFocus += FilterChanged;
                FilterPanelTextBox.Children.Add(textBox);
                FilterPanel.Children.Add(textBlock);
                i++;
            }

        }

        private void FilterChanged(object sender, RoutedEventArgs e)
        {
            if (e.Source is TextBox textBox)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    filterColumns.Remove(textBox.Name);
                }
                else
                {
                    if (!filterColumns.TryAdd(textBox.Name, textBox.Text))
                    {
                        filterColumns[textBox.Name] = textBox.Text;
                    }
                }

                Button_Click(null, null);
            }
        }

        private void ItemsTable_AutoGeneratedColumns(object sender, EventArgs e)
        {
            GenerateFilterPanel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tableEntriesCollectionViewSource.View.Filter = new Predicate<object>(Filter?.Invoke(filterColumns.Where(c => !string.IsNullOrEmpty(c.Value)).Select(pair => (pair.Key, pair.Value)).ToArray()));
        }
    }
}
