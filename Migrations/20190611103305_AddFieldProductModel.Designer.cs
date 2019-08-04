﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ShareALittle.Models;
using System;

namespace ShareALittle.Migrations
{
    [DbContext(typeof(ShareALittleContext))]
    [Migration("20190611103305_AddFieldProductModel")]
    partial class AddFieldProductModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ShareALittle.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryDescription")
                        .HasMaxLength(200);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("CategoryID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ShareALittle.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryID");

                    b.Property<string>("ContactDetail");

                    b.Property<bool>("IsBooked");

                    b.Property<string>("OwnerID");

                    b.Property<string>("ProductDescription");

                    b.Property<byte[]>("ProductImage");

                    b.Property<string>("ProductName");

                    b.Property<decimal>("ProductPrice");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ShareALittle.Models.Product", b =>
                {
                    b.HasOne("ShareALittle.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
