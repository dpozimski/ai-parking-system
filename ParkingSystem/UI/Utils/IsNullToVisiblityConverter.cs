using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ParkingSystem.UI.Utils
{
    /// <summary>
    /// Class which implmenets an is null conversion to visibility.
    /// </summary>
    /// <CreatedOn>19.11.2017 12:12</CreatedOn>
    /// <CreatedBy>dpozimski</CreatedBy>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    public class IsNullToVisiblityConverter : IValueConverter
    {
        #region [IValueConverter]        
        /// <summary>
        /// Converts a specified value to visibility.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <CreatedOn>19.11.2017 12:12</CreatedOn>
        /// <CreatedBy>dpozimski</CreatedBy>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool invert = false;
            bool.TryParse(parameter as string, out invert);

            if(invert)
                return (value != null) ? Visibility.Collapsed : Visibility.Visible;
            else
                return (value != null) ? Visibility.Visible : Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
        #endregion
    }
}
