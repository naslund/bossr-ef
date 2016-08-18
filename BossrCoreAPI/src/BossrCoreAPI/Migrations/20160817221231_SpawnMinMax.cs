using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BossrCoreAPI.Migrations
{
    public partial class SpawnMinMax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeUtc",
                table: "Spawns");

            migrationBuilder.DropColumn(
                name: "IsExact",
                table: "Spawns");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "TimeMaxUtc",
                table: "Spawns",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "TimeMinUtc",
                table: "Spawns",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeMaxUtc",
                table: "Spawns");

            migrationBuilder.DropColumn(
                name: "TimeMinUtc",
                table: "Spawns");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateTimeUtc",
                table: "Spawns",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsExact",
                table: "Spawns",
                nullable: false,
                defaultValue: false);
        }
    }
}
