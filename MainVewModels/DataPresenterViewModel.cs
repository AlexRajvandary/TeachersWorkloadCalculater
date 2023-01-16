using Microsoft.VisualBasic.Logging;
using StudingWorkloadCalculator.AccessDataBase;
using StudingWorkloadCalculator.ExcelWriter;
using StudingWorkloadCalculator.Models;
using StudingWorkloadCalculator.SupportClasses;
using StudingWorkloadCalculator.UserControls;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Input;
using System.Windows.Shapes;
using static System.Windows.Forms.LinkLabel;

namespace StudingWorkloadCalculator.MainVewModels
{
    public class DataPresenterViewModel<T> : PropertyChangedNotifier
    {
        private string dataSourcePath;
        private ObservableCollection<T> data;
        private T selectedItem;
        private Action<T, bool> acsessDataUpdater;
        private Action<T> acsessDataDelete;
        private Action<string, string> filterData;
        private IEnumerable<(string, string)> propertyDictionary;


        public DataPresenterViewModel(Action<T, bool> acsessDataUpdater, Action<T> acsessDataDelete)
        {
            AddItemCommand = new RelayCommand(AddItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
            this.acsessDataUpdater = acsessDataUpdater;
            this.acsessDataDelete = acsessDataDelete;

            propertyDictionary = typeof(T).
                                      GetTypeInfo().
                                      GetProperties().Where(property => property.GetCustomAttribute(typeof(DataGridColumnGeneratorAttribute)) is DataGridColumnGeneratorAttribute attribute && attribute.GenerateColumn).
                                      Select(property => (property.GetCustomAttribute<DataGridColumnGeneratorAttribute>().ColumnName, property.Name));
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
                acsessDataDelete?.Invoke(SelectedItem);
                Data.Remove(SelectedItem);
            }
        }

        public void UpdateItem(object editedObject)
        {
            var editedItem = (T)editedObject;
            acsessDataUpdater?.Invoke(editedItem, false);
        }

        public Func<object, bool> FilterData((string, string)[] filteredNameAndValue)
        {
            var result = (object obj) =>
            {
                if (obj is T entry)
                {
                    foreach (var ColumnNameAndValueOfFilter in filteredNameAndValue)
                    {
                        var propertyInfo = typeof(T).GetProperties().FirstOrDefault(property => property.Name == propertyDictionary.FirstOrDefault(item => item.Item1.Replace(" ","").Replace(".", "") == ColumnNameAndValueOfFilter.Item1).Item2);
                        var propertyType = propertyInfo?.PropertyType ?? typeof(string);

                        if (propertyType == typeof(string))
                        {
                            var propertyValue = (string?)propertyInfo?.GetValue(entry) ?? string.Empty;
                            var res = !string.IsNullOrEmpty(propertyValue) ? propertyValue.Contains(ColumnNameAndValueOfFilter.Item2 ?? "", StringComparison.OrdinalIgnoreCase) : string.IsNullOrEmpty(ColumnNameAndValueOfFilter.Item2);
                            return res;
                        }
                        else if (propertyType == typeof(int))
                        {
                            var propertyValue = (int?)propertyInfo?.GetValue(entry) ?? 0;

                            return !int.TryParse(ColumnNameAndValueOfFilter.Item2, out var filter) || (filter == 0 || filter == propertyValue);
                        }
                        else if (propertyType == typeof(DateTime))
                        {
                            var propertyValue = (DateTime?)propertyInfo?.GetValue(entry) ?? DateTime.MinValue;
                            var dateTimeString = propertyValue.ToString();
                            return !string.IsNullOrEmpty(dateTimeString) ? dateTimeString.Contains(ColumnNameAndValueOfFilter.Item2 ?? "", StringComparison.OrdinalIgnoreCase) : string.IsNullOrEmpty(ColumnNameAndValueOfFilter.Item2);
                        }
                        else
                        {
                            return false;
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            };

            return result;
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
