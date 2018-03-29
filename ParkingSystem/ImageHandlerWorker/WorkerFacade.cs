using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingSystem.DriversRepository.DAL;
using ParkingSystem.DriversRepository.Entities;
using ParkingSystem.RestServer;

namespace ParkingSystem.ImageHandlerWorker
{
    public class WorkerFacade : IDisposable
    {
        private PriceCalculator _priceCalculator;
        private EmailSender _emailSender;
        private LicensePlateCharsExtractorProxy _licensePlateCharsExtractorProxy;
        private Server _server;
        private LogsDAL _logsDal;
        private DriversDAL _driversDal;

        public event EventHandler<NumberPlateEventArgs> NumberPlateArrived;
        
        public void Start()
        {
            _priceCalculator = new PriceCalculator();
            _emailSender = new EmailSender();
            _licensePlateCharsExtractorProxy = new LicensePlateCharsExtractorProxy();
            _driversDal = new DriversDAL();
            _logsDal = new LogsDAL();
            _server = new Server();
            _server.Start();
            ImagesResource.ImageArrived += OnImageArrived;
        }

        public void Dispose()
        {
            _server.Dispose();
            ImagesResource.ImageArrived -= OnImageArrived;
        }

        private void OnImageArrived(object sender, Image e)
        {
            decimal? price = null;
            var numberPlate = _licensePlateCharsExtractorProxy.Extract(e);
            var driver = _driversDal.GetByNumberPlateNumber(numberPlate);
            //there is no driver with this plates
            if (driver is null)
                throw new Exception("There is no driver with number plate " + numberPlate);
            //check for staying in parking area
            var date = DateTime.Now;
            var enterLog = CheckIfCarIsInParkingArea(numberPlate);
            //calculate price
            if(enterLog != null)
                price = _priceCalculator.Calculate(enterLog.DateCreated, date);
            //insert log
            var log = InsertLog(driver, numberPlate, enterLog, price, date);
            //detected enterLog that means we can send the invoice
            if(enterLog != null)
                _emailSender.Send(driver, enterLog, log, price.Value);
            //inform observerss
            NumberPlateArrived?.Invoke(this, new NumberPlateEventArgs()
            {
                Log = log,
                Owner = driver,
                Price = price
            });
        }

        private LogEntity InsertLog(DriverEntity driver, string numberPlate, LogEntity enterLog, decimal? price, DateTime date)
        {
            var entity = new LogEntity()
            {
                DriverId = driver.Id,
                ParentLogId = enterLog?.Id,
                Price = price,
                DateCreated = date
            };
            _logsDal.Insert(entity);
            return entity;
        }

        private LogEntity CheckIfCarIsInParkingArea(string numberPlate)
        {
            //Try to get is in parking area log
            LogEntity tmpEnterLog = _logsDal.GetByNumberPlateNumber(numberPlate);
            if (tmpEnterLog != null && !tmpEnterLog.ParentLogId.HasValue)
                return tmpEnterLog;
            else
                return null;
        }
    }
}
