using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Homework_22_WPF.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value)) 
                return false;

            field = value;
            OnPropertyChanged(propertyName);

            return true;
        }
    }
}
