using System.Globalization;
using System.Windows.Controls;

namespace ParkingSystem.UI.Utils
{
    /// <summary>
    /// Class which implements the only integer validation rule.
    /// </summary>
    public class OnlyIntegerValidationRule : ValidationRule
    {
        /// <summary>
        /// The minimum value.
        /// </summary>
        public int MinValue { get; set; }
        /// <summary>
        /// The maximum value.
        /// </summary>
        public int MaxValue { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = ValidationResult.ValidResult;

            int iValue = -1;
            bool success = int.TryParse(value.ToString(), NumberStyles.Any, cultureInfo, out iValue);
            if (success && (iValue < MinValue || iValue > MaxValue))
                result = new ValidationResult(false, string.Format("Allowed range: {0} - {1}", MinValue, MaxValue));
            else if(!success)
                result = new ValidationResult(false, "Only integer values allowed");

            return result;
        }
    }
}
