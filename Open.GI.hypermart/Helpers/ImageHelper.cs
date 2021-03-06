﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Open.GI.hypermart.Helpers
{
    /// <summary>
    /// Image Helpers - manage images.
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// Converts a Image into a base64 representation of an image - probably should consider caching this to disk, and returning an Image element that references the image instead - work on that later
        /// </summary>
        /// <param name="html"></param>
        /// <param name="ImageToConvert">Image element to convert.</param>
        /// <returns>a PNG object that represents the image</returns>
        public static MvcHtmlString ImageToB64(this HtmlHelper html, Image ImageToConvert)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                ImageToConvert.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);

                return new MvcHtmlString(base64String);
            }
        }



        /// <summary>
        /// Images to Base 64 string.
        /// </summary>
        /// <param name="ImageToConvert">The image to convert.</param>
        /// <returns></returns>
        public static string ImageToB64(this Image ImageToConvert)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                ImageToConvert.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);

                return base64String;
            }
        }

        /// <summary>
        /// Images to byte array.
        /// </summary>
        /// <param name="imageIn">The image in.</param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(this System.Drawing.Image  imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        /// <summary>
        /// Tests the extension.
        /// </summary>
        /// <param name="foo">The foo.</param>
        /// <returns></returns>
        public static string TESTExtension(this System.Drawing.Bitmap foo )
        {
            return "hi";
        }

        /// <summary>
        /// Images to byte array.
        /// </summary>
        /// <param name="imageIn">The image in.</param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(this System.Drawing.Bitmap imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        /// <summary>
        /// Bytes the array to image.
        /// </summary>
        /// <param name="byteArrayIn">The byte array in.</param>
        /// <returns></returns>
        public static Image byteArrayToImage(this byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }




    }
}