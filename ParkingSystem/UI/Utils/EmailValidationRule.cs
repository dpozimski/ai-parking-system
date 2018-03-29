using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Annotations = System.ComponentModel.DataAnnotations;

namespace ParkingSystem.UI.Utils
{
    public class EmailValidationRule : NotEmptyValidationRule
    {
        private Annotations.EmailAddressAttribute _emailAddressAttribute = new Annotations.EmailAddressAttribute();

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = base.Validate(value, cultureInfo);

            string text = (value ?? "").ToString();

            if (result == ValidationResult.ValidResult && !_emailAddressAttribute.IsValid(text))
            {
                result = new ValidationResult(false, "Incorrect address");
            }

            return result;
        }
    }
}
