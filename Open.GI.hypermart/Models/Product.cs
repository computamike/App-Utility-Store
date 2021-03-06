namespace Open.GI.hypermart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    /// <summary>
    /// Product Model Class
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            //AverageRating = new List<RatingDetails>();
            MyRating = new List<Rating>();
            Files = new HashSet<File>();
            Screenshots = new HashSet<Screenshot>();
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
        [StringLength(50)]
        public string Lead { get; set; }

        /// <summary>
        /// Gets or sets the source code location for the project.
        /// </summary>
        /// <value>
        /// The source code.
        /// </value>
        public string SourceCode { get; set; }

        /// <summary>
        /// Gets or sets the maintainers.
        /// </summary>
        /// <value>
        /// The maintainers.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<string> Maintainers { get; set; }
        

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>
        /// The files.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<File> Files { get; set; }

        /// <summary>
        /// Gets or sets the screenshots.
        /// </summary>
        /// <value>
        /// The screenshots.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Screenshot> Screenshots { get; set; }
        /// <summary>
        /// Gets or sets the ratings.
        /// </summary>
        /// <value>
        /// The ratings.
        /// </value>
        public virtual ICollection<Rating> Ratings{ get; set; }

        // <summary>
        // Gets or sets the ratings Detail.
        // </summary>
        // <value>
        // The ratings.
        // </value>
        //public virtual ICollection<RatingDetails> RatingsDetail { get; set; }
        private ICollection<Rating> CalcuateAverageRating()
        {
            // work out the average based on the ratings...
            List<Rating> AverageResults = new List<Rating>();
            if (this.Ratings != null)
            {
                var RatingAreas = this.Ratings.Select(x => x.RatingCategory).Distinct();
                foreach (var Area in RatingAreas)
                {
                    double avg = 0;
                    if (this.Ratings.Where(x => x.RatingCategory == Area).Where(t => t.rating != 0).Count() != 0)
                    {
                        avg = this.Ratings.Where(x => x.RatingCategory == Area).Where(t => t.rating != 0).Average(t => t.rating);
                    }
                    AverageResults.Add(new Rating() { ProductID = this.ID, RatingCategory = Area, rating = avg });
                }
            }
            return AverageResults;
        }


        /// <summary>
        /// Gets the average ratings for all 
        /// </summary>
        /// <value>
        /// The average rating.
        /// </value>
        [NotMapped]
        public virtual ICollection<Rating> AverageRating
        {
            get
            {
                List<Rating> averages = new List<Rating>();
                averages.Add(new Rating {  RatingCategory = "foo", rating = 4.6});
                averages.Add(new Rating { RatingCategory = "bar", rating = 2.6 });
                return averages;
                //return CalcuateAverageRating();
            }
            set{}


        }
        /// <summary>
        /// Gets my rating.
        /// </summary>
        /// <value>
        /// My rating.
        /// </value>
        [NotMapped]
        public virtual ICollection<Rating> MyRating
        {
            get;
            set;
        }

       

    }
}
