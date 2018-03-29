using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hardcodet.Wpf.TaskbarNotification;
using ParkingSystem.UI.ViewModels.Core;

namespace ParkingSystem.UI.Utils
{
    public static class Toaster
    {
        public static void Show(ShellViewModel rootVm, string message)
        {
            var icon = BalloonIcon.Info;
            rootVm.HWND.Dispatcher.BeginInvoke(new Action(() =>
                rootVm.HWND.TaskbarIcon.ShowBalloonTip("ArcadiaSync", message, icon)));
        }
    }
}
