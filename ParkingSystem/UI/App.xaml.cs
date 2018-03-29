using System;
using System.Threading.Tasks;
using System.Windows;

namespace ParkingSystem.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            //unhandled exception handler
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            var application = new App();

            application.InitializeComponent();
            application.Run();
        }

        #region Private Methods
        /// <summary>
        /// Obsługa unhandled exception.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject != null)
            {
                Exception ex = e.ExceptionObject as Exception;
                MessageBox.Show("There was an exception. \r\n" + ex.Message, "Kolektor danych", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
