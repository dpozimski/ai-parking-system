using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using ParkingSystem.DriversRepository.Entities;

namespace ParkingSystem.DriversRepository.DAL
{
    public class LogsDAL : BaseDAL
    {
        public IEnumerable<LogEntity> GetAll()
        {
            return DbConnection.Query<LogEntity>("SELECT * FROM LogEntitys");
        }

        public LogEntity GetByNumberPlateNumber(string numberPlateNumber)
        {
            return DbConnection.QueryFirstOrDefault<LogEntity>(
                $"SELECT * FROM LogEntitys " +
                $"INNER JOIN DriverEntitys ON LogEntitys.DriverId = DriverEntitys.Id " +
                $"WHERE DriverEntitys.NumberPlateNumber = '{numberPlateNumber}' " +
                $"ORDER BY LogEntitys.DateCreated DESC;");
        }

        public void Insert(LogEntity entity)
        {
            DbConnection.Insert(entity);
        }
    }
}
