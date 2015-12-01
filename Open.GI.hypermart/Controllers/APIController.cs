using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Open.GI.hypermart.Models;
using Open.GI.hypermart.DAL;
using System.IO;
using System.Drawing;
using Open.GI.hypermart.Models;
using Open.GI.hypermart.Helpers;

namespace Open.GI.hypermart.Controllers
{
    public class APIController : ApiController
    {
        private HypermartContext db = new HypermartContext();

        public IEnumerable<Product> GetAllProducts()
        {
            return db.Products;
        }
        
        [HttpPost]
        public void AddScreenShot(int ProductID)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            if (Request.Content.IsMimeMultipartContent())
            {
                Request.Content.ReadAsMultipartAsync<MultipartMemoryStreamProvider>(new MultipartMemoryStreamProvider()).ContinueWith((task) =>
                {
                    MultipartMemoryStreamProvider provider = task.Result;
                    foreach (HttpContent content in provider.Contents)
                    {
                        Stream stream = content.ReadAsStreamAsync().Result;
                        var image = (Bitmap)Bitmap.FromStream(stream);
                        Product product = db.Products.Find(ProductID);
                        var z = image.ImageToByteArray();
                        product.ScreenShots.Add(new Screenshot{ScreenShot1 = z});
                        db.SaveChanges();
                    }
                });
            return ;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }
    }
}

