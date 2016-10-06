using Open.GI.hypermart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Open.GI.hypermart.Attributes
{
    /// <summary>
    /// API identity object
    /// </summary>
    /// <seealso cref="System.Security.Principal.IIdentity" />
    public class ApiIdentity : IIdentity
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public WSUser User
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiIdentity"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public ApiIdentity(WSUser user)
        {
            if (user == null) throw new ArgumentNullException("user");
            this.User = user;
        }

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        public string Name
        {
            get
            {
                return this.User.Username;
            }
        }

        /// <summary>
        /// Gets the type of authentication used.
        /// </summary>
        public string AuthenticationType
        {
            get
            {
                return "Basic";
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }
    }
}
