﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StartGrow.Data.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    AreasID = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.AreasID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false),
                    Apellido1 = table.Column<string>(maxLength: 20, nullable: false),
                    Apellido2 = table.Column<string>(maxLength: 20, nullable: false),
                    NIF = table.Column<int>(maxLength: 8, nullable: false),
                    Nacionalidad = table.Column<string>(maxLength: 40, nullable: false),
                    PaisDeResidencia = table.Column<string>(maxLength: 40, nullable: false),
                    Provincia = table.Column<string>(maxLength: 40, nullable: false),
                    Municipio = table.Column<string>(maxLength: 40, nullable: false),
                    Domicilio = table.Column<string>(maxLength: 40, nullable: false),
                    CodigoPostal = table.Column<int>(maxLength: 5, nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    DenominacionSocial = table.Column<string>(maxLength: 50, nullable: true),
                    FechaDeConstitucion = table.Column<DateTime>(nullable: true),
                    Actividad = table.Column<string>(maxLength: 100, nullable: true),
                    CIF = table.Column<int>(maxLength: 9, nullable: true),
                    PaisDelDomicilioSocial = table.Column<string>(maxLength: 25, nullable: true),
                    ProvinciaDelDomicilioSocial = table.Column<string>(maxLength: 25, nullable: true),
                    MunucipioDelDomicilioSocial = table.Column<string>(maxLength: 25, nullable: true),
                    DomicilioSocial = table.Column<string>(maxLength: 25, nullable: true),
                    PuestoDeTrabajo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proyecto",
                columns: table => new
                {
                    ProyectoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false),
                    MinInversion = table.Column<float>(nullable: false),
                    Plazo = table.Column<int>(nullable: false),
                    Interes = table.Column<float>(nullable: false),
                    Importe = table.Column<float>(nullable: false),
                    Progreso = table.Column<int>(nullable: false),
                    NumInversores = table.Column<int>(nullable: false),
                    FechaExpiracion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyecto", x => x.ProyectoId);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatingID = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.RatingID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Monedero",
                columns: table => new
                {
                    MonederoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InversorId = table.Column<int>(nullable: false),
                    InversorId1 = table.Column<string>(nullable: true),
                    Dinero = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monedero", x => x.MonederoId);
                    table.ForeignKey(
                        name: "FK_Monedero_AspNetUsers_InversorId1",
                        column: x => x.InversorId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inversion",
                columns: table => new
                {
                    InversionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InversorId = table.Column<int>(nullable: false),
                    InversorId1 = table.Column<string>(nullable: true),
                    ProyectoId = table.Column<int>(nullable: false),
                    Cuota = table.Column<float>(nullable: false),
                    Intereses = table.Column<float>(nullable: false),
                    Total = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inversion", x => x.InversionId);
                    table.ForeignKey(
                        name: "FK_Inversion_AspNetUsers_InversorId1",
                        column: x => x.InversorId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inversion_Proyecto_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "ProyectoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Solicitud",
                columns: table => new
                {
                    SolicitudID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProyectoId = table.Column<int>(nullable: false),
                    Estado = table.Column<int>(nullable: false),
                    FechaSolicitud = table.Column<DateTime>(nullable: false),
                    TrabajadorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitud", x => x.SolicitudID);
                    table.ForeignKey(
                        name: "FK_Solicitud_Proyecto_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "ProyectoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Solicitud_AspNetUsers_TrabajadorId",
                        column: x => x.TrabajadorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InversionRecuperadas",
                columns: table => new
                {
                    InversionRecuperadaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaRecuperacion = table.Column<DateTime>(nullable: false),
                    Comentario = table.Column<string>(nullable: false),
                    CantidadRecuperada = table.Column<float>(nullable: false),
                    MonederoId = table.Column<int>(nullable: false),
                    InversionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InversionRecuperadas", x => x.InversionRecuperadaId);
                    table.ForeignKey(
                        name: "FK_InversionRecuperadas_Inversion_InversionId",
                        column: x => x.InversionId,
                        principalTable: "Inversion",
                        principalColumn: "InversionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InversionRecuperadas_Monedero_MonederoId",
                        column: x => x.MonederoId,
                        principalTable: "Monedero",
                        principalColumn: "MonederoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TiposInversiones",
                columns: table => new
                {
                    TiposInversionesID = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    InversionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposInversiones", x => x.TiposInversionesID);
                    table.ForeignKey(
                        name: "FK_TiposInversiones_Inversion_InversionId",
                        column: x => x.InversionId,
                        principalTable: "Inversion",
                        principalColumn: "InversionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Preferencias",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TiposInversionesID = table.Column<string>(nullable: true),
                    RatingID = table.Column<string>(nullable: true),
                    AreasID = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<int>(nullable: false),
                    ApplicationUserId1 = table.Column<string>(nullable: true),
                    ProyectoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferencias", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Preferencias_AspNetUsers_ApplicationUserId1",
                        column: x => x.ApplicationUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Preferencias_Areas_AreasID",
                        column: x => x.AreasID,
                        principalTable: "Areas",
                        principalColumn: "AreasID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Preferencias_Proyecto_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "ProyectoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Preferencias_Rating_RatingID",
                        column: x => x.RatingID,
                        principalTable: "Rating",
                        principalColumn: "RatingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Preferencias_TiposInversiones_TiposInversionesID",
                        column: x => x.TiposInversionesID,
                        principalTable: "TiposInversiones",
                        principalColumn: "TiposInversionesID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Inversion_InversorId1",
                table: "Inversion",
                column: "InversorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Inversion_ProyectoId",
                table: "Inversion",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_InversionRecuperadas_InversionId",
                table: "InversionRecuperadas",
                column: "InversionId");

            migrationBuilder.CreateIndex(
                name: "IX_InversionRecuperadas_MonederoId",
                table: "InversionRecuperadas",
                column: "MonederoId");

            migrationBuilder.CreateIndex(
                name: "IX_Monedero_InversorId1",
                table: "Monedero",
                column: "InversorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Preferencias_ApplicationUserId1",
                table: "Preferencias",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Preferencias_AreasID",
                table: "Preferencias",
                column: "AreasID");

            migrationBuilder.CreateIndex(
                name: "IX_Preferencias_ProyectoId",
                table: "Preferencias",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Preferencias_RatingID",
                table: "Preferencias",
                column: "RatingID");

            migrationBuilder.CreateIndex(
                name: "IX_Preferencias_TiposInversionesID",
                table: "Preferencias",
                column: "TiposInversionesID");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_ProyectoId",
                table: "Solicitud",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_TrabajadorId",
                table: "Solicitud",
                column: "TrabajadorId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposInversiones_InversionId",
                table: "TiposInversiones",
                column: "InversionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "InversionRecuperadas");

            migrationBuilder.DropTable(
                name: "Preferencias");

            migrationBuilder.DropTable(
                name: "Solicitud");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Monedero");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "TiposInversiones");

            migrationBuilder.DropTable(
                name: "Inversion");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Proyecto");
        }
    }
}
