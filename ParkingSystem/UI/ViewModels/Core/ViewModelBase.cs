using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ParkingSystem.UI.Utils;

namespace ParkingSystem.UI.ViewModels.Core
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public static ShellViewModel RootViewModel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaiseAndSetIfChanged<T>(ref T refValue, T value, [CallerMemberName] string propertyName = null)
        {
            if (!ReferenceEquals(refValue, value))
            {
                refValue = value;
                RaisePropertyChanged(propertyName);
            } 
        }

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
