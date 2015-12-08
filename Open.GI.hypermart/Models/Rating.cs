using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Open.GI.hypermart.Models
{
    public class Rating
    {
        [Key, Column(Order = 0)]
        public string userID { get; set; }
        [Key, Column(Order = 1)]
        public Category RatingCategory { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        public int rating { get; set; }

        public string Comments { get; set; }

    }
}