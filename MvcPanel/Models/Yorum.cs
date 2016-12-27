namespace MvcPanel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Yorum")]
    public partial class Yorum
    {
        public int ID { get; set; }

        public string YorumIcerik { get; set; }

        public int? KullaniciID { get; set; }

        public int? MakaleID { get; set; }

        public virtual Makale Makale { get; set; }
    }
}
