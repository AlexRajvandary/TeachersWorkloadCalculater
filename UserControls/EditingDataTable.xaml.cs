using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudingWorkloadCalculator.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class EditingDataTable : UserControl
    {
        public static DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(EditingDataTable), new PropertyMetadata(null));

        public EditingDataTable()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            var attribute = propertyDescriptor.Attributes.OfType<DataGridColumnGeneratorAttribute>().FirstOrDefault();
            if (!attribute?.GenerateColumn ?? false)
            {
                e.Cancel = true;
            }
            else
            {
                e.Column.Header = string.IsNullOrWhiteSpace(attribute?.ColumnName) ? propertyDescriptor.DisplayName : attribute.ColumnName;
            }
        }
    }
}
