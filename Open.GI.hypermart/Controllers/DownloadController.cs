using Open.GI.hypermart.DAL;
using Open.GI.hypermart.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Open.GI.hypermart.Controllers
{
    /// <summary>
    /// Controller responsible for serving donwload files to the user.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class DownloadController : Controller
    {
        private HypermartContext db = new HypermartContext();
        /// <summary>
        /// Downloads the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Web.HttpException">
        /// Cannot find file 
        /// or
        /// Cannot find file - can't get remote link.
        /// </exception>
        public FileResult Download(int id)
        {

            Open.GI.hypermart.Models.File downloadFile = db.Files.Find(id);
            //write the Installation History....
            InstallationHistory IH = new InstallationHistory();
            IH.InstallationDate = DateTime.Now;
            IH.FileID = downloadFile.ID;
            db.InstallationHistory.Add(IH);
            db.SaveChanges();
            //if (id == null)
            //{
            //    throw new HttpException("Cannot find file ");
            //}
            
            if (downloadFile == null)
            {
                throw new HttpException("Cannot find file - can't get remote link.");
                //return new 
                //return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }


            
 


            //var item = ItemRepo.GetItemById(id);
            //string path = Path.Combine(Server.MapPath("~/App_Data/Items"), item.Path);
            return File(new FileStream(downloadFile.Link, FileMode.Open), "application/octetstream", downloadFile.FileName);
 

        }


      }
}