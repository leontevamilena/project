using System;
using System.Collections.Generic;
using AuthLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthLibrary.Contexts;

public partial class CinemaDbContext : DbContext
{
    public CinemaDbContext()
    {
    }

    public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CinemaPrivilege> CinemaPrivileges { get; set; }

    public virtual DbSet<CinemaUser> CinemaUsers { get; set; }

    public virtual DbSet<CinemaUserRole> CinemaUserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ispp3103;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CinemaPrivilege>(entity =>
        {
            entity.HasKey(e => e.PrivilegeId);

            entity.ToTable("CinemaPrivilege");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasMany(d => d.UserRoles).WithMany(p => p.Privileges)
                .UsingEntity<Dictionary<string, object>>(
                    "CinemaRolePrivilege",
                    r => r.HasOne<CinemaUserRole>().WithMany()
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CinemaRolePrivilege_CinemaUserRole"),
                    l => l.HasOne<CinemaPrivilege>().WithMany()
                        .HasForeignKey("PrivilegeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CinemaRolePrivilege_CinemaPrivilege"),
                    j =>
                    {
                        j.HasKey("PrivilegeId", "UserRoleId");
                        j.ToTable("CinemaRolePrivilege");
                    });
        });

        modelBuilder.Entity<CinemaUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("CinemaUser");

            entity.Property(e => e.HashPassword).HasMaxLength(200);
            entity.Property(e => e.Login).HasMaxLength(50);

            entity.HasOne(d => d.UserRole).WithMany(p => p.CinemaUsers)
                .HasForeignKey(d => d.UserRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CinemaUser_CinemaUserRole");
        });

        modelBuilder.Entity<CinemaUserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId);

            entity.ToTable("CinemaUserRole");

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
