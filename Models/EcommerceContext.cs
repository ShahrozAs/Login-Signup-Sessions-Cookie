using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Identity_in_WebApi.Models;

public partial class EcommerceContext : DbContext
{
    public EcommerceContext()
    {
    }

    public EcommerceContext(DbContextOptions<EcommerceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PersonalInfo> PersonalInfos { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonalInfo>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Personal__AA2FFB853EBF9751");

            entity.ToTable("Personal_Info");

            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Address).IsUnicode(false);
            entity.Property(e => e.City).IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LoginTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProfileImage)
                .HasColumnType("image")
                .HasColumnName("Profile_Image");
            entity.Property(e => e.UserType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("userType");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.PId).HasName("PK__Product__A3420A57DC753A51");

            entity.ToTable("Product");

            entity.Property(e => e.PId).HasColumnName("P_Id");
            entity.Property(e => e.AddedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Added_date");
            entity.Property(e => e.PName)
                .HasMaxLength(50)
                .HasColumnName("P_Name");
            entity.Property(e => e.PStoke).HasColumnName("P_Stoke");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");

           
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
