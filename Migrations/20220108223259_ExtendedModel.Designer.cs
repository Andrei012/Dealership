﻿// <auto-generated />
using System;
using Dealership.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dealership.Migrations
{
    [DbContext(typeof(DealershipContext))]
    [Migration("20220108223259_ExtendedModel")]
    partial class ExtendedModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dealership.Models.Car", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("An")
                        .HasColumnType("int");

                    b.Property<string>("Combustibil")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cutie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Marca")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Motor")
                        .HasColumnType("float");

                    b.Property<int>("Pret")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Car");
                });

            modelBuilder.Entity("Dealership.Models.CarSuppliers", b =>
                {
                    b.Property<int>("CarID")
                        .HasColumnType("int");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.HasKey("CarID", "SupplierID");

                    b.HasIndex("SupplierID");

                    b.ToTable("CarSuppliers");
                });

            modelBuilder.Entity("Dealership.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Dealership.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarID")
                        .HasColumnType("int");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderID");

                    b.HasIndex("CarID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Dealership.Models.Supplier", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress")
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("Dealership.Models.CarSuppliers", b =>
                {
                    b.HasOne("Dealership.Models.Car", "Car")
                        .WithMany("CarSuppliers")
                        .HasForeignKey("CarID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dealership.Models.Supplier", "Supplier")
                        .WithMany("CarSuppliers")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Dealership.Models.Order", b =>
                {
                    b.HasOne("Dealership.Models.Car", "Car")
                        .WithMany("Orders")
                        .HasForeignKey("CarID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dealership.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Dealership.Models.Car", b =>
                {
                    b.Navigation("CarSuppliers");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Dealership.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Dealership.Models.Supplier", b =>
                {
                    b.Navigation("CarSuppliers");
                });
#pragma warning restore 612, 618
        }
    }
}
