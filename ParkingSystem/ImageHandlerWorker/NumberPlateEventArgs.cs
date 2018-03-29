using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingSystem.DriversRepository.Entities;

namespace ParkingSystem.ImageHandlerWorker
{
    public class NumberPlateEventArgs
    {
        public LogEntity Log { get; set; }
        public DriverEntity Owner { get; set; }
        public decimal? Price { get; set; }
    }
}
