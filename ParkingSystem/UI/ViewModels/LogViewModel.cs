using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingSystem.DriversRepository.DAL;
using ParkingSystem.DriversRepository.Entities;
using ParkingSystem.UI.ViewModels.Core;

namespace ParkingSystem.UI.ViewModels
{
    public class LogViewModel : ViewModelBase
    {
        private DriversDAL _driverDal;
        private DriverEntity _driverEntity;
        private LogEntity _logEntity;

        public DateTime CreatedDate
        {
            get => _logEntity.DateCreated;
        }

        public decimal? Price
        {
            get => _logEntity.Price;
        }

        public string NumberPlateNumber
        {
            get => _driverEntity.NumberPlateNumber;
        }

        public string FirstName
        {
            get => _driverEntity.FirstName;
        }

        public string LastName
        {
            get => _driverEntity.LastName;
        }

        public bool IsInParkingArea
        {
            get => !_logEntity.ParentLogId.HasValue;
        }

        public LogViewModel(LogEntity logEntity)
        {
            _driverDal = new DriversDAL();
            _logEntity = logEntity;
            _driverEntity = _driverDal.GetById(logEntity.DriverId);
        }
    }
}
