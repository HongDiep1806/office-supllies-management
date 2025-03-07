﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Office_supplies_management.DAL;

#nullable disable

namespace Office_supplies_management.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20250305072328_updatedb")]
    partial class updatedb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Office_supplies_management.Models.Permission", b =>
                {
                    b.Property<int>("PermissionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PermissionID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("PermissionID");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Office_supplies_management.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitCurrency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitPrice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Office_supplies_management.Models.Product_Request", b =>
                {
                    b.Property<int>("Product_RequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Product_RequestID"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("RequestID")
                        .HasColumnType("int");

                    b.HasKey("Product_RequestID");

                    b.HasIndex("ProductID");

                    b.HasIndex("RequestID");

                    b.ToTable("Product_Requests");
                });

            modelBuilder.Entity("Office_supplies_management.Models.Request", b =>
                {
                    b.Property<int>("RequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestID"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsApprovedByDepLead")
                        .HasColumnType("bit");

                    b.Property<bool>("IsApprovedBySupLead")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RequestCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("RequestID");

                    b.HasIndex("UserID");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Office_supplies_management.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserTypeID")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.HasIndex("UserTypeID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Office_supplies_management.Models.UserType", b =>
                {
                    b.Property<int>("UserTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserTypeID"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserTypeID");

                    b.ToTable("UserTypes");
                });

            modelBuilder.Entity("Office_supplies_management.Models.UserType_Permission", b =>
                {
                    b.Property<int>("UserType_PermissionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserType_PermissionID"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("PermissionID")
                        .HasColumnType("int");

                    b.Property<int>("UserTypeID")
                        .HasColumnType("int");

                    b.HasKey("UserType_PermissionID");

                    b.HasIndex("PermissionID");

                    b.HasIndex("UserTypeID");

                    b.ToTable("UserTypes_Permissions");
                });

            modelBuilder.Entity("PermissionUserType", b =>
                {
                    b.Property<int>("PermissionsPermissionID")
                        .HasColumnType("int");

                    b.Property<int>("UserTypesUserTypeID")
                        .HasColumnType("int");

                    b.HasKey("PermissionsPermissionID", "UserTypesUserTypeID");

                    b.HasIndex("UserTypesUserTypeID");

                    b.ToTable("PermissionUserType");
                });

            modelBuilder.Entity("Office_supplies_management.Models.Product_Request", b =>
                {
                    b.HasOne("Office_supplies_management.Models.Product", "Product")
                        .WithMany("Product_Requests")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Office_supplies_management.Models.Request", "Request")
                        .WithMany("Product_Requests")
                        .HasForeignKey("RequestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("Office_supplies_management.Models.Request", b =>
                {
                    b.HasOne("Office_supplies_management.Models.User", "User")
                        .WithMany("Requests")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Office_supplies_management.Models.User", b =>
                {
                    b.HasOne("Office_supplies_management.Models.UserType", "UserType")
                        .WithMany("Users")
                        .HasForeignKey("UserTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("Office_supplies_management.Models.UserType_Permission", b =>
                {
                    b.HasOne("Office_supplies_management.Models.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Office_supplies_management.Models.UserType", "UserType")
                        .WithMany()
                        .HasForeignKey("UserTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("PermissionUserType", b =>
                {
                    b.HasOne("Office_supplies_management.Models.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionsPermissionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Office_supplies_management.Models.UserType", null)
                        .WithMany()
                        .HasForeignKey("UserTypesUserTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Office_supplies_management.Models.Product", b =>
                {
                    b.Navigation("Product_Requests");
                });

            modelBuilder.Entity("Office_supplies_management.Models.Request", b =>
                {
                    b.Navigation("Product_Requests");
                });

            modelBuilder.Entity("Office_supplies_management.Models.User", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("Office_supplies_management.Models.UserType", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
