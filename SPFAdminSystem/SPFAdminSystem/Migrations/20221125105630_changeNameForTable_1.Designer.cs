﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace SPFAdminSystem.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221125105630_changeNameForTable_1")]
    partial class changeNameForTable_1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("Mapping", b =>
                {
                    b.Property<string>("ProductIdMapping")
                        .HasColumnType("TEXT");

                    b.Property<string>("Barcode")
                        .HasColumnType("TEXT");

                    b.Property<int?>("MinOrder")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PackSize")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Target")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TitleGWS")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ProductIdMapping");

                    b.ToTable("Mappings");
                });

            modelBuilder.Entity("Product", b =>
                {
                    b.Property<string>("ProductId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ArriveDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("AvailableAmount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Barcode")
                        .HasColumnType("TEXT");

                    b.Property<string>("InHouseTitle")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("MinOrder")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OrderAmount")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("OrderPrice")
                        .HasColumnType("REAL");

                    b.Property<int?>("OrderQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Ordered")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Packsize")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("RemovedFromStockDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("StockAmount")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Target")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TitleGWS")
                        .HasColumnType("TEXT");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SPFAdminSystem.Database.UserFiles.UploadFileChangeProduct", b =>
                {
                    b.Property<int>("FileChangeProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProductId")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserActionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FileChangeProductId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserActionId");

                    b.ToTable("FileChangeProducts");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RegisterDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UserAction", b =>
                {
                    b.Property<int>("UserActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ActionType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductId")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserActionId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("UserActions");
                });

            modelBuilder.Entity("SPFAdminSystem.Database.UserFiles.UploadFileChangeProduct", b =>
                {
                    b.HasOne("Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("UserAction", "UserAction")
                        .WithMany()
                        .HasForeignKey("UserActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("UserAction");
                });

            modelBuilder.Entity("UserAction", b =>
                {
                    b.HasOne("Product", "Product")
                        .WithMany("UserActions")
                        .HasForeignKey("ProductId");

                    b.HasOne("User", "User")
                        .WithMany("UserActions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Product", b =>
                {
                    b.Navigation("UserActions");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Navigation("UserActions");
                });
#pragma warning restore 612, 618
        }
    }
}
