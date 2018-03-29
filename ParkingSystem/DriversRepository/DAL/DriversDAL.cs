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
    public class DriversDAL : BaseDAL
    {
        public IEnumerable<DriverEntity> GetAll()
        {
            return DbConnection.Query<DriverEntity>("SELECT * FROM DriverEntitys");
        }

        public DriverEntity GetById(int driverId)
        {
            return DbConnection.QueryFirstOrDefault<DriverEntity>(
                $"SELECT * FROM DriverEntitys WHERE Id = {driverId}");
        }

        public DriverEntity GetByNumberPlateNumber(string numberPlateNumber)
        {
            return DbConnection.QueryFirstOrDefault<DriverEntity>(
                $"SELECT * FROM DriverEntitys WHERE NumberPlateNumber = '{numberPlateNumber}'");
        }

        public void Insert(DriverEntity entity)
        {
            entity.DateCreated = DateTime.Now;
            DbConnection.Insert(entity);
        }

        public void Remove(DriverEntity entity)
        {
            DbConnection.ExecuteScalar($@"
                    DELETE FROM DriverEntitys
                    WHERE Id = {entity.Id}");
            DbConnection.ExecuteScalar($@"
                    DELETE FROM LogEntitys
                    WHERE DriverId = {entity.Id}");
        }
    }
}
