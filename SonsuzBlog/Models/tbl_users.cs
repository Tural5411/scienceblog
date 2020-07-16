namespace SonsuzBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_users()
        {
            tbl_post = new HashSet<tbl_post>();
            tbl_userrol = new HashSet<tbl_userrol>();
            tbl_users1 = new HashSet<tbl_users>();
            tbl_users2 = new HashSet<tbl_users>();
        }

        [Key]
        public int UserId { get; set; }

        [StringLength(50)]
        public string Ad { get; set; }

        [StringLength(50)]
        public string Soyad { get; set; }

        [StringLength(50)]
        public string Login { get; set; }

        [StringLength(500)]
        public string Sifre { get; set; }

        [StringLength(70)]
        public string Mail { get; set; }

        public DateTime? DogumTarixi { get; set; }

        public DateTime? QeydiyyatTarixi { get; set; }

        public int? PhotoId { get; set; }

        public bool? Yazar { get; set; }

        public bool? Aktiv { get; set; }

        public bool? QebulEdildi { get; set; }

        public bool? Cinsi { get; set; }

        [StringLength(300)]
        public string Haqqinda { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_post> tbl_post { get; set; }

        public virtual tbl_sekil tbl_sekil { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_userrol> tbl_userrol { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_users> tbl_users1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_users> tbl_users2 { get; set; }
    }
}
