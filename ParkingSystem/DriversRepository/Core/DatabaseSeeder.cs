using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystem.DriversRepository.Core
{
    class DatabaseSeeder
    {
        private readonly SQLiteConnection _dbConnection;

        public DatabaseSeeder(SQLiteConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Seed()
        {
            CreateDriverTable();
            CreateLogTable();
        }

        private void CreateLogTable()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS LogEntitys
                    (
                        [Id] INTEGER PRIMARY KEY,
                        [DriverId] INTEGER NOT NULL,
                        [ParentLogId] INTEGER NULL,
                        [Price] REAL NULL,
                        [DateCreated] DATETIME NOT NULL DEFAULT current_timestamp
                    )");
        }

        private void CreateDriverTable()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS DriverEntitys 
                    (
                        [Id] INTEGER PRIMARY KEY,
                        [FirstName] NVARCHAR(64) NOT NULL,
                        [LastName] NVARCHAR(64) NOT NULL,
                        [Email] NVARCHAR(128) NOT NULL,
                        [NumberPlateNumber] NVARCHAR(64) NOT NULL,
                        [DateCreated] DATETIME NOT NULL DEFAULT current_timestamp
                    )");
        }
    }
}
