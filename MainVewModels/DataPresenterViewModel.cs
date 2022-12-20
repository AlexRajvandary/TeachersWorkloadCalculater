using StudingWorkloadCalculator.ExcelWriter;
using StudingWorkloadCalculator.Models;
using System.Collections.ObjectModel;

namespace StudingWorkloadCalculator.MainVewModels
{
    public class DataPresenterViewModel<T> : PropertyChangedNotifier
    {
        private string dataSourcePath;
        private ObservableCollection<T> data;

        public string DataSourcePath
        {
            get => dataSourcePath;
            set
            {
                if(dataSourcePath != value)
                {
                    dataSourcePath = value;
                    OnPropertyChanged();
                    GetData();
                }
            }
        }

        public ObservableCollection<T> Data
        {
            get => data;
            set
            {
                data = value;
                OnPropertyChanged();
            }
        }

        private void GetData()
        {
            var data = ExcelReader.ReadExcel<T>(DataSourcePath, startRow: 2, startColumn: 1);
            Data = new ObservableCollection<T>(data);
        }
    }
}
