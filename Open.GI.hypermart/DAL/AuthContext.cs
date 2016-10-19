using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open.GI.hypermart.DAL
{

    /// <summary>
    /// 
    /// </summary>
    public class AuthContext :  IdentityDbContext<IdentityUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthContext"/> class.
        /// </summary>
        public AuthContext()
            : base("AuthContext")
        {

        }
    }
}
