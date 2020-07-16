namespace SonsuzBlog.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=SonsuzBlogDB")
        {
        }

        public virtual DbSet<tbl_category> tbl_category { get; set; }
        public virtual DbSet<tbl_comment> tbl_comment { get; set; }
        public virtual DbSet<tbl_etiket> tbl_etiket { get; set; }
        public virtual DbSet<tbl_post> tbl_post { get; set; }
        public virtual DbSet<tbl_rol> tbl_rol { get; set; }
        public virtual DbSet<tbl_sekil> tbl_sekil { get; set; }
        public virtual DbSet<tbl_userrol> tbl_userrol { get; set; }
        public virtual DbSet<tbl_users> tbl_users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_etiket>()
                .HasMany(e => e.tbl_post)
                .WithMany(e => e.tbl_etiket)
                .Map(m => m.ToTable("tbl_post_etiket").MapLeftKey("EtiketId").MapRightKey("PostId"));

            modelBuilder.Entity<tbl_rol>()
                .HasMany(e => e.tbl_userrol)
                .WithRequired(e => e.tbl_rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_users>()
                .HasMany(e => e.tbl_post)
                .WithOptional(e => e.tbl_users)
                .HasForeignKey(e => e.MuellifId);

            modelBuilder.Entity<tbl_users>()
                .HasMany(e => e.tbl_userrol)
                .WithRequired(e => e.tbl_users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_users>()
                .HasMany(e => e.tbl_users1)
                .WithMany(e => e.tbl_users2)
                .Map(m => m.ToTable("tbl_muellif_takip").MapLeftKey("UserId").MapRightKey("MuellifId"));
        }
    }
}
