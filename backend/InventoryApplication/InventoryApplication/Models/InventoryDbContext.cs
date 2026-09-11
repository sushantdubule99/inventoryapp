using System;
using Microsoft.EntityFrameworkCore;

namespace InventoryApplication.Models;

public partial class InventoryDbContext : DbContext
{
    public InventoryDbContext()
    {
    }

    public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerTbl> CustomerTbls { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<BillTbl> BillTbls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=InventoryCon");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        modelBuilder.Entity<CustomerTbl>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.ToTable("CustomerTbl");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedOnAdd()
                .HasColumnName("customerId");

            entity.Property(e => e.CustomerEmail)
                .HasColumnName("customerEmail");

            entity.Property(e => e.CustomerPhone)
                .HasColumnName("customerPhone");

            entity.Property(e => e.RegistrationDate)
                .HasColumnName("registrationDate");
        });


       
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.ToTable("Inventory");

            entity.Property(e => e.ProductId)
                .ValueGeneratedOnAdd()
                .HasColumnName("productId");

            entity.Property(e => e.ProductName)
                .HasColumnName("productName");

            entity.Property(e => e.StockAvailable)
                .HasColumnName("stockAvailable");

            entity.Property(e => e.RecordStock)
                .HasColumnName("recordStock");

            entity.Property(e => e.Price)
               .HasColumnName("Price");


        });


        
        modelBuilder.Entity<BillTbl>(entity =>
        {
            entity.HasKey(e => e.BillId);

            entity.ToTable("billTbl");

            entity.Property(e => e.BillId)
                .ValueGeneratedOnAdd()
                .HasColumnName("billId");

            entity.Property(e => e.CustomerId)
                .HasColumnName("customerId");

            entity.Property(e => e.ProductId)
                .HasColumnName("productId");

            entity.Property(e => e.Quantity)
                .HasColumnName("quantity");

            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");

            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");

            entity.Property(e => e.BillDate)
                .HasColumnType("datetime")
                .HasColumnName("billDate");


          
            entity.HasOne<CustomerTbl>()
                .WithMany()
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);


          
            entity.HasOne<Inventory>()
                .WithMany()
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}