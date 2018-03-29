using MahApps.Metro.Controls;
using System.Drawing;
using ParkingSystem.UI.ViewModels.Core;

namespace ParkingSystem.UI.Views.Core
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : MetroWindow
    {
        public Shell()
        {
            DataContext = new ShellViewModel();
            (DataContext as ShellViewModel).HWND = this;
            InitializeComponent();
        }
    }
}
