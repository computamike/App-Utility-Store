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
        
        // GET: Search        
        /// <summary>
        /// Indexes the specified search term. (performs a search, returns a list of results)
        /// </summary>
        /// <param name="SearchTerm">The search term.</param>
        /// <returns></returns>
        public ActionResult Index(string SearchTerm)
        {
            ViewData.Add("SearchTerm", SearchTerm);
            return View();
        }
    }
}