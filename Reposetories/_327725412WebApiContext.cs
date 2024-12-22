using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace Reposetories;

public partial class _327725412WebApiContext : DbContext
{
    public _327725412WebApiContext()
    {
    }

    public _327725412WebApiContext(DbContextOptions<_327725412WebApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SRV2\\PUPILS;Database=327725412_webApi;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_ORDERS");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("ORDER_ID");
            entity.Property(e => e.OrderDate).HasColumnName("ORDER_DATE");
            entity.Property(e => e.OrderSum).HasColumnName("ORDER_SUM");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_User");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("Order_Item");

            entity.Property(e => e.OrderItemId).HasColumnName("ORDER_ITEM_ID");
            entity.Property(e => e.OrderId).HasColumnName("ORDER_ID");
            entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Item_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Item_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(25);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Product_Category");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("PASSWORD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
