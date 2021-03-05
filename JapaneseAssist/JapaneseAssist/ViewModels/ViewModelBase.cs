using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JapaneseAssist.ViewModels
{
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Activates when a new value has been assigned to a property
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// A method that activates the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
