using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace USWR.Models
{
    public partial class USWRContext : DbContext
    {
        public USWRContext()
        {
        }

        public USWRContext(DbContextOptions<USWRContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Ratings> Ratings { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Sites> Sites { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-72UGUF6\\ARVALOR;DataBase=USWR;user=USWR;password=123456789876543;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminLogin = "Admin";
            string adminPassword = "123456";

            // добавляем роли
            Roles adminRole = new Roles { Id = 1, Role = adminRoleName };
            Roles userRole = new Roles { Id = 2, Role = userRoleName };
            Users adminUser = new Users { Id = new Guid("9cb6ca17-f555-4b3e-b0cf-e3d664b3cbf6"), Login = adminLogin, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Roles>().HasData(new Roles[] { adminRole, userRole });
            modelBuilder.Entity<Users>().HasData(new Users[] { adminUser });
            modelBuilder.Entity<Comments>(entity =>
            {
                entity.ToTable("comments");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment");

                entity.Property(e => e.SiteId).HasColumnName("site_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.SiteId)
                    .HasConstraintName("Fk_CommentsSiteId_Cascade");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Fk_UserCommentsSiteId_Cascade");
            });

            modelBuilder.Entity<Ratings>(entity =>
            {
                entity.ToTable("ratings");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SiteId).HasColumnName("site_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.SiteId)
                    .HasConstraintName("Fk_RatingSiteId_Cascade");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Fk_RatingUserId_Cascade");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Sites>(entity =>
            {
                entity.ToTable("sites");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Header)
                    .IsRequired()
                    .HasColumnName("header");

                entity.Property(e => e.Keywords)
                    .IsRequired()
                    .HasColumnName("keywords");

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasColumnName("link");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasDefaultValueSql("((2))");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("Fk_UserRole_Cascade");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
