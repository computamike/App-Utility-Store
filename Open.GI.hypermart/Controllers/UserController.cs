using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Open.GI.hypermart.Controllers
{
    public class UserController : Controller
    {
        private Open.GI.hypermart.Models.User notFounduser = new Models.User 
        {
            Email = "",
            username = "User Not Found",
            Photo = Properties.Resources.ImageNotFound,
            JobTitle = "Not Found",
            PhoneNumnber = ""        };
 


        [ChildActionOnly]
        public ActionResult Details(string userid)
        {
            Helpers.AD_Repository sdr = new  Helpers.AD_Repository();
            var user = sdr.getUser(userid);
            if (user == null)
                return PartialView(notFounduser);
            return PartialView(user);
        }

    }
}