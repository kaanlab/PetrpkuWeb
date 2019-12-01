﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetrpkuWeb.Server.Data;

namespace PetrpkuWeb.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20191201110523_InitMigration")]
    partial class InitMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0-preview3.19554.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "eb4550b3-4572-4ff8-acc7-937b7109a178",
                            ConcurrencyStamp = "ba347e27-e991-4b65-b324-979ca026013a",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "9a59f8dd-9fa3-40ab-a6ca-72e42f6a248b",
                            ConcurrencyStamp = "0f74c0d9-8dd4-4030-83db-f3b6944999a9",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "1325309b-4e99-4c4a-aa0f-5ed932c5f790",
                            ConcurrencyStamp = "bc5ef394-54bd-43e9-a4c1-a9a41cb41d22",
                            Name = "Kadry",
                            NormalizedName = "KADRY"
                        },
                        new
                        {
                            Id = "f239058c-65e8-49d4-8091-eca645527870",
                            ConcurrencyStamp = "e1743108-540c-404b-87a6-6fbfd6046cdf",
                            Name = "Publisher",
                            NormalizedName = "PUBLISHER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<int?>("BuildingId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ExtPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IntPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDuty")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LdapAuth")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MidleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Room")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("WorkingPosition")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.Approved", b =>
                {
                    b.Property<int>("ApprovedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.HasKey("ApprovedId");

                    b.HasIndex("AppUserId");

                    b.ToTable("Approved");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.Attachment", b =>
                {
                    b.Property<int>("AttachmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DocSectionId")
                        .HasColumnType("int");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsImage")
                        .HasColumnType("bit");

                    b.Property<long>("Length")
                        .HasColumnType("bigint");

                    b.Property<int?>("MilRequestId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.HasKey("AttachmentId");

                    b.HasIndex("DocSectionId");

                    b.HasIndex("MilRequestId");

                    b.HasIndex("PostId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.Building", b =>
                {
                    b.Property<int>("BuildingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BuildingId");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.Checked", b =>
                {
                    b.Property<int>("CheckedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsChecked")
                        .HasColumnType("bit");

                    b.HasKey("CheckedId");

                    b.HasIndex("AppUserId");

                    b.ToTable("Checkeds");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.CssType", b =>
                {
                    b.Property<int>("CssTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CssTypeId");

                    b.ToTable("CssTypes");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.DocSection", b =>
                {
                    b.Property<int>("DocSectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DocSectionId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("DocSection");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.Duty", b =>
                {
                    b.Property<int>("DutyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DayOfDuty")
                        .HasColumnType("datetime2");

                    b.HasKey("DutyId");

                    b.HasIndex("AppUserId");

                    b.ToTable("Duties");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.MilRequest", b =>
                {
                    b.Property<int>("MilRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ApprovedId")
                        .HasColumnType("int");

                    b.Property<int?>("CheckedId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsReadonly")
                        .HasColumnType("bit");

                    b.Property<int?>("PublishedId")
                        .HasColumnType("int");

                    b.Property<int?>("SentId")
                        .HasColumnType("int");

                    b.Property<int?>("SiteSectionId")
                        .HasColumnType("int");

                    b.Property<int?>("SiteSubSectionId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ToDo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("MilRequestId");

                    b.HasIndex("AppUserId");

                    b.HasIndex("ApprovedId");

                    b.HasIndex("CheckedId");

                    b.HasIndex("PublishedId");

                    b.HasIndex("SentId");

                    b.HasIndex("SiteSectionId");

                    b.HasIndex("SiteSubSectionId");

                    b.ToTable("MilRequests");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.Note", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CssTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("NoteId");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CssTypeId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<bool>("OnMain")
                        .HasColumnType("bit");

                    b.Property<string>("Poster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PostId");

                    b.HasIndex("AppUserId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.Published", b =>
                {
                    b.Property<int>("PublishedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.HasKey("PublishedId");

                    b.HasIndex("AppUserId");

                    b.ToTable("Publisheds");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.Sent", b =>
                {
                    b.Property<int>("SentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsSent")
                        .HasColumnType("bit");

                    b.HasKey("SentId");

                    b.HasIndex("AppUserId");

                    b.ToTable("Sents");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.SiteSection", b =>
                {
                    b.Property<int>("SiteSectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SiteSectionId");

                    b.ToTable("SiteSections");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.SiteSubsection", b =>
                {
                    b.Property<int>("SiteSubSectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("SiteSectionId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SiteSubSectionId");

                    b.HasIndex("SiteSectionId");

                    b.ToTable("SiteSubsections");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PetrpkuWeb.Server.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PetrpkuWeb.Server.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetrpkuWeb.Server.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PetrpkuWeb.Server.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.AppUser", b =>
                {
                    b.HasOne("PetrpkuWeb.Server.Models.Building", "Building")
                        .WithMany("AppUsers")
                        .HasForeignKey("BuildingId");

                    b.HasOne("PetrpkuWeb.Server.Models.Department", "Department")
                        .WithMany("AppUsers")
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.Approved", b =>
                {
                    b.HasOne("PetrpkuWeb.Server.Models.AppUser", "AppUser")
                        .WithMany("Approveds")
                        .HasForeignKey("AppUserId");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.Attachment", b =>
                {
                    b.HasOne("PetrpkuWeb.Server.Models.DocSection", "DocSection")
                        .WithMany("Attachments")
                        .HasForeignKey("DocSectionId");

                    b.HasOne("PetrpkuWeb.Server.Models.MilRequest", "MilRequest")
                        .WithMany("Attachments")
                        .HasForeignKey("MilRequestId");

                    b.HasOne("PetrpkuWeb.Server.Models.Post", "Post")
                        .WithMany("Attachments")
                        .HasForeignKey("PostId");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.Checked", b =>
                {
                    b.HasOne("PetrpkuWeb.Server.Models.AppUser", "AppUser")
                        .WithMany("Checkeds")
                        .HasForeignKey("AppUserId");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.DocSection", b =>
                {
                    b.HasOne("PetrpkuWeb.Server.Models.AppUser", "Author")
                        .WithMany("DocSections")
                        .HasForeignKey("AuthorId");

                    b.HasOne("PetrpkuWeb.Server.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.Duty", b =>
                {
                    b.HasOne("PetrpkuWeb.Server.Models.AppUser", "AppUser")
                        .WithMany("DaysOfDuty")
                        .HasForeignKey("AppUserId");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.MilRequest", b =>
                {
                    b.HasOne("PetrpkuWeb.Server.Models.AppUser", "AppUser")
                        .WithMany("MilRequests")
                        .HasForeignKey("AppUserId");

                    b.HasOne("PetrpkuWeb.Server.Models.Approved", "Approved")
                        .WithMany()
                        .HasForeignKey("ApprovedId");

                    b.HasOne("PetrpkuWeb.Server.Models.Checked", "Checked")
                        .WithMany()
                        .HasForeignKey("CheckedId");

                    b.HasOne("PetrpkuWeb.Server.Models.Published", "Published")
                        .WithMany()
                        .HasForeignKey("PublishedId");

                    b.HasOne("PetrpkuWeb.Server.Models.Sent", "Sent")
                        .WithMany()
                        .HasForeignKey("SentId");

                    b.HasOne("PetrpkuWeb.Server.Models.SiteSection", "SiteSection")
                        .WithMany()
                        .HasForeignKey("SiteSectionId");

                    b.HasOne("PetrpkuWeb.Server.Models.SiteSubsection", "SiteSubSection")
                        .WithMany()
                        .HasForeignKey("SiteSubSectionId");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.Note", b =>
                {
                    b.HasOne("PetrpkuWeb.Server.Models.AppUser", "AppUser")
                        .WithMany("Notes")
                        .HasForeignKey("AppUserId");

                    b.HasOne("PetrpkuWeb.Server.Models.CssType", "CssType")
                        .WithMany("Notes")
                        .HasForeignKey("CssTypeId");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.Post", b =>
                {
                    b.HasOne("PetrpkuWeb.Server.Models.AppUser", "AppUser")
                        .WithMany("Posts")
                        .HasForeignKey("AppUserId");

                    b.HasOne("PetrpkuWeb.Server.Models.Department", "Department")
                        .WithMany("Posts")
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.Published", b =>
                {
                    b.HasOne("PetrpkuWeb.Server.Models.AppUser", "AppUser")
                        .WithMany("Publisheds")
                        .HasForeignKey("AppUserId");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.Sent", b =>
                {
                    b.HasOne("PetrpkuWeb.Server.Models.AppUser", "AppUser")
                        .WithMany("Sents")
                        .HasForeignKey("AppUserId");
                });

            modelBuilder.Entity("PetrpkuWeb.Server.Models.SiteSubsection", b =>
                {
                    b.HasOne("PetrpkuWeb.Server.Models.SiteSection", "SiteSection")
                        .WithMany("SiteSubSections")
                        .HasForeignKey("SiteSectionId");
                });
#pragma warning restore 612, 618
        }
    }
}
