using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Open.GI.hypermart.Models
{
    /// <summary>
    /// Type of storage used.
    /// </summary>
    public enum storageType
    {
        /// <summary>
        /// The internal BLOB store 
        /// </summary>
        InternalBLOB,
        /// <summary>
        /// The remote share
        /// </summary>
        RemoteShare,

    }
}