using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ParkingSystem.UI.Views.Core;

namespace ParkingSystem.UI.ViewModels.Core
{
    public class ShellViewModel : ViewModelBase
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        public ShellViewModel()
        {
            InitTimer();
            RootViewModel = this;
        }

        private void InitTimer()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += (o, e) => this.RaisePropertyChanged(nameof(Now));
            timer.Start();
        }
    }
}
