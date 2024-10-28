﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NhaSachDaiThang_BE_API.Data;

#nullable disable

namespace NhaSachDaiThang_BE_API.Migrations
{
    [DbContext(typeof(BookStoreContext))]
    [Migration("20241027045137_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookPromotion", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("BookID");

                    b.Property<int>("PromotionId")
                        .HasColumnType("int")
                        .HasColumnName("PromotionID");

                    b.HasKey("BookId", "PromotionId")
                        .HasName("PK__BookProm__D8CC80D5FD16A69C");

                    b.HasIndex("PromotionId");

                    b.ToTable("BookPromotions", (string)null);
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BookID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"));

                    b.Property<string>("Author")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool?>("IsDel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("MainImage")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateOnly?>("PublishDate")
                        .HasColumnType("date");

                    b.Property<string>("Publisher")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal?>("PurchasePrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("BookId")
                        .HasName("PK__Books__3DE0C227509BC471");

                    b.HasIndex("CategoryId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.BookImage", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ImageID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageId"));

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("BookID");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ImageURL");

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime");

                    b.HasKey("ImageId")
                        .HasName("PK__BookImag__7516F4EC1E8E4B20");

                    b.HasIndex("BookId");

                    b.ToTable("BookImages");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoryId")
                        .HasName("PK__Categori__19093A2BE21B8E70");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Customer", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FullName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("RoleID");

                    b.HasKey("UserId")
                        .HasName("PK__Customer__A4AE64B80DDF41BD");

                    b.HasIndex("RoleId");

                    b.HasIndex(new[] { "Email" }, "UQ__Customer__A9D1053471732474")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FullName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("RoleID");

                    b.HasKey("EmployeeId")
                        .HasName("PK__Employee__7AD04FF1F64003B7");

                    b.HasIndex("RoleId");

                    b.HasIndex(new[] { "Email" }, "UQ__Employee__A9D10534FA126257")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Status")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal?>("TotalAmount")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("OrderId")
                        .HasName("PK__Orders__C3905BAF2225F30A");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderDetailID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailId"));

                    b.Property<int?>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("BookID");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId")
                        .HasName("PK__OrderDet__D3B9D30CB3022B7C");

                    b.HasIndex("BookId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PaymentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<DateTime>("PaymentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("PaymentMethod")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PaymentId")
                        .HasName("PK__Payments__9B556A588C4AA9B9");

                    b.HasIndex("OrderId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Promotion", b =>
                {
                    b.Property<int>("PromotionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PromotionID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PromotionId"));

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateOnly?>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("PromotionId")
                        .HasName("PK__Promotio__52C42F2F433C3844");

                    b.ToTable("Promotions");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ReviewID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<int?>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("BookID");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReviewDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("ReviewId")
                        .HasName("PK__Reviews__74BC79AEC46D3806");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RoleID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoleId")
                        .HasName("PK__Roles__8AFACE3AE06EA5E2");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Voucher", b =>
                {
                    b.Property<int>("VoucherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("VoucherID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VoucherId"));

                    b.Property<string>("Code")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<DateOnly?>("ExpirationDate")
                        .HasColumnType("date");

                    b.Property<decimal?>("MinOrderValue")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime");

                    b.HasKey("VoucherId")
                        .HasName("PK__Vouchers__3AEE79C132561A24");

                    b.HasIndex(new[] { "Code" }, "UQ__Vouchers__A25C5AA76011FE50")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("OrderVoucher", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<int>("VoucherId")
                        .HasColumnType("int")
                        .HasColumnName("VoucherID");

                    b.HasKey("OrderId", "VoucherId")
                        .HasName("PK__OrderVou__C03EBC33F9DC2251");

                    b.HasIndex("VoucherId");

                    b.ToTable("OrderVouchers", (string)null);
                });

            modelBuilder.Entity("BookPromotion", b =>
                {
                    b.HasOne("NhaSachDaiThang_BE_API.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("BookId")
                        .IsRequired()
                        .HasConstraintName("FK__BookPromo__BookI__48CFD27E");

                    b.HasOne("NhaSachDaiThang_BE_API.Models.Promotion", null)
                        .WithMany()
                        .HasForeignKey("PromotionId")
                        .IsRequired()
                        .HasConstraintName("FK__BookPromo__Promo__49C3F6B7");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Book", b =>
                {
                    b.HasOne("NhaSachDaiThang_BE_API.Models.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK__Books__CategoryI__3E52440B");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.BookImage", b =>
                {
                    b.HasOne("NhaSachDaiThang_BE_API.Models.Book", "Book")
                        .WithMany("BookImages")
                        .HasForeignKey("BookId")
                        .IsRequired()
                        .HasConstraintName("FK__BookImage__BookI__4222D4EF");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Customer", b =>
                {
                    b.HasOne("NhaSachDaiThang_BE_API.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__Customers__RoleI__534D60F1");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Employee", b =>
                {
                    b.HasOne("NhaSachDaiThang_BE_API.Models.Role", "Role")
                        .WithMany("Employees")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__Employees__RoleI__5812160E");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Order", b =>
                {
                    b.HasOne("NhaSachDaiThang_BE_API.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Orders__Customer__5DCAEF64");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.OrderDetail", b =>
                {
                    b.HasOne("NhaSachDaiThang_BE_API.Models.Book", "Book")
                        .WithMany("OrderDetails")
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK__OrderDeta__BookI__619B8048");

                    b.HasOne("NhaSachDaiThang_BE_API.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__OrderDeta__Order__60A75C0F");

                    b.Navigation("Book");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Payment", b =>
                {
                    b.HasOne("NhaSachDaiThang_BE_API.Models.Order", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__Payments__OrderI__6B24EA82");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Review", b =>
                {
                    b.HasOne("NhaSachDaiThang_BE_API.Models.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK__Reviews__BookID__71D1E811");

                    b.HasOne("NhaSachDaiThang_BE_API.Models.Customer", "Customer")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Reviews__Custome__70DDC3D8");

                    b.Navigation("Book");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("OrderVoucher", b =>
                {
                    b.HasOne("NhaSachDaiThang_BE_API.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK__OrderVouc__Order__6477ECF3");

                    b.HasOne("NhaSachDaiThang_BE_API.Models.Voucher", null)
                        .WithMany()
                        .HasForeignKey("VoucherId")
                        .IsRequired()
                        .HasConstraintName("FK__OrderVouc__Vouch__656C112C");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Book", b =>
                {
                    b.Navigation("BookImages");

                    b.Navigation("OrderDetails");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Category", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Customer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("NhaSachDaiThang_BE_API.Models.Role", b =>
                {
                    b.Navigation("Users");

                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
