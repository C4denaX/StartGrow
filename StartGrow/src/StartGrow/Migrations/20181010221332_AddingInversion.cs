using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StartGrow.Migrations
{
    public partial class AddingInversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoInversionesId",
                table: "Inversion",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Inversion_TipoInversionesId",
                table: "Inversion",
                column: "TipoInversionesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inversion_TiposInversiones_TipoInversionesId",
                table: "Inversion",
                column: "TipoInversionesId",
                principalTable: "TiposInversiones",
                principalColumn: "TiposInversionesID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inversion_TiposInversiones_TipoInversionesId",
                table: "Inversion");

            migrationBuilder.DropIndex(
                name: "IX_Inversion_TipoInversionesId",
                table: "Inversion");

            migrationBuilder.DropColumn(
                name: "TipoInversionesId",
                table: "Inversion");
        }
    }
}
