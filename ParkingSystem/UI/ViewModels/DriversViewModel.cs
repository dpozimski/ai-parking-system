using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ParkingSystem.UI.ViewModels.Core;
using System.ComponentModel;
using ParkingSystem.DriversRepository.DAL;
using ParkingSystem.DriversRepository.Entities;
using ParkingSystem.UI.Utils;

namespace ParkingSystem.UI.ViewModels
{
    public class DriversViewModel : ViewModelBase
    {
        private DriversDAL _driversDal;
        private ObservableCollection<DriverViewModel> _drivers;
        private DriverViewModel _selectedDriver;
        private DriverViewModel _newDriver;

        public ObservableCollection<DriverViewModel> Drivers
        {
            get => _drivers;
            set => this.RaiseAndSetIfChanged(ref _drivers, value);
        }
        public DriverViewModel SelectedDriver
        {
            get => _selectedDriver;
            set => this.RaiseAndSetIfChanged(ref _selectedDriver, value);
        }
        public DriverViewModel NewDriver
        {
            get => _newDriver;
            set => this.RaiseAndSetIfChanged(ref _newDriver, value);
        }

        public ICommand RemoveDriverCommand { get; }
        public ICommand AddDriverCommand { get; }

        public DriversViewModel()
        {
            _newDriver = new DriverViewModel();
            _driversDal = new DriversDAL();
            AddDriverCommand = new RelayCommand<object>(s => AddDriver());
            RemoveDriverCommand = new RelayCommand<object>(s => RemoveDriver());

            Drivers = new ObservableCollection<DriverViewModel>(_driversDal.GetAll().Select(s => new DriverViewModel(s)));
        }

        private void RemoveDriver()
        {
            _driversDal.Remove(SelectedDriver.Model);
            Drivers.Remove(SelectedDriver);
            SelectedDriver = Drivers.FirstOrDefault();
        }

        private void AddDriver()
        {
            if(_driversDal.GetByNumberPlateNumber(NewDriver.NumberPlateNumber) != null)
            {
                Toaster.Show(RootViewModel, "Driver with this number plate already exists.");
                return;
            }

            var entity = new DriverEntity()
            {
                Email = NewDriver.Email,
                FirstName = NewDriver.FirstName,
                LastName = NewDriver.LastName,
                NumberPlateNumber = NewDriver.NumberPlateNumber
            };

            _driversDal.Insert(entity);

            Drivers.Add(new DriverViewModel(entity));

            NewDriver = new DriverViewModel();
        }
    }
}
