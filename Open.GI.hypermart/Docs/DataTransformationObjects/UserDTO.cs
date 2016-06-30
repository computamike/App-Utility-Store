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
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDTO"/> class.
        /// </summary>
        public UserDTO()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDTO"/> class.
        /// </summary>
        /// <param name="UserToWrap">The user to wrap.</param>
        public UserDTO(Open.GI.hypermart.Models.User UserToWrap)
        {
            username = UserToWrap.username;
            PhoneNumnber = UserToWrap.PhoneNumnber;
            Photo_byteArray = UserToWrap.Photo.ImageToByteArray();
            Email = UserToWrap.Email;
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string username { get; set; }
        /// <summary>
        /// Gets or sets the phone numnber.
        /// </summary>
        /// <value>
        /// The phone numnber.
        /// </value>
        public string PhoneNumnber { get; set; }
        /// <summary>
        /// Base 64 encoded png image.
        /// </summary>
        public Byte[] Photo_byteArray{ get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the job title.
        /// </summary>
        /// <value>
        /// The job title.
        /// </value>
        public string JobTitle { get; set; }
    }
}
