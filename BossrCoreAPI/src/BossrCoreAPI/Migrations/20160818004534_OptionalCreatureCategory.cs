using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BossrCoreAPI.Migrations
{
    public partial class OptionalCreatureCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creatures_Categories_CategoryId",
                table: "Creatures");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Creatures",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Creatures_Categories_CategoryId",
                table: "Creatures",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creatures_Categories_CategoryId",
                table: "Creatures");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Creatures",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Creatures_Categories_CategoryId",
                table: "Creatures",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
