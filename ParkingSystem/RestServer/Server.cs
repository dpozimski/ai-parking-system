using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grapevine.Server;

namespace ParkingSystem.RestServer
{
    public class Server : IDisposable
    {
        private RESTServer server;

        public void Start()
        {
            server = new RESTServer(port: "12358");
            server.Start();

            if(!server.IsListening)
            {
                throw new InvalidOperationException("Server cannot be started");
            }
        }

        public void Dispose()
        {
            server.Dispose();
        }
    }
}
