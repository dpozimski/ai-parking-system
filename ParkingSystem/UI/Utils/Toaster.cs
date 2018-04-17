using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using ParkingSystem.UI.ViewModels.Core;
using ParkingSystem.UI.Views.Core;

namespace ParkingSystem.UI.Utils
{
    public static class Toaster
    {
        public static void Show(ShellViewModel rootVm, string message)
        {
            var icon = BalloonIcon.Info;
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                ((Shell)Application.Current.MainWindow).TaskbarIcon.ShowBalloonTip("ParkingSystem", message, icon);
            }));     
        }
    }
}
