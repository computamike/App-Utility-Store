using Open.GI.hypermart.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Open.GI.hypermart.Controllers
{
    /// <summary>
    /// Performs searches against the Product metadata (possibly also against the user database) - to be decided
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class SearchController : Controller
    {
        /// <summary>
        /// DAL
        /// </summary>
        public IHypermartContext db { get; set; }
        /// <summary>
        /// Controller - passing in contrext
        /// </summary>
        /// <param name="db"></param>
        public SearchController(IHypermartContext db)
        {
            this.db = db;
        }


        /// <summary>
        /// Standard installer for Search Controller
        /// </summary>
        public SearchController()
        {

        }
        // GET: Search        
        /// <summary>
        /// Indexes the specified search term. (performs a search, returns a list of results)
        /// </summary>
        /// <param name="SearchTerm">The search term.</param>
        /// <returns></returns>
        public ActionResult Index(string SearchTerm)
        { 

            var TitleMatches  = db.Products.Where(x => x.Title.Contains(SearchTerm.Trim()));
            var DescriptionMatches  = db.Products.Where(x => x.Description.Contains(SearchTerm.Trim()));
            
            var result =TitleMatches.Union(DescriptionMatches);
  





            ViewData.Add("SearchTerm", SearchTerm);
            return View(result);
        }
    }
}