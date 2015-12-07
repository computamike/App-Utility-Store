namespace Open.GI.hypermart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Screenshot
    {
        public int ID { get; set; }

        public int ProductID { get; set; }

        [Required]
        public byte[] ScreenShot1 { get; set; }

        public int? Product_ID { get; set; }

        public virtual Product Product { get; set; }
    }
}
