using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystem.DriversRepository.Core
{
    class DatabaseConnectionFactory
    {
        private const string DbPath = "./ParkingSystem.sqlite";

        public SQLiteConnection Create()
        {
            if (!File.Exists(DbPath))
            {
                SQLiteConnection.CreateFile(DbPath);
            }
            var dbConnection = new SQLiteConnection($"Data Source={DbPath};Version=3;");
            return dbConnection;
        }
    }
}
