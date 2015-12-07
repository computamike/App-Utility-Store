using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Open.GI.hypermart.Helpers;
namespace Open.GI.hypermart.DataTransformationObjects
{
    /// <summary>
    /// Class for Over-the-wire transmission of user information.  If a user cannot be found, then an Unknown User will be returned.
    /// </summary>
    public class UserDTO
    {
        public UserDTO()
        {

        }

        public UserDTO(Open.GI.hypermart.Models.User UserToWrap)
        {
            username = UserToWrap.username;
            PhoneNumnber = UserToWrap.PhoneNumnber;
            Photo_byteArray = UserToWrap.Photo.ImageToByteArray();
            Email = UserToWrap.Email;
        }

        public string username { get; set; }
        public string PhoneNumnber { get; set; }
        /// <summary>
        /// Base 64 encoded png image.
        /// </summary>
        public Byte[] Photo_byteArray{ get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
    }
}
