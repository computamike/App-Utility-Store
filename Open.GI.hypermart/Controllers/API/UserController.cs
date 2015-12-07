using Open.GI.hypermart.DataTransformationObjects;
using Open.GI.hypermart.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Open.GI.hypermart.Helpers;
namespace Open.GI.hypermart.Controllers.API
{
    /// <summary>
    /// This is the OpenGI API layer for interacting with users.  This API layer allows user details to be retireved from Active Directory.
    /// </summary>
    /// <remarks>
    /// Required minimal configuration, but does require a directory services of sorts - this should work with the Active Directory Lightweight Directory Services (to be confirmed).
    /// </remarks>
    public class UserController : ApiController
    {
        private UserDTO notFounduser = new UserDTO
        {
            Email = "",
            username = "User Not Found",

            Photo_byteArray = Properties.Resources.ImageNotFound.ImageToByteArray(),
            JobTitle = "Not Found",
            PhoneNumnber = ""
        };

        /// <summary>
        /// Retrieves the user information from the directory.
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet]
        public UserDTO Details(string userid)
        {
  
            notFounduser.username = userid;
            try
            {
                Helpers.AD_Repository sdr = new Helpers.AD_Repository();
                var user = sdr.getUser(userid);
                if (user == null)
                    return  notFounduser;
                return new UserDTO(user);
            }
            catch (Exception)
            {

                return notFounduser;
            }

        }
    }
}
