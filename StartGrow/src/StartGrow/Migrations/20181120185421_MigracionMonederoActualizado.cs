using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StartGrow.Migrations
{
    public partial class MigracionMonederoActualizado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monedero_AspNetUsers_InversorId1",
                table: "Monedero");

            migrationBuilder.DropIndex(
                name: "IX_Monedero_InversorId1",
                table: "Monedero");

            migrationBuilder.DropColumn(
                name: "InversorId1",
                table: "Monedero");

            migrationBuilder.AddColumn<int>(
                name: "MonederoId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MonederoId",
                table: "AspNetUsers",
                column: "MonederoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Monedero_MonederoId",
                table: "AspNetUsers",
                column: "MonederoId",
                principalTable: "Monedero",
                principalColumn: "MonederoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Monedero_MonederoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MonederoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MonederoId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "InversorId1",
                table: "Monedero",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Monedero_InversorId1",
                table: "Monedero",
                column: "InversorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Monedero_AspNetUsers_InversorId1",
                table: "Monedero",
                column: "InversorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
