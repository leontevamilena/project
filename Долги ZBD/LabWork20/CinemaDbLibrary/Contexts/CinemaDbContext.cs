using System;
using System.Collections.Generic;
using CinemaDbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaDbLibrary.Contexts;

public partial class CinemaDbContext : DbContext
{
    public CinemaDbContext()
    {
    }

    public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Frame> Frames { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ispp3103;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Frame>(entity =>
        {
            entity.ToTable("Frame");

            entity.Property(e => e.FrameId).ValueGeneratedNever();
            entity.Property(e => e.FileName).HasMaxLength(200);

            entity.HasOne(d => d.Movie).WithMany(p => p.Frames)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK_Frame_Movie");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.ToTable("Movie");

            entity.Property(e => e.AgeRating)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Duration).HasDefaultValue((short)90);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Year).HasDefaultValueSql("(datepart(year,getdate()))");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
