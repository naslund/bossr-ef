using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BossrCoreAPI.Migrations
{
    public partial class CategoryColorHex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorRgb",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "ColorRgbHex",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorRgbHex",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "ColorRgb",
                table: "Categories",
                nullable: false,
                defaultValue: 0);
        }
    }
}
