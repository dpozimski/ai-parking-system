using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystem.DriversRepository.Entities
{
    public class LogEntity : BaseEntity
    {
        public int DriverId { get; set; }
        public int? ParentLogId { get; set; }
        public decimal? Price { get; set; }
    }
}
