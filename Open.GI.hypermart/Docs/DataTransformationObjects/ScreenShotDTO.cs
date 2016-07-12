using Open.GI.hypermart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Open.GI.hypermart.Docs.DataTransformationObjects
{
    /// <summary>
    /// ScreenShot DTO
    /// </summary>
    public class ScreenShotDTO
    {
        /// <summary>
        /// ScreenShot DTO Constructor
        /// </summary>
        /// <param name="screenshot"></param>
        public ScreenShotDTO(Screenshot screenshot)
        {
            this.ID = screenshot.ID;
            this.ScreenShot1 = screenshot.ScreenShot1;
        }
    /// <summary>
    /// 
    /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte[]  ScreenShot1 { get; set; }
    }
}