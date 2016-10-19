using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Open.GI.hypermart.Controllers
{
    /// <summary>
    /// Controller for returning Errors
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class FakeController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        // GET: Fake        
        public ActionResult Index()
        {
            return View();
        }
    }
}