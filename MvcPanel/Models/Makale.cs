namespace MvcPanel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Makale")]
    public partial class Makale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Makale()
        {
            Yorums = new HashSet<Yorum>();
            Etikets = new HashSet<Etiket>();
        }

        public int ID { get; set; }

        [StringLength(150)]
        public string Baslik { get; set; }

        public string Icerik { get; set; }

        public int? KullaniciID { get; set; }

        public int? KategoriID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Tarih { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorum> Yorums { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Etiket> Etikets { get; set; }
    }
}
