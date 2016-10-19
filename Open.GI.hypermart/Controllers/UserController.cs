using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Open.GI.hypermart.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class UserController : Controller
    {
        private Open.GI.hypermart.Models.User notFounduser = new Models.User 
        {
            Email = "",
            username = "User Not Found",
            Photo = Properties.Resources.ImageNotFound,
            JobTitle = "Not Found",
            PhoneNumnber = ""        };

        /// <summary>
        /// Detailses the specified userid.
        /// </summary>
        /// <param name="id">The userid.</param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            notFounduser.username = id;
            try
            {

                var user = Helpers.AD_Repository.getUser(id);
                if (user == null)
                    return View(notFounduser);
                return View("Details",user);
            }
            catch (Exception)
            {

                return View(notFounduser);
            }




             
        }

        /// <summary>
        /// Detailses the specified userid.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult popup_Details(string userid)
        {
            notFounduser.username = userid;
            try
            {
                
                var user = Helpers.AD_Repository.getUser(userid);
                if (user == null)
                    return PartialView(notFounduser);
                return PartialView(user);
                }
            catch (Exception)
            {

                return PartialView(notFounduser);
            }

        }



    }
}