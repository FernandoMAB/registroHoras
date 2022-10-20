using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace registroHoras.Models
{
    public partial class pruebaContext : DbContext
    {
        public pruebaContext()
        {
        }

        public pruebaContext(DbContextOptions<pruebaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RegistroEntradum> RegistroEntrada { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=prueba;Username=postgres;Password=admin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegistroEntradum>(entity =>
            {
                entity.ToTable("registro_entrada");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Estado)
                    .HasColumnType("char")
                    .HasColumnName("estado");

                entity.Property(e => e.Fecha).HasColumnName("fecha").HasColumnType("timestamp without time zone");

                entity.Property(e => e.Usuario)
                    .HasColumnType("character varying")
                    .HasColumnName("usuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Apllido)
                    .HasColumnType("character varying")
                    .HasColumnName("apllido");

                entity.Property(e => e.Clave)
                    .HasColumnType("character varying")
                    .HasColumnName("clave");

                entity.Property(e => e.Correo)
                    .HasColumnType("character varying")
                    .HasColumnName("correo");

                entity.Property(e => e.Nombre)
                    .HasColumnType("character varying")
                    .HasColumnName("nombre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
