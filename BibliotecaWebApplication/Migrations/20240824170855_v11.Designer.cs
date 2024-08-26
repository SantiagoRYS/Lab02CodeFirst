﻿// <auto-generated />
using System;
using BibliotecaWebApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BibliotecaWebApplication.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240824170855_v11")]
    partial class v11
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BibliotecaWebApplication.Models.Autor", b =>
                {
                    b.Property<Guid>("AutorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nacionalidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AutorId");

                    b.ToTable("Autores");
                });

            modelBuilder.Entity("BibliotecaWebApplication.Models.AutorLibro", b =>
                {
                    b.Property<Guid>("AutorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LibroId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AutorId", "LibroId");

                    b.HasIndex("LibroId");

                    b.ToTable("AutorLibros");
                });

            modelBuilder.Entity("BibliotecaWebApplication.Models.Ejemplar", b =>
                {
                    b.Property<int>("EjemplarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EjemplarId"));

                    b.Property<int>("EstanteId")
                        .HasColumnType("int");

                    b.Property<int>("PublicacionId")
                        .HasColumnType("int");

                    b.HasKey("EjemplarId");

                    b.HasIndex("EstanteId");

                    b.HasIndex("PublicacionId");

                    b.ToTable("Ejemplares");
                });

            modelBuilder.Entity("BibliotecaWebApplication.Models.Estante", b =>
                {
                    b.Property<int>("EstanteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EstanteId"));

                    b.Property<int>("CodigoEstante")
                        .HasColumnType("int");

                    b.Property<int>("EstanteriaId")
                        .HasColumnType("int");

                    b.HasKey("EstanteId");

                    b.HasIndex("EstanteriaId");

                    b.ToTable("Estantes");
                });

            modelBuilder.Entity("BibliotecaWebApplication.Models.Estanteria", b =>
                {
                    b.Property<int>("EstanteriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EstanteriaId"));

                    b.Property<double>("Alto")
                        .HasColumnType("float");

                    b.Property<double>("Ancho")
                        .HasColumnType("float");

                    b.HasKey("EstanteriaId");

                    b.ToTable("Estanterias");
                });

            modelBuilder.Entity("BibliotecaWebApplication.Models.Libro", b =>
                {
                    b.Property<Guid>("LibroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Formato")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumeroPaginas")
                        .HasColumnType("int");

                    b.Property<int>("PublicacionId")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LibroId");

                    b.HasIndex("PublicacionId");

                    b.ToTable("Libros");
                });

            modelBuilder.Entity("BibliotecaWebApplication.Models.Publicacion", b =>
                {
                    b.Property<int>("PublicacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PublicacionId"));

                    b.HasKey("PublicacionId");

                    b.ToTable("Publicaciones");
                });

            modelBuilder.Entity("BibliotecaWebApplication.Models.Revista", b =>
                {
                    b.Property<Guid>("RevistaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FechaPublicacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PublicacionId")
                        .HasColumnType("int");

                    b.HasKey("RevistaId");

                    b.HasIndex("PublicacionId");

                    b.ToTable("Revistas");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "ProviderKey");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("BibliotecaWebApplication.Models.AutorLibro", b =>
                {
                    b.HasOne("BibliotecaWebApplication.Models.Autor", "Autor")
                        .WithMany("AutorLibros")
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BibliotecaWebApplication.Models.Libro", "Libro")
                        .WithMany("AutorLibros")
                        .HasForeignKey("LibroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Autor");

                    b.Navigation("Libro");
                });

            modelBuilder.Entity("BibliotecaWebApplication.Models.Ejemplar", b =>
                {
                    b.HasOne("BibliotecaWebApplication.Models.Estante", "Estante")
                        .WithMany("Ejemplares")
                        .HasForeignKey("EstanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BibliotecaWebApplication.Models.Publicacion", "Publicacion")
                        .WithMany("Ejemplares")
                        .HasForeignKey("PublicacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estante");

                    b.Navigation("Publicacion");
                });

            modelBuilder.Entity("BibliotecaWebApplication.Models.Estante", b =>
                {
                    b.HasOne("BibliotecaWebApplication.Models.Estanteria", "Estanteria")
                        .WithMany("Estantes")
                        .HasForeignKey("EstanteriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estanteria");
                });

            modelBuilder.Entity("BibliotecaWebApplication.Models.Libro", b =>
                {
                    b.HasOne("BibliotecaWebApplication.Models.Publicacion", "Publicacion")
                        .WithMany("Libros")
                        .HasForeignKey("PublicacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publicacion");
                });

            modelBuilder.Entity("BibliotecaWebApplication.Models.Revista", b =>
                {
                    b.HasOne("BibliotecaWebApplication.Models.Publicacion", "Publicacion")
                        .WithMany("Revistas")
                        .HasForeignKey("PublicacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publicacion");
                });

            modelBuilder.Entity("BibliotecaWebApplication.Models.Autor", b =>
                {
                    b.Navigation("AutorLibros");
                });

            modelBuilder.Entity("BibliotecaWebApplication.Models.Estante", b =>
                {
                    b.Navigation("Ejemplares");
                });

            modelBuilder.Entity("BibliotecaWebApplication.Models.Estanteria", b =>
                {
                    b.Navigation("Estantes");
                });

            modelBuilder.Entity("BibliotecaWebApplication.Models.Libro", b =>
                {
                    b.Navigation("AutorLibros");
                });

            modelBuilder.Entity("BibliotecaWebApplication.Models.Publicacion", b =>
                {
                    b.Navigation("Ejemplares");

                    b.Navigation("Libros");

                    b.Navigation("Revistas");
                });
#pragma warning restore 612, 618
        }
    }
}
