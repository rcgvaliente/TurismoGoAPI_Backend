using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TurismoGoDOMAIN.Core.Entities;

namespace TurismoGoDOMAIN.Infraestructure.Data;

public partial class TurismoGoBdContext : DbContext
{
    public TurismoGoBdContext(DbContextOptions<TurismoGoBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actividades> Actividades { get; set; }

    public virtual DbSet<Historial> Historial { get; set; }

    public virtual DbSet<Reservas> Reservas { get; set; }

    public virtual DbSet<Resenias> Resenias { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=TurismoGo_BD;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actividades>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activida__3213E83F3F7BB7C9");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capacidad).HasColumnName("capacidad");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Destino)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("destino");
            entity.Property(e => e.EmpresaId).HasColumnName("empresa_id");
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.FechaPublicacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_publicacion");
            entity.Property(e => e.Itinerario)
                .HasColumnType("text")
                .HasColumnName("itinerario");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("titulo");

            entity.HasOne(d => d.Empresa).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Actividad__empre__4E88ABD4");
        });

        modelBuilder.Entity<Historial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3213E83FB47A4B72");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActividadId).HasColumnName("actividad_id");
            entity.Property(e => e.FechaActividad)
                .HasColumnType("datetime")
                .HasColumnName("fecha_actividad");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Actividad).WithMany(p => p.Historial)
                .HasForeignKey(d => d.ActividadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__activ__5BE2A6F2");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Historial)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__usuar__5AEE82B9");
        });

        modelBuilder.Entity<Reservas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reservas__3213E83F0FC855B5");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActividadId).HasColumnName("actividad_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.FechaReserva)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_reserva");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Actividad).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.ActividadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__activi__534D60F1");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__usuari__52593CB8");
        });

        modelBuilder.Entity<Resenias>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Resenias__3213E83F37E69578");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActividadId).HasColumnName("actividad_id");
            entity.Property(e => e.Calificacion).HasColumnName("calificacion");
            entity.Property(e => e.Comentario)
                .HasColumnType("text")
                .HasColumnName("comentario");
            entity.Property(e => e.FechaPublicacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_publicacion");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Actividad).WithMany(p => p.Resenias)
                .HasForeignKey(d => d.ActividadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Resenias__activid__5812160E");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Resenias)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Resenias__usuario__571DF1D5");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3213E83F939778A2");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__AB6E6164C4A8EA94").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
