﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using StartGrow.Data;
using System;

namespace StartGrow.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("StartGrow.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Apellido1")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Apellido2")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("CodPost")
                        .HasMaxLength(5);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Domicilio")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Municipio")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<int>("NIF")
                        .HasMaxLength(8);

                    b.Property<string>("Nacionalidad")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PaisDeResidencia")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Provincia")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ApplicationUser");
                });

            modelBuilder.Entity("StartGrow.Models.Areas", b =>
                {
                    b.Property<int>("AreasId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("AreasId");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("StartGrow.Models.Inversion", b =>
                {
                    b.Property<int>("InversionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId")
                        .IsRequired();

                    b.Property<float>("Cuota");

                    b.Property<float>("Intereses");

                    b.Property<string>("InversorId");

                    b.Property<int>("ProyectoId");

                    b.Property<int>("TipoInversionesId");

                    b.Property<float>("Total");

                    b.HasKey("InversionId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("InversorId");

                    b.HasIndex("ProyectoId");

                    b.HasIndex("TipoInversionesId");

                    b.ToTable("Inversion");
                });

            modelBuilder.Entity("StartGrow.Models.InversionRecuperada", b =>
                {
                    b.Property<int>("InversionRecuperadaId")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("CantidadRecuperada");

                    b.Property<string>("Comentario")
                        .IsRequired();

                    b.Property<DateTime>("FechaRecuperacion");

                    b.Property<int>("InversionId");

                    b.Property<int>("MonederoId");

                    b.HasKey("InversionRecuperadaId");

                    b.HasIndex("InversionId");

                    b.HasIndex("MonederoId");

                    b.ToTable("InversionRecuperada");
                });

            modelBuilder.Entity("StartGrow.Models.Monedero", b =>
                {
                    b.Property<int>("MonederoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId")
                        .IsRequired();

                    b.Property<decimal>("Dinero");

                    b.Property<string>("InversorId");

                    b.HasKey("MonederoId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("InversorId");

                    b.ToTable("Monedero");
                });

            modelBuilder.Entity("StartGrow.Models.Preferencias", b =>
                {
                    b.Property<int>("PreferenciasId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId")
                        .IsRequired();

                    b.Property<int>("AreasId");

                    b.Property<string>("InversorId");

                    b.Property<int>("RatingId");

                    b.Property<int>("TiposInversionesId");

                    b.HasKey("PreferenciasId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("AreasId");

                    b.HasIndex("InversorId");

                    b.HasIndex("RatingId");

                    b.HasIndex("TiposInversionesId");

                    b.ToTable("Preferencias");
                });

            modelBuilder.Entity("StartGrow.Models.Proyecto", b =>
                {
                    b.Property<int>("ProyectoId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FechaExpiracion");

                    b.Property<float>("Importe");

                    b.Property<float>("Interes");

                    b.Property<float>("MinInversion");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.Property<int>("NumInversores");

                    b.Property<int>("Plazo");

                    b.Property<int>("Progreso");

                    b.Property<int>("RatingId");

                    b.HasKey("ProyectoId");

                    b.HasIndex("RatingId");

                    b.ToTable("Proyecto");
                });

            modelBuilder.Entity("StartGrow.Models.ProyectoAreas", b =>
                {
                    b.Property<int>("ProyectoAreasId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AreasId");

                    b.Property<int>("ProyectoId");

                    b.HasKey("ProyectoAreasId");

                    b.HasIndex("AreasId");

                    b.HasIndex("ProyectoId");

                    b.ToTable("ProyectoAreas");
                });

            modelBuilder.Entity("StartGrow.Models.ProyectoTiposInversiones", b =>
                {
                    b.Property<int>("ProyectoTiposInversionesId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProyectoId");

                    b.Property<int>("TiposInversionesId");

                    b.HasKey("ProyectoTiposInversionesId");

                    b.HasIndex("ProyectoId");

                    b.HasIndex("TiposInversionesId");

                    b.ToTable("ProyectoTiposInversiones");
                });

            modelBuilder.Entity("StartGrow.Models.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("RatingId");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("StartGrow.Models.Solicitud", b =>
                {
                    b.Property<int>("SolicitudId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Estado");

                    b.Property<DateTime>("FechaSolicitud");

                    b.Property<int>("ProyectoId");

                    b.Property<string>("TrabajadorId")
                        .IsRequired();

                    b.HasKey("SolicitudId");

                    b.HasIndex("ProyectoId");

                    b.HasIndex("TrabajadorId");

                    b.ToTable("Solicitud");
                });

            modelBuilder.Entity("StartGrow.Models.TiposInversiones", b =>
                {
                    b.Property<int>("TiposInversionesId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("TiposInversionesId");

                    b.ToTable("TiposInversiones");
                });

            modelBuilder.Entity("StartGrow.Models.Inversor", b =>
                {
                    b.HasBaseType("StartGrow.Models.ApplicationUser");


                    b.ToTable("Inversor");

                    b.HasDiscriminator().HasValue("Inversor");
                });

            modelBuilder.Entity("StartGrow.Models.Trabajador", b =>
                {
                    b.HasBaseType("StartGrow.Models.ApplicationUser");

                    b.Property<string>("PuestoDeTrabajo")
                        .IsRequired();

                    b.ToTable("Trabajador");

                    b.HasDiscriminator().HasValue("Trabajador");
                });

            modelBuilder.Entity("StartGrow.Models.Empresa", b =>
                {
                    b.HasBaseType("StartGrow.Models.Inversor");

                    b.Property<string>("Actividad")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("CIF")
                        .HasMaxLength(9);

                    b.Property<string>("DenominacionSocial")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("DomicilioSocial")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<DateTime>("FechaDeConstitucion");

                    b.Property<string>("MunicipioDelDomicilioSocial")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("PaisDelDomicilioSocial")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("ProvinciaDelDomicilioSocial")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.ToTable("Empresa");

                    b.HasDiscriminator().HasValue("Empresa");
                });

            modelBuilder.Entity("StartGrow.Models.Particular", b =>
                {
                    b.HasBaseType("StartGrow.Models.Inversor");


                    b.ToTable("Particular");

                    b.HasDiscriminator().HasValue("Particular");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("StartGrow.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("StartGrow.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StartGrow.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("StartGrow.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StartGrow.Models.Inversion", b =>
                {
                    b.HasOne("StartGrow.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StartGrow.Models.Inversor")
                        .WithMany("Inversiones")
                        .HasForeignKey("InversorId");

                    b.HasOne("StartGrow.Models.Proyecto", "Proyecto")
                        .WithMany("Inversiones")
                        .HasForeignKey("ProyectoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StartGrow.Models.TiposInversiones", "TipoInversiones")
                        .WithMany("Inversiones")
                        .HasForeignKey("TipoInversionesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StartGrow.Models.InversionRecuperada", b =>
                {
                    b.HasOne("StartGrow.Models.Inversion", "Inversion")
                        .WithMany()
                        .HasForeignKey("InversionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StartGrow.Models.Monedero", "Monedero")
                        .WithMany("InversionesRecuperadas")
                        .HasForeignKey("MonederoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StartGrow.Models.Monedero", b =>
                {
                    b.HasOne("StartGrow.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StartGrow.Models.Inversor")
                        .WithMany("Monedero")
                        .HasForeignKey("InversorId");
                });

            modelBuilder.Entity("StartGrow.Models.Preferencias", b =>
                {
                    b.HasOne("StartGrow.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StartGrow.Models.Areas", "Areas")
                        .WithMany("Preferencias")
                        .HasForeignKey("AreasId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StartGrow.Models.Inversor")
                        .WithMany("Preferencias")
                        .HasForeignKey("InversorId");

                    b.HasOne("StartGrow.Models.Rating", "Rating")
                        .WithMany("Preferencias")
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StartGrow.Models.TiposInversiones", "TiposInversiones")
                        .WithMany("Preferencias")
                        .HasForeignKey("TiposInversionesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StartGrow.Models.Proyecto", b =>
                {
                    b.HasOne("StartGrow.Models.Rating", "Rating")
                        .WithMany("Proyectos")
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StartGrow.Models.ProyectoAreas", b =>
                {
                    b.HasOne("StartGrow.Models.Areas", "Areas")
                        .WithMany("ProyectoAreas")
                        .HasForeignKey("AreasId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StartGrow.Models.Proyecto", "Proyecto")
                        .WithMany("ProyectoAreas")
                        .HasForeignKey("ProyectoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StartGrow.Models.ProyectoTiposInversiones", b =>
                {
                    b.HasOne("StartGrow.Models.Proyecto", "Proyecto")
                        .WithMany("ProyectoTiposInversiones")
                        .HasForeignKey("ProyectoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StartGrow.Models.TiposInversiones", "TiposInversiones")
                        .WithMany("ProyectoTiposInversiones")
                        .HasForeignKey("TiposInversionesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StartGrow.Models.Solicitud", b =>
                {
                    b.HasOne("StartGrow.Models.Proyecto", "Proyecto")
                        .WithMany()
                        .HasForeignKey("ProyectoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StartGrow.Models.Trabajador", "Trabajador")
                        .WithMany("SolicitudesTratadas")
                        .HasForeignKey("TrabajadorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
