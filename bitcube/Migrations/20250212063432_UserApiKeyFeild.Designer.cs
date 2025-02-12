﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bitcube.Model;

namespace bitcube.Migrations
{
    [DbContext(typeof(BitcubeContext))]
    [Migration("20250212063432_UserApiKeyFeild")]
    partial class UserApiKeyFeild
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("bitcube.Model.Product", b =>
                {
                    b.Property<long>("productId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("createdByusername")
                        .HasColumnType("TEXT");

                    b.Property<string>("productName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("productPrice")
                        .HasColumnType("REAL");

                    b.Property<long>("quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("productId");

                    b.HasIndex("createdByusername");

                    b.ToTable("products");
                });

            modelBuilder.Entity("bitcube.Model.User", b =>
                {
                    b.Property<string>("username")
                        .HasColumnType("TEXT");

                    b.Property<string>("apiKey")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("created")
                        .HasColumnType("TEXT");

                    b.Property<string>("firstname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("last_updated")
                        .HasColumnType("TEXT");

                    b.Property<string>("lastname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("username");

                    b.ToTable("users");
                });

            modelBuilder.Entity("bitcube.Model.Product", b =>
                {
                    b.HasOne("bitcube.Model.User", "createdBy")
                        .WithMany()
                        .HasForeignKey("createdByusername");

                    b.Navigation("createdBy");
                });
#pragma warning restore 612, 618
        }
    }
}
