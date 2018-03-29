using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ParkingSystem.DriversRepository.Entities;
using ParkingSystem.UI.Utils;
using ParkingSystem.UI.ViewModels.Core;

namespace ParkingSystem.UI.ViewModels
{
    public class DriverViewModel : ViewModelBase
    {
        private ValidationRule _validationRule;
        private bool _isValid;

        public DriverEntity Model { get; }

        public DateTime CreatedDate
        {
            get => Model.DateCreated;
            set
            {
                Model.DateCreated = value;
                RaisePropertyChanged();
            }
        }

        public string FirstName
        {
            get => Model.FirstName;
            set
            {
                Model.FirstName = value;
                RaisePropertyChanged();
                Validate();
            }
        }

        public string LastName
        {
            get => Model.LastName;
            set
            {
                Model.LastName = value;
                RaisePropertyChanged();
                Validate();
            }
        }

        public string Email
        {
            get => Model.Email;
            set
            {
                Model.Email = value;
                RaisePropertyChanged();
                Validate();
            }
        }

        public string NumberPlateNumber
        {
            get => Model.NumberPlateNumber;
            set
            {
                Model.NumberPlateNumber = value;
                RaisePropertyChanged();
                Validate();
            }
        }

        public bool IsValid
        {
            get => _isValid;
            set
            {
                _isValid = value;
                RaisePropertyChanged();
            }
        }

        public DriverViewModel(DriverEntity driverEntity = null)
        {
            _validationRule = new EmailValidationRule();
            Model = driverEntity ?? new DriverEntity();
        }

        private void Validate()
        {
            IsValid = !string.IsNullOrEmpty(FirstName) &&
                !string.IsNullOrEmpty(LastName) &&
                !string.IsNullOrEmpty(NumberPlateNumber) &&
                _validationRule.Validate(Email, CultureInfo.InvariantCulture).IsValid;
        }
    }
}
