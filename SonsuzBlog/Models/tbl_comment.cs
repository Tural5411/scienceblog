namespace SonsuzBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_comment
    {
        [Key]
        public int CommentId { get; set; }

        public int? PostId { get; set; }

        public string Context { get; set; }

        [StringLength(50)]
        public string Ad { get; set; }

        [StringLength(50)]
        public string Soyad { get; set; }

        public DateTime? Tarixi { get; set; }

        public virtual tbl_post tbl_post { get; set; }
    }
}
