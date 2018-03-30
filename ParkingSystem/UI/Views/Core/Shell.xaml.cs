using MahApps.Metro.Controls;
using System.Drawing;
using ParkingSystem.UI.ViewModels.Core;
using System.ComponentModel;
using System;

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
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            (LogsView.DataContext as IDisposable).Dispose();
        }
    }
}
