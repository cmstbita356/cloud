using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Cloud.Models;

namespace Cloud.Models;

public partial class Group08ElectricmtContext : DbContext
{
    public Group08ElectricmtContext()
    {
    }

    public Group08ElectricmtContext(DbContextOptions<Group08ElectricmtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCompany> TblCompanies { get; set; }

    public virtual DbSet<TblEmployee> TblEmployees { get; set; }

    public virtual DbSet<TblItem> TblItems { get; set; }

    public virtual DbSet<TblOrder> TblOrders { get; set; }

    public virtual DbSet<TblOrderdetail> TblOrderdetails { get; set; }

    public virtual DbSet<TblTruck> TblTrucks { get; set; }

    public virtual DbSet<TblTruckItem> TblTruckItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCompany>(entity =>
        {
            entity.HasKey(e => e.CompId).HasName("PK__tbl_comp__531653DD6BDF3351");

            entity.ToTable("tbl_company");

            entity.Property(e => e.CompId)
                .ValueGeneratedNever()
                .HasColumnName("comp_id");
            entity.Property(e => e.CompEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("comp_email");
            entity.Property(e => e.CompName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("comp_name");
            entity.Property(e => e.CompPassword)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("comp_password");
            entity.Property(e => e.CompPhone)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("comp_phone");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<TblEmployee>(entity =>
        {
            entity.HasKey(e => e.EmplId).HasName("PK__tbl_empl__4773921989E4A3BC");

            entity.ToTable("tbl_employee");

            entity.Property(e => e.EmplId)
                .ValueGeneratedNever()
                .HasColumnName("empl_id");
            entity.Property(e => e.CompId).HasColumnName("comp_id");
            entity.Property(e => e.EmplEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("empl_email");
            entity.Property(e => e.EmplFirstname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("empl_firstname");
            entity.Property(e => e.EmplLastname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("empl_lastname");
            entity.Property(e => e.EmplPassword)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("empl_password");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<TblItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__tbl_item__52020FDD99FF67AF");

            entity.ToTable("tbl_item");

            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnName("item_id");
            entity.Property(e => e.CompId).HasColumnName("comp_id");
            entity.Property(e => e.ItemInstock).HasColumnName("item_instock");
            entity.Property(e => e.ItemName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("item_name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<TblOrder>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__tbl_orde__779B7C58622B974F");

            entity.ToTable("tbl_order");

            entity.Property(e => e.ReportId)
                .ValueGeneratedNever()
                .HasColumnName("report_id");
            entity.Property(e => e.CompId).HasColumnName("comp_id");
            entity.Property(e => e.OrderDate)
                .HasColumnType("date")
                .HasColumnName("order_date");
            entity.Property(e => e.OrderDelivered).HasColumnName("order_delivered");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<TblOrderdetail>(entity =>
        {
            entity.HasKey(e => e.EntryId).HasName("PK__tbl_orde__810FDCE191D875DF");

            entity.ToTable("tbl_orderdetail");

            entity.Property(e => e.EntryId)
                .ValueGeneratedNever()
                .HasColumnName("entry_id");
            entity.Property(e => e.CompId).HasColumnName("comp_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.ItemQuantity).HasColumnName("item_quantity");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<TblTruck>(entity =>
        {
            entity.HasKey(e => e.TruckId).HasName("PK__tbl_truc__27C8351AF6CD12FB");

            entity.ToTable("tbl_truck");

            entity.Property(e => e.TruckId)
                .ValueGeneratedNever()
                .HasColumnName("truck_id");
            entity.Property(e => e.CompId).HasColumnName("comp_id");
            entity.Property(e => e.EmplId).HasColumnName("empl_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TruckLicense)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("truck_license");
            entity.Property(e => e.TruckModel)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("truck_model");
        });

        modelBuilder.Entity<TblTruckItem>(entity =>
        {
            entity.HasKey(e => e.EntryId).HasName("PK__tbl_truc__810FDCE1BC18D6BC");

            entity.ToTable("tbl_truck_item");

            entity.Property(e => e.EntryId)
                .ValueGeneratedNever()
                .HasColumnName("entry_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.ItemQuantity).HasColumnName("item_quantity");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TruckId).HasColumnName("truck_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
