using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BossrCoreAPI.Migrations
{
    public partial class NavigationAndDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Spawns");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateTimeUtc",
                table: "Spawns",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_Spawns_CreatureId",
                table: "Spawns",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Spawns_WorldId",
                table: "Spawns",
                column: "WorldId");

            migrationBuilder.CreateIndex(
                name: "IX_Creatures_CategoryId",
                table: "Creatures",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Creatures_Categories_CategoryId",
                table: "Creatures",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Spawns_Creatures_CreatureId",
                table: "Spawns",
                column: "CreatureId",
                principalTable: "Creatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Spawns_Worlds_WorldId",
                table: "Spawns",
                column: "WorldId",
                principalTable: "Worlds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creatures_Categories_CategoryId",
                table: "Creatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Spawns_Creatures_CreatureId",
                table: "Spawns");

            migrationBuilder.DropForeignKey(
                name: "FK_Spawns_Worlds_WorldId",
                table: "Spawns");

            migrationBuilder.DropIndex(
                name: "IX_Spawns_CreatureId",
                table: "Spawns");

            migrationBuilder.DropIndex(
                name: "IX_Spawns_WorldId",
                table: "Spawns");

            migrationBuilder.DropIndex(
                name: "IX_Creatures_CategoryId",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "DateTimeUtc",
                table: "Spawns");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateTime",
                table: "Spawns",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
