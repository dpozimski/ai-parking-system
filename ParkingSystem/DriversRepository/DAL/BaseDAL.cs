using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ParkingSystem.DriversRepository.Core;
using ParkingSystem.DriversRepository.Entities;

namespace ParkingSystem.DriversRepository.DAL
{
    public class BaseDAL
    {
        protected static SQLiteConnection DbConnection { get; }

        static BaseDAL()
        {
            var factory = new DatabaseConnectionFactory();
            DbConnection = factory.Create();
            var seeder = new DatabaseSeeder(DbConnection);
            seeder.Seed();
        }
    }
}
