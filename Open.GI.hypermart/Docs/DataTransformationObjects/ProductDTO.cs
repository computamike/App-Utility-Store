using Open.GI.hypermart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open.GI.hypermart.DataTransformationObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDTO"/> class.
        /// </summary>
        public ProductDTO()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDTO"/> class.
        /// </summary>
        /// <param name="prod">The base.</param>
        public ProductDTO(Product prod)
        {
            this.ID = prod.ID;
            this.Description = prod.Description;
            this.Lead = prod.Lead;
            this.Tagline = prod.Lead;
            this.Title = prod.Title;
            this.ScreenShot = new List<byte[]>();
            foreach (var screenshot in prod.Screenshots )
            {
                this.ScreenShot.Add(screenshot.ScreenShot1);
            }
        }


    


        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the tagline.
        /// </summary>
        /// <value>
        /// The tagline.
        /// </value>
        public string Tagline { get; set; }


        /// <summary>
        /// Gets or sets the lead.
        /// </summary>
        /// <value>
        /// The lead.
        /// </value>
        public string Lead { get; set; }

        /// <summary>
        /// Screen Shot
        /// </summary>
        public List<byte[]> ScreenShot { get; set; }
   

     

    }
}
