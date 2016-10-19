using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Open.GI.hypermart.Models
{
    /// <summary>
    /// Model object for storing the history of applications that a user has installed.
    /// </summary>
    public class InstallationHistory
    {
        /// <summary>
        /// Date of installation
        /// </summary>
        public DateTime InstallationDate { get; set; }
        /// <summary>
        /// File Installed
        /// </summary>
        public File InstalledFile { get; set; }

    }
}