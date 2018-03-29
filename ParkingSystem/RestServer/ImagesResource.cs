using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Grapevine.Server;
namespace ParkingSystem.RestServer
{
    public sealed class ImagesResource : RESTResource
    {
        public static event EventHandler<Image> ImageArrived;

        [RESTRoute(Method = Grapevine.HttpMethod.POST)]
        public void HandleAllGetRequests(HttpListenerContext context)
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    Stream stream = new MemoryStream();
                    await context.Request.InputStream.CopyToAsync(stream);
                    using (Image img = Image.FromStream(stream))
                    {
                        if (img.RawFormat.Equals(ImageFormat.Jpeg))
                        {
                            ImageArrived?.Invoke(this, img);
                            this.SendTextResponse(context, "The image has been proceeded");
                        }
                        else
                            this.SendTextResponse(context, "Image is not in jpg format");
                    }
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentException)
                        this.SendTextResponse(context, "Please send an image in jpg format");
                    else
                        this.SendTextResponse(context, "Unexpected error occured: " + ex.ToString());
                }
            });
        }
    }
}
