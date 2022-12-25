﻿using StudingWorkloadCalculator.ExcelWriter;
using StudingWorkloadCalculator.Models;
using StudingWorkloadCalculator.SupportClasses;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StudingWorkloadCalculator.MainVewModels
{
    public class DataPresenterViewModel<T> : PropertyChangedNotifier
    {
        private string dataSourcePath;
        private ObservableCollection<T> data;
        private T selectedItem;

        public DataPresenterViewModel()
        {
            DeleteItemCommand = new RelayCommand(DeleteItem);
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
