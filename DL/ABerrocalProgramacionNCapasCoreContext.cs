using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DL
{
    public partial class ABerrocalProgramacionNCapasCoreContext : DbContext
    {
        public ABerrocalProgramacionNCapasCoreContext()
        {
        }

        public ABerrocalProgramacionNCapasCoreContext(DbContextOptions<ABerrocalProgramacionNCapasCoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Areas { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<MetodoPago> MetodoPagos { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Proveedor> Proveedors { get; set; } = null!;
        public virtual DbSet<VentaProducto> VentaProductos { get; set; } = null!;
        public virtual DbSet<Ventum> Venta { get; set; } = null!;
        public virtual DbSet<VwProductosGetAll> VwProductosGetAlls { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.; Database=ABerrocalProgramacionNCapasCore; TrustServerCertificate=True; Trusted_Connection=True; User ID=sa; Password=pass@word1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(e => e.IdArea)
                    .HasName("PK__Area__2FC141AAFD742DDD");

                entity.ToTable("Area");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.IdDepartamento)
                    .HasName("PK__Departam__787A433DD400A711");

                entity.ToTable("Departamento");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.Departamentos)
                    .HasForeignKey(d => d.IdArea)
                    .HasConstraintName("FK__Departame__IdAre__3B75D760");
            });

            modelBuilder.Entity<MetodoPago>(entity =>
            {
                entity.HasKey(e => e.IdMetodoPago)
                    .HasName("PK__MedodoPa__6F49A9BE64D0B211");

                entity.ToTable("MetodoPago");

                entity.Property(e => e.Metodo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__0988921086E51703");

                entity.ToTable("Producto");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PrecioUnitorio).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor)
                    .HasName("PK__Proveedo__E8B631AF7FA26CA8");

                entity.ToTable("Proveedor");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VentaProducto>(entity =>
            {
                entity.HasKey(e => e.IdVentaProducto)
                    .HasName("PK__VentaPro__E4CB50990D46A688");

                entity.ToTable("VentaProducto");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.VentaProductos)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__VentaProd__IdPro__3E52440B");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.VentaProductos)
                    .HasForeignKey(d => d.IdVenta)
                    .HasConstraintName("FK__VentaProd__IdVen__4BAC3F29");
            });

            modelBuilder.Entity<Ventum>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("PK__Venta__BC1240BDE6613D6F");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.IdCliente).HasMaxLength(450);

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__Venta__IdCliente__49C3F6B7");

                entity.HasOne(d => d.IdMetodoPagoNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdMetodoPago)
                    .HasConstraintName("FK__Venta__IdMetodoP__4AB81AF0");
            });

            modelBuilder.Entity<VwProductosGetAll>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwProductosGetAll");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NombreArea)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreDepartamento)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NombreProveedor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrecioUnitorio).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
