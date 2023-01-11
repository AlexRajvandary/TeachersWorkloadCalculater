using StudingWorkloadCalculator.AccessDataBase;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudingWorkloadCalculator.Models
{
    public class PropertyChangedNotifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
