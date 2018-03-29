using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystem.ImageHandlerWorker
{
    class PriceCalculator
    {
        public decimal Calculate(DateTime from, DateTime to)
        {
            return Convert.ToDecimal((to - from).TotalHours * 3);
        }
    }
}
