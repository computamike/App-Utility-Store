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
using Open.GI.hypermart.Helpers;
using Open.GI.hypermart.DataTransformationObjects;

namespace Open.GI.hypermart.Controllers
{
    /// <summary>
    /// This is the OpenGI API layer for interacting with the Store(Ading content etc).  Some of the calls here relating to updates and creation will require a session token.
    /// </summary>
    public class StoreContentController : ApiController
    {
        private HypermartContext db = new HypermartContext();

        public IQueryable<ProductDTO> GetAllProducts()
        {
            var x = from b in db.Products
                    select new ProductDTO()
                    {
                        ID = b.ID,
                        Description = b.Description,
                        Lead = b.Lead,
                        Tagline = b.Tagline,
                        Title = b.Title
                    };
            
            return x;

        }
        
        
        /// <summary>
        /// Create a New Product
        /// </summary>
        [HttpPost]
        public ProductDTO AddProduct(Product itemToAdd)
        {
            try
            {
                if (itemToAdd.Screenshots.Count == 0)
                {
                    itemToAdd.Screenshots = null;
                }
                var AddedProduct = db.Products.Add(itemToAdd);
                db.SaveChanges();
                return new ProductDTO()
                {
                    ID = AddedProduct.ID,
                    Lead = AddedProduct.Lead,
                    Description = AddedProduct.Description,
                    Tagline = AddedProduct.Tagline,
                    Title = AddedProduct.Title,
                };
            }
            catch (Exception ex)
            {

                throw new Exception("Cannot add a product", ex);
            }

        }

        /// <summary>
        /// Add A File to a product
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="FileToAdd"></param>
        /// <returns></returns>
        [HttpPost]
        public FileDTO AddFile(int ProductID, Open.GI.hypermart.Models.File FileToAdd )
        {
            var AddedFile = db.Files.Add(FileToAdd);
            return new FileDTO() 
            { 
                ID = AddedFile.ID, 
                BLOB = AddedFile.BLOB, 
                FileName = AddedFile.FileName, 
                Link = AddedFile.Link, 
                Platforms = AddedFile.Platforms, 
                Product = AddedFile.Product, 
                ProductID = AddedFile.ProductID, 
                StorageType = AddedFile.StorageType, 
                Version = AddedFile.Version 
            };
        }

        
        /// <summary>
        /// Add a Screenshot to an Product
        /// </summary>
        /// <param name="ProductID"></param>
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
                        product.Screenshots.Add(new Screenshot{ScreenShot1 = z});
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

