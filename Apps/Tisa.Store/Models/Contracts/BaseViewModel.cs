using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tisa.Store.Models.Contracts;

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? property = null)
    {
        if (!string.IsNullOrWhiteSpace(property))
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}