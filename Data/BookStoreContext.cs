using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Models.Entities;

namespace NhaSachDaiThang_BE_API.Data;

public partial class BookStoreContext : DbContext
{
    public BookStoreContext()
    {
    }

    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Book { get; set; }


    public virtual DbSet<Category> Category { get; set; }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<Order> Order { get; set; }

    public virtual DbSet<OrderDetail> OrderDetail { get; set; }

    public virtual DbSet<Payment> Payment { get; set; }

    public virtual DbSet<Review> Review { get; set; }

    public virtual DbSet<Role> Role { get; set; }

    public virtual DbSet<Voucher> Voucher { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("BookStoreDb"));
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C227509BC471");

            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PromotionEndDate)
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.PageCount)
              .IsRequired(false);
            entity.Property(e => e.Promotion).HasMaxLength(30)
              .IsRequired(false);
            entity.Property(e => e.Size)
                .HasMaxLength(50)
                .IsRequired(false);
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(5, 2)")
                .IsRequired(false);
            entity.Property(e => e.IsDel).HasDefaultValue(false);
            entity.Property(e => e.MainImage).HasMaxLength(255);
            entity.Property(e => e.AdditionalImages)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);
            entity.Property(e => e.ModifyBy).HasMaxLength(100);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Publisher).HasMaxLength(100);

            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Books__CategoryI__3E52440B");
        });

        modelBuilder.Entity<SupplierBook>(
            entity =>
            {
                entity.HasKey(sb => new { sb.SupplierId, sb.BookId }).HasName("PK__SupplierBook");

                entity.Property(e => e.BookId).HasColumnName("BookID");
                entity.Property(e => e.SupplyDate).HasColumnType("datetime").IsRequired(false);
                entity.Property(e => e.SupplyPrice).HasColumnType("decimal(18, 2)").IsRequired(false);
                entity.Property(e => e.Quanlity).HasColumnName("Quantity");
                entity.Property(e => e.ModifyBy).HasMaxLength(100);
                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
                entity.Property(e => e.CreatedBy).HasMaxLength(100);
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.HasOne(db => db.Supplier)
                .WithMany(sb => sb.SupplierBooks)
                .HasForeignKey(entity => entity.SupplierId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(db=> db.Book)
                .WithMany(sb=> sb.SupplierBooks)
                .HasForeignKey(sb=>sb.BookId).OnDelete(DeleteBehavior.Restrict);

            }
        );

        modelBuilder.Entity<Supplier>(
            entity =>
            {
                entity.HasKey(e => e.SupplierId).HasName("PK__Supplyer");
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(50)
                  .IsRequired(false);
                entity.Property(e => e.Address).HasMaxLength(30).IsRequired(false);
                entity.Property(e => e.Phone).HasMaxLength(15).IsRequired(false);

                entity.Property(e => e.ModifyBy).HasMaxLength(100);
                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
                entity.Property(e => e.CreatedBy).HasMaxLength(100);
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");

            }
         );
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2BE21B8E70");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.IsDel).HasDefaultValue(false);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ModifyBy).HasMaxLength(100);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.HasOne(c=>c.ParentCategory).WithMany(c=> c.SubCategories).HasForeignKey(c=>c.CategoryId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Customer__A4AE64B80DDF41BD");

            entity.HasIndex(e => e.Email, "UQ__Customer__A9D1053471732474").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserId");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.RefreshToken).HasMaxLength(255);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RefreshTokenExpiryTime)
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(100);
            entity.Property(e => e.ModifyBy).HasMaxLength(100);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Customers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Customers__RoleI__534D60F1");
        });


        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF2225F30A");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserId");
            entity.Property(e => e.Phone).HasColumnName("Phone").HasMaxLength(15);
            entity.Property(e => e.ShippingAddress).HasColumnName("ShippingAddress").HasMaxLength(100);
            entity.Property(e => e.PaymentMethod).HasColumnName("PaymentMethod").HasMaxLength(60);
            entity.Property(e => e.Email).HasColumnName("Email").HasMaxLength(30);
            entity.Property(e => e.ModifyBy).HasMaxLength(100);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__Customer__5DCAEF64");

            entity.HasMany(d => d.Vouchers).WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderVoucher",
                    r => r.HasOne<Voucher>().WithMany()
                        .HasForeignKey("VoucherId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OrderVouc__Vouch__656C112C"),
                    l => l.HasOne<Order>().WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OrderVouc__Order__6477ECF3"),
                    j =>
                    {
                        j.HasKey("OrderId", "VoucherId").HasName("PK__OrderVou__C03EBC33F9DC2251");
                        j.ToTable("OrderVouchers");
                        j.IndexerProperty<int>("OrderId").HasColumnName("OrderID");
                        j.IndexerProperty<int>("VoucherId").HasColumnName("VoucherID");
                    });
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30CB3022B7C");

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Book).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__OrderDeta__BookI__619B8048");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__60A75C0F");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A588C4AA9B9");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifyBy).HasMaxLength(100);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Payments__OrderI__6B24EA82");
        });


        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AEC46D3806");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.Comment).HasMaxLength(500);
            //entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserId");
            //entity.Property(e => e.ModifyBy).HasMaxLength(100);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            //entity.Property(e => e.ReviewDate)
                //.HasDefaultValueSql("(getdate())")
                //.HasColumnType("datetime");

            entity.HasOne(d => d.Book).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Reviews__BookID__71D1E811");

            //entity.HasOne(d => d.Customer).WithMany(p => p.Reviews)
            //    .HasForeignKey(d => d.UserId)
            //    .HasConstraintName("FK__Reviews__Custome__70DDC3D8");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3AE06EA5E2");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__Vouchers__3AEE79C132561A24");

            entity.HasIndex(e => e.Code, "UQ__Vouchers__A25C5AA76011FE50").IsUnique();

            entity.Property(e => e.VoucherId).HasColumnName("VoucherID");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Discount).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.MinOrderValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ModifyBy).HasMaxLength(100);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
