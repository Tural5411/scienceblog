namespace SonsuzBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_post()
        {
            tbl_comment = new HashSet<tbl_comment>();
            tbl_etiket = new HashSet<tbl_etiket>();
        }

        [Key]
        public int PostId { get; set; }

        public int? MuellifId { get; set; }

        [StringLength(300)]
        public string Baslig { get; set; }

        public string Context { get; set; }

        public int? PhotoId { get; set; }

        public int? CategoryId { get; set; }

        public DateTime? Tarixi { get; set; }

        public int? Baxis { get; set; }

        public int? Beyenme { get; set; }

        [StringLength(500)]
        public string Keyword { get; set; }

        public bool? QebulEdildi { get; set; }

        public bool? Aktiv { get; set; }

        [StringLength(300)]
        public string Ozet { get; set; }

        public virtual tbl_category tbl_category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_comment> tbl_comment { get; set; }

        public virtual tbl_sekil tbl_sekil { get; set; }

        public virtual tbl_users tbl_users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_etiket> tbl_etiket { get; set; }
    }
}
