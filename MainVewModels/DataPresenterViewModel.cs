using StudingWorkloadCalculator.AccessDataBase;
using StudingWorkloadCalculator.ExcelWriter;
using StudingWorkloadCalculator.Models;
using StudingWorkloadCalculator.SupportClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;

namespace StudingWorkloadCalculator.MainVewModels
{
    public class DataPresenterViewModel<T> : PropertyChangedNotifier
    {
        private string dataSourcePath;
        private ObservableCollection<T> data;
        private T selectedItem;
        private Action<T, bool> acsessDataUpdater;

        public DataPresenterViewModel(Action<T, bool> acsessDataUpdater)
        {
            AddItemCommand = new RelayCommand(AddItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
            this.acsessDataUpdater = acsessDataUpdater;
        }

        public string DataSourcePath
        {
            get => dataSourcePath;
            set
            {
                if (dataSourcePath != value)
                {
                    dataSourcePath = value;
                    OnPropertyChanged();
                    GetDataFromExcel();
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

        public T SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddItemCommand { get; set; }

        public ICommand DeleteItemCommand { get; set; }

        public void AddData(IEnumerable<T> data)
        {
            if (Data is null)
            {
                Data = new ObservableCollection<T>(data);
            }
            else
            {
                foreach (var item in data)
                {
                    Data.Add(item);
                }
            }
        }

        private void AddItem()
        {
            var t = Data.GetType().GetTypeInfo().GenericTypeArguments[0];
            var obj = (dynamic)t.GetConstructor(Array.Empty<Type>()).Invoke(Array.Empty<object>());
            Data.Add(obj);
            acsessDataUpdater?.Invoke(obj, true);
        }

        private void DeleteItem()
        {
            if (SelectedItem != null)
            {
                Data.Remove(SelectedItem);
            }
        }

        private void GetDataFromExcel()
        {
            var data = ExcelReader.ReadExcel<T>(DataSourcePath, startRow: 2, startColumn: 1);
            if (data == null)
            {
                return;
            }

            AddData(data);
        }
    }
}
