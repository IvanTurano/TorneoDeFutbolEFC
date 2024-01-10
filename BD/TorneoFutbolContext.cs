using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BD;

public partial class TorneoFutbolContext : DbContext
{
    public TorneoFutbolContext()
    {
    }

    public TorneoFutbolContext(DbContextOptions<TorneoFutbolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Entrenador> Entrenadors { get; set; }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Jugador> Jugadors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(optionsBuilder.IsConfigured != null)
        {
            optionsBuilder.UseSqlServer("Server=IVAN\\SQLEXPRESS; Database=TorneoFutbol; Trusted_Connection=True; TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entrenador>(entity =>
        {
            entity.ToTable("Entrenador");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumEquipo).HasColumnName("numEquipo");

            entity.HasOne(d => d.NumEquipoNavigation).WithMany(p => p.Entrenadors)
                .HasForeignKey(d => d.NumEquipo)
                .HasConstraintName("FK_Entrenador_Equipo");
        });

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Equipo");

            entity.Property(e => e.Codigo)
                .ValueGeneratedNever()
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Jugador>(entity =>
        {
            entity.ToTable("Jugador");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumEquipo).HasColumnName("numEquipo");
            entity.Property(e => e.Posicion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("posicion");

            entity.HasOne(d => d.NumEquipoNavigation).WithMany(p => p.Jugadors)
                .HasForeignKey(d => d.NumEquipo)
                .HasConstraintName("FK_Jugador_Equipo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
