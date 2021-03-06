// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OtiumActio;

namespace OtiumActio.Migrations
{
    [DbContext(typeof(OtiumActioContext))]
    [Migration("20211028201432_AddedPrimaryKeyToActivityCategory")]
    partial class AddedPrimaryKeyToActivityCategory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OtiumActio.Activity", b =>
                {
                    b.Property<int>("AcId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Ac_Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AcCategoryId")
                        .HasColumnType("int")
                        .HasColumnName("Ac_CategoryId");

                    b.Property<DateTime?>("AcCreated")
                        .HasColumnType("datetime")
                        .HasColumnName("Ac_Created");

                    b.Property<DateTime?>("AcDate")
                        .HasColumnType("datetime")
                        .HasColumnName("Ac_Date");

                    b.Property<string>("AcDescription")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Ac_Description");

                    b.Property<DateTime?>("AcModified")
                        .HasColumnType("datetime")
                        .HasColumnName("AC_Modified");

                    b.Property<byte?>("AcParticipants")
                        .HasColumnType("tinyint")
                        .HasColumnName("Ac_Participants");

                    b.HasKey("AcId")
                        .HasName("PK_Tbl_Activity");

                    b.HasIndex("AcCategoryId");

                    b.ToTable("Activity", "Activity");
                });

            modelBuilder.Entity("OtiumActio.ActivityCategory", b =>
                {
                    b.Property<int?>("AcatActivityId")
                        .HasColumnType("int")
                        .HasColumnName("Acat_ActivityId");

                    b.Property<int>("AcatCategoryId")
                        .HasColumnType("int")
                        .HasColumnName("Acat_CategoryId");

                    b.Property<DateTime?>("AcatCreated")
                        .HasColumnType("datetime")
                        .HasColumnName("Acat_Created");

                    b.HasIndex("AcatActivityId");

                    b.HasIndex("AcatCategoryId");

                    b.ToTable("ActivityCategory", "Activity");
                });

            modelBuilder.Entity("OtiumActio.Category", b =>
                {
                    b.Property<int>("CatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Cat_Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CatName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Cat_Name");

                    b.HasKey("CatId")
                        .HasName("PK_Tbl_Category");

                    b.ToTable("Category", "Activity");
                });

            modelBuilder.Entity("OtiumActio.Participant", b =>
                {
                    b.Property<int>("PrtcId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Prtc_Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PrtcActivityId")
                        .HasColumnType("int")
                        .HasColumnName("Prtc_ActivityId");

                    b.Property<int?>("PrtcAge")
                        .HasColumnType("int")
                        .HasColumnName("Prtc_Age");

                    b.Property<DateTime?>("PrtcCreated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PrtcFavouritCategory")
                        .HasColumnType("int")
                        .HasColumnName("Prtc_FavouritCategory");

                    b.Property<string>("PrtcFirstName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Prtc_FirstName");

                    b.Property<string>("PrtcLastName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Prtc_LastName");

                    b.Property<DateTime?>("PrtcModified")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("PrtcPasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PrtcPasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PrtcUserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PrtcId")
                        .HasName("PK_Tbl_Participant");

                    b.HasIndex("PrtcActivityId");

                    b.HasIndex("PrtcFavouritCategory");

                    b.ToTable("Participant", "User");
                });

            modelBuilder.Entity("OtiumActio.Activity", b =>
                {
                    b.HasOne("OtiumActio.Category", "AcCategory")
                        .WithMany("Activities")
                        .HasForeignKey("AcCategoryId")
                        .HasConstraintName("FK_CategoryId");

                    b.Navigation("AcCategory");
                });

            modelBuilder.Entity("OtiumActio.ActivityCategory", b =>
                {
                    b.HasOne("OtiumActio.Activity", "AcatActivity")
                        .WithMany()
                        .HasForeignKey("AcatActivityId")
                        .HasConstraintName("FK_AcatActivityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OtiumActio.Category", "AcatCategory")
                        .WithMany()
                        .HasForeignKey("AcatCategoryId")
                        .HasConstraintName("FK_AcatCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AcatActivity");

                    b.Navigation("AcatCategory");
                });

            modelBuilder.Entity("OtiumActio.Participant", b =>
                {
                    b.HasOne("OtiumActio.Activity", "PrtcActivity")
                        .WithMany("Participants")
                        .HasForeignKey("PrtcActivityId")
                        .HasConstraintName("FK__Tbl_Parti__Prtc___787EE5A0");

                    b.HasOne("OtiumActio.Category", "PrtcFavouritCategoryNavigation")
                        .WithMany("Participants")
                        .HasForeignKey("PrtcFavouritCategory")
                        .HasConstraintName("FK__Tbl_Parti__Prtc___797309D9");

                    b.Navigation("PrtcActivity");

                    b.Navigation("PrtcFavouritCategoryNavigation");
                });

            modelBuilder.Entity("OtiumActio.Activity", b =>
                {
                    b.Navigation("Participants");
                });

            modelBuilder.Entity("OtiumActio.Category", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Participants");
                });
#pragma warning restore 612, 618
        }
    }
}
