using System.Net.Mail;
using ParkingSystem.DriversRepository.Entities;

namespace ParkingSystem.ImageHandlerWorker
{
    class EmailSender
    {
        private const string _emailAddress = "ai.parking.system.polsl@wp.pl";
        private const string _emailPwd = "test1234%";
        private const string _smtpHost = "smtp.wp.pl";
        private const string _mailSubject = "You need to pay for my awesome parking stop!!";
        private const string _mailName = "AI Parking System Polsl";
        private readonly string _mailBody = Properties.Resources.MailBody;
        private const int _smtpPort = 587;

        public void Send(DriverEntity driver, LogEntity entry, LogEntity exit, decimal price)
        {
            MailMessage newMail = new MailMessage();
            newMail.To.Add(driver.Email);
            newMail.Subject = _mailSubject;
            newMail.Body = string.Format(_mailBody, entry.DateCreated, exit.DateCreated, price.ToString("N2"));
            newMail.From = new MailAddress(_emailAddress, _mailName);
            newMail.IsBodyHtml = true;

            SmtpClient smtpSender = new SmtpClient();
            smtpSender.Credentials = new System.Net.NetworkCredential(_emailAddress, _emailPwd);
            smtpSender.Port = _smtpPort;
            smtpSender.Host = _smtpHost;
            smtpSender.EnableSsl = true;
            smtpSender.Send(newMail);
        }
    }
}