using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StartGrow.Data.Migrations
{
    public partial class AddingVariasClases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "Apellido1",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Apellido2",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CodPost",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Domicilio",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Municipio",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nacionalidad",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nif",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaisResidencia",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Provincia",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    AreasID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.AreasID);
                });

            migrationBuilder.CreateTable(
                name: "Monedero",
                columns: table => new
                {
                    MonederoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Dinero = table.Column<float>(type: "real", nullable: false),
                    InversorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monedero", x => x.MonederoId);
                    table.ForeignKey(
                        name: "FK_Monedero_AspNetUsers_InversorId",
                        column: x => x.InversorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatingID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.RatingID);
                });

            migrationBuilder.CreateTable(
                name: "TiposInversiones",
                columns: table => new
                {
                    TiposInversionesID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposInversiones", x => x.TiposInversionesID);
                });

            migrationBuilder.CreateTable(
                name: "Preferencias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AreasID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InversorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RatingID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TiposInversionesID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferencias", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Preferencias_Areas_AreasID",
                        column: x => x.AreasID,
                        principalTable: "Areas",
                        principalColumn: "AreasID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Preferencias_AspNetUsers_InversorId",
                        column: x => x.InversorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "Proyecto",
                columns: table => new
                {
                    ProyectoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AreasID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Importe = table.Column<float>(type: "real", nullable: false),
                    Interes = table.Column<float>(type: "real", nullable: false),
                    MinInversion = table.Column<float>(type: "real", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumInversores = table.Column<int>(type: "int", nullable: false),
                    Plazo = table.Column<int>(type: "int", nullable: false),
                    Progreso = table.Column<int>(type: "int", nullable: false),
                    RatingID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyecto", x => x.ProyectoId);
                    table.ForeignKey(
                        name: "FK_Proyecto_TiposInversiones_AreasID",
                        column: x => x.AreasID,
                        principalTable: "TiposInversiones",
                        principalColumn: "TiposInversionesID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proyecto_Rating_RatingID",
                        column: x => x.RatingID,
                        principalTable: "Rating",
                        principalColumn: "RatingID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inversion",
                columns: table => new
                {
                    InversionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cuota = table.Column<float>(type: "real", nullable: false),
                    Intereses = table.Column<float>(type: "real", nullable: false),
                    InversorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProyectoId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inversion", x => x.InversionId);
                    table.ForeignKey(
                        name: "FK_Inversion_AspNetUsers_InversorId",
                        column: x => x.InversorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inversion_Proyecto_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "ProyectoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProyectoAreas",
                columns: table => new
                {
                    ProyectoAreasId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AreasId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProyectoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectoAreas", x => x.ProyectoAreasId);
                    table.ForeignKey(
                        name: "FK_ProyectoAreas_Areas_AreasId",
                        column: x => x.AreasId,
                        principalTable: "Areas",
                        principalColumn: "AreasID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyectoAreas_Proyecto_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "ProyectoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProyectoTiposInversiones",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProyectoId = table.Column<int>(type: "int", nullable: false),
                    TiposInversionesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectoTiposInversiones", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProyectoTiposInversiones_Proyecto_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "ProyectoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyectoTiposInversiones_TiposInversiones_TiposInversionesId",
                        column: x => x.TiposInversionesId,
                        principalTable: "TiposInversiones",
                        principalColumn: "TiposInversionesID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InversionRecuperada",
                columns: table => new
                {
                    InversionRecuperadaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CantidadRecuperada = table.Column<float>(type: "real", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRecuperacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InversionId = table.Column<int>(type: "int", nullable: false),
                    MonederoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InversionRecuperada", x => x.InversionRecuperadaId);
                    table.ForeignKey(
                        name: "FK_InversionRecuperada_Inversion_InversionId",
                        column: x => x.InversionId,
                        principalTable: "Inversion",
                        principalColumn: "InversionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InversionRecuperada_Monedero_MonederoId",
                        column: x => x.MonederoId,
                        principalTable: "Monedero",
                        principalColumn: "MonederoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Inversion_InversorId",
                table: "Inversion",
                column: "InversorId");

            migrationBuilder.CreateIndex(
                name: "IX_Inversion_ProyectoId",
                table: "Inversion",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_InversionRecuperada_InversionId",
                table: "InversionRecuperada",
                column: "InversionId");

            migrationBuilder.CreateIndex(
                name: "IX_InversionRecuperada_MonederoId",
                table: "InversionRecuperada",
                column: "MonederoId");

            migrationBuilder.CreateIndex(
                name: "IX_Monedero_InversorId",
                table: "Monedero",
                column: "InversorId");

            migrationBuilder.CreateIndex(
                name: "IX_Preferencias_AreasID",
                table: "Preferencias",
                column: "AreasID");

            migrationBuilder.CreateIndex(
                name: "IX_Preferencias_InversorId",
                table: "Preferencias",
                column: "InversorId");

            migrationBuilder.CreateIndex(
                name: "IX_Preferencias_RatingID",
                table: "Preferencias",
                column: "RatingID");

            migrationBuilder.CreateIndex(
                name: "IX_Preferencias_TiposInversionesID",
                table: "Preferencias",
                column: "TiposInversionesID");

            migrationBuilder.CreateIndex(
                name: "IX_Proyecto_AreasID",
                table: "Proyecto",
                column: "AreasID");

            migrationBuilder.CreateIndex(
                name: "IX_Proyecto_RatingID",
                table: "Proyecto",
                column: "RatingID");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoAreas_AreasId",
                table: "ProyectoAreas",
                column: "AreasId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoAreas_ProyectoId",
                table: "ProyectoAreas",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoTiposInversiones_ProyectoId",
                table: "ProyectoTiposInversiones",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoTiposInversiones_TiposInversionesId",
                table: "ProyectoTiposInversiones",
                column: "TiposInversionesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "InversionRecuperada");

            migrationBuilder.DropTable(
                name: "Preferencias");

            migrationBuilder.DropTable(
                name: "ProyectoAreas");

            migrationBuilder.DropTable(
                name: "ProyectoTiposInversiones");

            migrationBuilder.DropTable(
                name: "Inversion");

            migrationBuilder.DropTable(
                name: "Monedero");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Proyecto");

            migrationBuilder.DropTable(
                name: "TiposInversiones");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Apellido1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Apellido2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CodPost",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Domicilio",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Municipio",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nacionalidad",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nif",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PaisResidencia",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Provincia",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
