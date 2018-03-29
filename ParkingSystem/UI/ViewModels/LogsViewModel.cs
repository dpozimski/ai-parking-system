using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ParkingSystem.UI.ViewModels.Core;
using System.ComponentModel;
using ParkingSystem.ImageHandlerWorker;
using ParkingSystem.DriversRepository.DAL;
using ParkingSystem.UI.Utils;

namespace ParkingSystem.UI.ViewModels
{
    public class LogsViewModel : ViewModelBase
    {
        private LogsDAL _logsDal;
        private WorkerFacade _workerFacade;
        private ObservableCollection<LogViewModel> _logs;

        public ObservableCollection<LogViewModel> Logs
        {
            get => _logs;
            set => this.RaiseAndSetIfChanged(ref _logs, value);
        }

        public LogsViewModel()
        {
            _logsDal = new LogsDAL();
            _workerFacade = new WorkerFacade();
            _workerFacade.NumberPlateArrived += OnNumberPlateArrived;
            _workerFacade.Start();

            Logs = new ObservableCollection<LogViewModel>(_logsDal.GetAll().Select(s => new LogViewModel(s)));
        }

        private void OnNumberPlateArrived(object sender, NumberPlateEventArgs e)
        {
            var vm = new LogViewModel(e.Log);

            RootViewModel.HWND.Dispatcher.BeginInvoke(new Action(() =>
            {
                Logs.Add(vm);

                Toaster.Show(RootViewModel,
                    vm.IsInParkingArea ?
                    $"{e.Owner.FirstName} {e.Owner.LastName} {e.Owner.NumberPlateNumber} arrived" :
                    $"{e.Owner.FirstName} {e.Owner.LastName} {e.Owner.NumberPlateNumber} needs to pay {e.Price.Value.ToString("N2")} PLN");
            }));
        }
    }
}
