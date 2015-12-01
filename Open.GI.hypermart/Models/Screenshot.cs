using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Open.GI.hypermart.Models
{
    [Table("Screenshot")]
    public partial class Screenshot
    {
        public int ID { get; set; }

        public int ProductID { get; set; }

        [Column("ScreenShot")]
        [Required]
        public byte[] ScreenShot1 { get; set; }

        public virtual Product Product { get; set; }
    }
}