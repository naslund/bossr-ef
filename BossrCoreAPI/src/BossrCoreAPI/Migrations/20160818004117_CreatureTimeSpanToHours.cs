using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BossrCoreAPI.Migrations
{
    public partial class CreatureTimeSpanToHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Creatures_CreatureId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Spawns_Location_LocationId",
                table: "Spawns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "SpawnIntervalMax",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "SpawnIntervalMin",
                table: "Creatures");

            migrationBuilder.AddColumn<int>(
                name: "HoursBetweenEachSpawnMax",
                table: "Creatures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HoursBetweenEachSpawnMin",
                table: "Creatures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Location",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Creatures_CreatureId",
                table: "Location",
                column: "CreatureId",
                principalTable: "Creatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Spawns_Locations_LocationId",
                table: "Spawns",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Location_CreatureId",
                table: "Location",
                newName: "IX_Locations_CreatureId");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Creatures_CreatureId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Spawns_Locations_LocationId",
                table: "Spawns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "HoursBetweenEachSpawnMax",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "HoursBetweenEachSpawnMin",
                table: "Creatures");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SpawnIntervalMax",
                table: "Creatures",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SpawnIntervalMin",
                table: "Creatures",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Creatures_CreatureId",
                table: "Locations",
                column: "CreatureId",
                principalTable: "Creatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Spawns_Location_LocationId",
                table: "Spawns",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Locations_CreatureId",
                table: "Locations",
                newName: "IX_Location_CreatureId");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");
        }
    }
}
