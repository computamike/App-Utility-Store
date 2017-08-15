using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Open.GI.hypermart.DAL;
using Open.GI.hypermart.Models;
using System.IO;

namespace Open.GI.hypermart.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class ProductsController : Controller
    {
        /// <summary>
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public IHypermartContext db { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        public ProductsController()
        {
             //db = new HypermartContext();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public ProductsController(IHypermartContext db)
        {
           this.db = db;
        }
        
        // GET: Products
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.FirstOrDefault(x=> x.ID == id);


            if (product == null)
            {
                return HttpNotFound();
            }

            //Populate the Ratings
            //List<Rating> result = new List<Rating>();
            //var user = User.Identity.Name;
            //var myratings = product.Ratings.Where(x => x.userID == user).OrderBy(x => x.RatingCategory);
            //foreach (Rating Area in myratings)
            //{
            //    result.Add(new Rating() { ProductID = product.ID, RatingCategory = Area.RatingCategory, rating = Area.rating, userID = Area.userID });
            //}
            
            //var rd = new API.RatingsController();
            //var Areas = rd.GetAvailableRatingAreas();
            //List<Rating> AvailableResult = new List<Rating>();
            //foreach (var RatingArea in Areas)
            //{
            //    if (result.SingleOrDefault(t=> t.RatingCategory == RatingArea ) == null)
            //    {
            //        AvailableResult.Add(new Rating() { ProductID = product.ID, RatingCategory = RatingArea, rating = 0, userID = user });
            //    }
            //    else
            //    {
            //        AvailableResult.Add(result.SingleOrDefault(t => t.RatingCategory == RatingArea));
            //    }

                
            //    if (product.Ratings.Where(t => t.RatingCategory == RatingArea).Count() == 0)
            //    {

            //        product.Ratings.Add(new Rating() { ProductID = product.ID, RatingCategory = RatingArea, rating = 0, userID = null });
            //    }

            //}
             

            


            //// My ratings.
            //product.MyRating = AvailableResult; 


            return View(product);
        }

        // GET: Products/Create
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Creates the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,Tagline,SourceCode,Lead,")] Product product)
        {
            if (ModelState.IsValid)
            {
                // add screenshots

                foreach (HttpPostedFileBase file in Request.Files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        file.InputStream.Position = 0;
                        Screenshot Screen = new Screenshot();
                        Screen.ProductID = product.ID;
                        Screen.Product = product;
                        using (var memoryStream = new MemoryStream())
                        {
                            file.InputStream.CopyTo(memoryStream);
                            Screen.ScreenShot1 = memoryStream.ToArray();
                        }
                        product.Screenshots.Add(Screen);
                    }

                }

 
                db.Products.Add(product);
                db.SaveChanges();

                if (Request.Files.Count == 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var t = RedirectToAction("Index");
                    
                    return Json(new { ErrorMessage = "", RedirectURL = Url.Action("Index",null, null, Request.Url.Scheme) });
                }
            }
            return View(product);
        }

        // GET: Products/Edit/5
        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edits the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Version,Description,Lead")] Product product)
        {
            if (ModelState.IsValid)
            {
                var d = db as DbContext;
                d.Entry(product).State = EntityState.Modified;
                d.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
