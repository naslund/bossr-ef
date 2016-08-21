using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BossrCoreAPI.Migrations
{
    public partial class WorldProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastDayDeaths",
                table: "Worlds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastDayKills",
                table: "Worlds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastScrapeTime",
                table: "Worlds",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "Monitored",
                table: "Worlds",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Monitored",
                table: "Creatures",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastDayDeaths",
                table: "Worlds");

            migrationBuilder.DropColumn(
                name: "LastDayKills",
                table: "Worlds");

            migrationBuilder.DropColumn(
                name: "LastScrapeTime",
                table: "Worlds");

            migrationBuilder.DropColumn(
                name: "Monitored",
                table: "Worlds");

            migrationBuilder.DropColumn(
                name: "Monitored",
                table: "Creatures");
        }
    }
}
