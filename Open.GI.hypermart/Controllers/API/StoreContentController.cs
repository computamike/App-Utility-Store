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

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreContentController"/> class.
        /// </summary>
        public StoreContentController()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreContentController"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public StoreContentController(IHypermartContext dbContext)
        {
            db = dbContext;
        }

        private IHypermartContext db = new HypermartContext();

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns></returns>
        public IQueryable<ProductDTO> GetAllProducts()
        {
            var x = from b in db.Products
                    select new ProductDTO
                    {
                        Description = b.Description,
                        ID  = b.ID,
                        Lead = b.Lead,
                        Tagline = b.Tagline ,
                        Title = b.Title 
                    };
            
            return x;

        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns></returns>
        public ProductDTO GetProducts(int id)
        {
          
            Product product = db.Products.Find(id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return new ProductDTO(product);

        }

        /// <summary>
        /// Gets all files for a product.
        /// </summary>
        /// <returns></returns>
        public List<FileDTO> GetFiles(int id)
        {
            var Result = new List<FileDTO>();

            var files = db.Files.Where(x => x.ProductID == id);
           foreach (var item in files)
           {
               Result.Add(new FileDTO(item));
               
           }

           return Result;

        }
            
        /// <summary>
        /// Create a New Product
        /// </summary>
        [HttpPost]
        public ProductDTO PostProduct(Product itemToAdd)
        {
            try
            {
                if (itemToAdd.Screenshots != null && itemToAdd.Screenshots.Count == 0)
                {
                    itemToAdd.Screenshots = null;
                }
                //itemToAdd.Screenshots = new List<Screenshot>();
                //itemToAdd.Files = new List<Open.GI.hypermart.Models.File>();
                //itemToAdd.SourceCode = "";
                db = new HypermartContext();

                var AddedProduct = db.Products.Add(itemToAdd);
                db.SaveChanges();
                return new ProductDTO(AddedProduct);
              }
            catch (Exception ex)
            {

                throw new Exception("Cannot add a product", ex);
            }

        }


        /// <summary>
        /// Posts the product file.
        /// </summary>
        /// <param name="ProductID">The product identifier.</param>
        /// <param name="FileToAdd">The file to add.</param>
        /// <returns></returns>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        /// <exception cref="System.Exception">Cannot add a product file</exception>
        [HttpPost]
        public FileDTO PostProductFile(int ProductID,Open.GI.hypermart.Models.File FileToAdd)
        {
            try
            {
                Product product = db.Products.Find(ProductID);
                if (product == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                FileToAdd.Product = product;

                var AddedFile = db.Files.Add(FileToAdd);
                db.SaveChanges();
                return new FileDTO(AddedFile);
            }
            catch (Exception ex)
            {

                throw new Exception("Cannot add a product file", ex);
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
        /// Deletes the product.
        /// </summary>
        /// <param name="ProductID">The product identifier.</param>
        [HttpPost]
        public void DeleteProduct(int ProductID )
        {
            var productToDelete =  db.Products.Find(ProductID);

            var ps = db.Products;
 

            var res = db.Products.Remove(productToDelete);
            db.SaveChanges();
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

