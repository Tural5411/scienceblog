namespace SonsuzBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_category()
        {
            tbl_post = new HashSet<tbl_post>();
        }

        [Key]
        public int CategoryId { get; set; }

        [StringLength(100)]
        public string Ad { get; set; }

        public int? PhotoId { get; set; }

        [StringLength(500)]
        public string Aciqlama { get; set; }

        [StringLength(500)]
        public string Keyword { get; set; }

        public virtual tbl_sekil tbl_sekil { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_post> tbl_post { get; set; }
    }
}
