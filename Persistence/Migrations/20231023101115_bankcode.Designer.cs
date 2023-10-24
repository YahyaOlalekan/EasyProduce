﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.AppDbContext;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20231023101115_bankcode")]
    partial class bankcode
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Entity.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("Domain.Entity.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NameOfCategory")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("ProduceName")
                        .HasColumnType("longtext");

                    b.Property<Guid>("ProduceTypeId")
                        .HasColumnType("char(36)");

                    b.Property<double>("Quantity")
                        .HasColumnType("double");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("UnitOfMeasurement")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ProduceTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("Domain.Entity.CartItemForOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NameOfCategory")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("ProduceName")
                        .HasColumnType("longtext");

                    b.Property<Guid>("ProductTypeId")
                        .HasColumnType("char(36)");

                    b.Property<double>("Quantity")
                        .HasColumnType("double");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("UnitOfMeasurement")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ProductTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("CartItemForOrders");
                });

            modelBuilder.Entity("Domain.Entity.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DescriptionOfCategory")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NameOfCategory")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Entity.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("FarmerId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("ManagerId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Message")
                        .HasColumnType("longtext");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PostedTime")
                        .HasColumnType("longtext");

                    b.Property<bool>("Seen")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("FarmerId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Domain.Entity.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RegistrationNumber")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Domain.Entity.Farmer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AccountName")
                        .HasColumnType("longtext");

                    b.Property<string>("AccountNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("BankCode")
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FarmName")
                        .HasColumnType("longtext");

                    b.Property<int>("FarmerRegStatus")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RegistrationNumber")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Farmers");
                });

            modelBuilder.Entity("Domain.Entity.FarmerProduceType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("FarmerId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ProduceTypeId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FarmerId");

                    b.HasIndex("ProduceTypeId");

                    b.ToTable("FarmerProduceTypes");
                });

            modelBuilder.Entity("Domain.Entity.Manager", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RegistrationNumber")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("Domain.Entity.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("OrderNumber")
                        .HasColumnType("longtext");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<double>("TotalQuantity")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Entity.OrderProductType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<Guid>("ProductTypeId")
                        .HasColumnType("char(36)");

                    b.Property<double>("Quantity")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("OrderProductTypes");
                });

            modelBuilder.Entity("Domain.Entity.Produce", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DescriptionOfProduce")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ProduceName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Produces");
                });

            modelBuilder.Entity("Domain.Entity.ProduceType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("CostPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ProduceId")
                        .HasColumnType("char(36)");

                    b.Property<double>("Quantity")
                        .HasColumnType("double");

                    b.Property<decimal>("SellingPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TypeName")
                        .HasColumnType("longtext");

                    b.Property<string>("UnitOfMeasurement")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProduceId");

                    b.ToTable("ProduceTypes");
                });

            modelBuilder.Entity("Domain.Entity.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ProduceName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.Entity.ProductType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("CostPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ProduceId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("char(36)");

                    b.Property<double>("QuantityToBuy")
                        .HasColumnType("double");

                    b.Property<decimal>("SellingPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TypeName")
                        .HasColumnType("longtext");

                    b.Property<string>("TypePicture")
                        .HasColumnType("longtext");

                    b.Property<string>("UnitOfMeasurement")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProduceId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("Domain.Entity.Request", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<Guid>("FarmerId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ProduceTypeId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ReasonForStopSelling")
                        .HasColumnType("longtext");

                    b.Property<string>("RegistrationNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("RejectionReason")
                        .HasColumnType("longtext");

                    b.Property<string>("RequestNumber")
                        .HasColumnType("longtext");

                    b.Property<int>("RequestStatus")
                        .HasColumnType("int");

                    b.Property<int>("RequestType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FarmerId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Domain.Entity.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RoleDescription")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.Entity.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("FarmerId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<Guid>("ProduceTypeId")
                        .HasColumnType("char(36)");

                    b.Property<double>("Quantity")
                        .HasColumnType("double");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("TransactionNum")
                        .HasColumnType("longtext");

                    b.Property<int>("TransactionStatus")
                        .HasColumnType("int");

                    b.Property<string>("UnitOfMeasurement")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("FarmerId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Domain.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("longtext");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entity.Admin", b =>
                {
                    b.HasOne("Domain.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entity.CartItem", b =>
                {
                    b.HasOne("Domain.Entity.ProduceType", "ProduceType")
                        .WithMany()
                        .HasForeignKey("ProduceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProduceType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entity.CartItemForOrder", b =>
                {
                    b.HasOne("Domain.Entity.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entity.Chat", b =>
                {
                    b.HasOne("Domain.Entity.Farmer", "Farmer")
                        .WithMany("Chats")
                        .HasForeignKey("FarmerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.Manager", "Manager")
                        .WithMany("Chats")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Farmer");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("Domain.Entity.Customer", b =>
                {
                    b.HasOne("Domain.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entity.Farmer", b =>
                {
                    b.HasOne("Domain.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entity.FarmerProduceType", b =>
                {
                    b.HasOne("Domain.Entity.Farmer", "Farmer")
                        .WithMany("FarmerProduceTypes")
                        .HasForeignKey("FarmerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.ProduceType", "ProduceType")
                        .WithMany("FarmerProduceTypes")
                        .HasForeignKey("ProduceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Farmer");

                    b.Navigation("ProduceType");
                });

            modelBuilder.Entity("Domain.Entity.Manager", b =>
                {
                    b.HasOne("Domain.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entity.Order", b =>
                {
                    b.HasOne("Domain.Entity.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Domain.Entity.OrderProductType", b =>
                {
                    b.HasOne("Domain.Entity.Order", "Order")
                        .WithMany("OrderProductTypes")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.ProductType", "ProductType")
                        .WithMany("OrderProductTypes")
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("Domain.Entity.Produce", b =>
                {
                    b.HasOne("Domain.Entity.Category", "Category")
                        .WithMany("Produces")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Domain.Entity.ProduceType", b =>
                {
                    b.HasOne("Domain.Entity.Produce", "Produce")
                        .WithMany("ProduceTypes")
                        .HasForeignKey("ProduceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produce");
                });

            modelBuilder.Entity("Domain.Entity.Product", b =>
                {
                    b.HasOne("Domain.Entity.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Domain.Entity.ProductType", b =>
                {
                    b.HasOne("Domain.Entity.Produce", "Produce")
                        .WithMany()
                        .HasForeignKey("ProduceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.Product", null)
                        .WithMany("ProductTypes")
                        .HasForeignKey("ProductId");

                    b.Navigation("Produce");
                });

            modelBuilder.Entity("Domain.Entity.Request", b =>
                {
                    b.HasOne("Domain.Entity.Farmer", "Farmer")
                        .WithMany("Requests")
                        .HasForeignKey("FarmerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Farmer");
                });

            modelBuilder.Entity("Domain.Entity.Transaction", b =>
                {
                    b.HasOne("Domain.Entity.Farmer", "Farmer")
                        .WithMany("Transactions")
                        .HasForeignKey("FarmerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.Manager", "Manager")
                        .WithMany("Transactions")
                        .HasForeignKey("ManagerId");

                    b.Navigation("Farmer");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("Domain.Entity.User", b =>
                {
                    b.HasOne("Domain.Entity.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Entity.Category", b =>
                {
                    b.Navigation("Produces");
                });

            modelBuilder.Entity("Domain.Entity.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Domain.Entity.Farmer", b =>
                {
                    b.Navigation("Chats");

                    b.Navigation("FarmerProduceTypes");

                    b.Navigation("Requests");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Domain.Entity.Manager", b =>
                {
                    b.Navigation("Chats");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Domain.Entity.Order", b =>
                {
                    b.Navigation("OrderProductTypes");
                });

            modelBuilder.Entity("Domain.Entity.Produce", b =>
                {
                    b.Navigation("ProduceTypes");
                });

            modelBuilder.Entity("Domain.Entity.ProduceType", b =>
                {
                    b.Navigation("FarmerProduceTypes");
                });

            modelBuilder.Entity("Domain.Entity.Product", b =>
                {
                    b.Navigation("ProductTypes");
                });

            modelBuilder.Entity("Domain.Entity.ProductType", b =>
                {
                    b.Navigation("OrderProductTypes");
                });

            modelBuilder.Entity("Domain.Entity.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
