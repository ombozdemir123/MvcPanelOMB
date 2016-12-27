namespace MvcPanel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Yetkilendirme")]
    public partial class Yetkilendirme
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Yetkilendirme()
        {
            Kullanicis = new HashSet<Kullanici>();
        }

        [Key]
        public int YetkiID { get; set; }

        [Required]
        [StringLength(50)]
        public string YetkiAdi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kullanici> Kullanicis { get; set; }
    }
}
