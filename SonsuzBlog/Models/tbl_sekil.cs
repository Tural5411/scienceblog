namespace SonsuzBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_sekil
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_sekil()
        {
            tbl_category = new HashSet<tbl_category>();
            tbl_post = new HashSet<tbl_post>();
            tbl_users = new HashSet<tbl_users>();
        }

        [Key]
        public int PhotoId { get; set; }

        [StringLength(500)]
        public string Kicik { get; set; }

        [StringLength(500)]
        public string Orta { get; set; }

        [StringLength(500)]
        public string Boyuk { get; set; }

        public int? PostId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_category> tbl_category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_post> tbl_post { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_users> tbl_users { get; set; }
    }
}
