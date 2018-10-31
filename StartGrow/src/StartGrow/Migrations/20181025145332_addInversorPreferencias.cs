using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StartGrow.Migrations
{
    public partial class addInversorPreferencias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Preferencias_AspNetUsers_InversorId1",
                table: "Preferencias");

            migrationBuilder.DropIndex(
                name: "IX_Preferencias_InversorId1",
                table: "Preferencias");

            migrationBuilder.DropColumn(
                name: "InversorId1",
                table: "Preferencias");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InversorId1",
                table: "Preferencias",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Preferencias_InversorId1",
                table: "Preferencias",
                column: "InversorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Preferencias_AspNetUsers_InversorId1",
                table: "Preferencias",
                column: "InversorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
