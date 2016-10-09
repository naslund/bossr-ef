using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BossrCoreAPI.Migrations
{
    public partial class CategoryColorSeparateChannels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorRgbHex",
                table: "Categories");

            migrationBuilder.AddColumn<byte>(
                name: "ColorB",
                table: "Categories",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "ColorG",
                table: "Categories",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "ColorR",
                table: "Categories",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorB",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ColorG",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ColorR",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "ColorRgbHex",
                table: "Categories",
                nullable: true);
        }
    }
}
