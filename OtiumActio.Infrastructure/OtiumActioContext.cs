using System;
using Microsoft.EntityFrameworkCore;
using OtiumActio.Domain.Activities;
using OtiumActio.Domain.Categories;
using OtiumActio.Domain.Users;
using System.Linq;

#nullable disable

namespace OtiumActio.Infrastructure
{
    public partial class OtiumActioContext : DbContext
    {
        //public OtiumActioContext()
        //{
        //}

        public OtiumActioContext(DbContextOptions<OtiumActioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivityCategory> ActivityCategories { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasKey(e => e.AcId)
                    .HasName("PK_Tbl_Activity");

                entity.ToTable("Activity", "Activity");

                entity.Property(e => e.AcId).HasColumnName("Ac_Id");

                entity.Property(e => e.AcCategoryId).HasColumnName("Ac_CategoryId");

                entity.Property(e => e.AcCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("Ac_Created");

                entity.Property(e => e.AcDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ac_Date");

                entity.Property(e => e.AcDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Ac_Description");

                entity.Property(e => e.AcModified)
                    .HasColumnType("datetime")
                    .HasColumnName("AC_Modified");

                entity.Property(e => e.AcParticipants).HasColumnName("Ac_Participants");

                entity.HasOne(d => d.AcCategory)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.AcCategoryId)
                    .HasConstraintName("FK_CategoryId");
            });

            modelBuilder.Entity<ActivityCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ActivityCategory", "Activity");

                entity.Property(e => e.AcatActivityId).HasColumnName("Acat_ActivityId");

                entity.Property(e => e.AcatCategoryId).HasColumnName("Acat_CategoryId");

                entity.Property(e => e.AcatCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("Acat_Created");

                entity.HasOne(d => d.AcatActivity)
                    .WithMany()
                    .HasForeignKey(d => d.AcatActivityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_AcatActivityId");

                entity.HasOne(d => d.AcatCategory)
                    .WithMany()
                    .HasForeignKey(d => d.AcatCategoryId)
                    .HasConstraintName("FK_AcatCategoryId");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CatId)
                    .HasName("PK_Tbl_Category");

                entity.ToTable("Category", "Activity");

                entity.Property(e => e.CatId).HasColumnName("Cat_Id");

                entity.Property(e => e.CatName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Cat_Name");
            });

            modelBuilder.Entity<Participant>(entity =>
            {
                entity.HasKey(e => e.PrtcId)
                    .HasName("PK_Tbl_Participant");

                entity.ToTable("Participant", "User");

                entity.Property(e => e.PrtcId).HasColumnName("Prtc_Id");

                entity.Property(e => e.PrtcActivityId).HasColumnName("Prtc_ActivityId");

                entity.Property(e => e.PrtcAge).HasColumnName("Prtc_Age");

                entity.Property(e => e.PrtcFavouritCategory).HasColumnName("Prtc_FavouritCategory");

                entity.Property(e => e.PrtcFirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Prtc_FirstName");

                entity.Property(e => e.PrtcLastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Prtc_LastName");

                entity.HasOne(d => d.PrtcActivity)
                    .WithMany(p => p.Participants)
                    .HasForeignKey(d => d.PrtcActivityId)
                    .HasConstraintName("FK__Tbl_Parti__Prtc___787EE5A0");

                entity.HasOne(d => d.PrtcFavouritCategoryNavigation)
                    .WithMany(p => p.Participants)
                    .HasForeignKey(d => d.PrtcFavouritCategory)
                    .HasConstraintName("FK__Tbl_Parti__Prtc___797309D9");
    //            entity.Property(e => e.PrtcUserName)
    //                .HasMaxLength(50)
    //                .IsUnicode(false);

    //            entity.Property(e => e.PrtcPasswordHash)
    //                .HasColumnType("binary")
    //                .HasColumnName("Prtc_PasswordHash");
    //            entity.Property(e => e.PrtcPasswordSalt)
    //.HasColumnType("binary")
    //.HasColumnName("Prtc_PrtcPasswordSalt");

    //            entity.Property(e => e.PrtcCreated)
    //                .HasColumnType("datetime")
    //                .HasColumnName("Prtc_Created");

    //            entity.Property(e => e.PrtcModified)
    //                .HasColumnType("datetime")
    //                .HasColumnName("Prtc_Modified");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
