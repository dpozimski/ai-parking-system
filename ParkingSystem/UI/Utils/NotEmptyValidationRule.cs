using System;
using System.Globalization;
using System.Windows.Controls;

namespace ParkingSystem.UI.Utils
{
    /// <summary>
    /// The not empty field validation rule.
    /// </summary>
    public class NotEmptyValidationRule : ValidationRule
    {
        /// <summary>
        /// The minimum length of the text.
        /// </summary>
        public int MinLength { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = ValidationResult.ValidResult;

            string text = (value ?? "").ToString();

            if (string.IsNullOrEmpty(text))
                result = new ValidationResult(false, "Field is required");
            else if (text.Length < MinLength)
                result = new ValidationResult(false, $"Minimum {MinLength} characters");

            return result;
        }
    }
}
