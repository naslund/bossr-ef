using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BossrCoreAPI.Migrations
{
    public partial class Location : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatureId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PosX = table.Column<int>(nullable: false),
                    PosY = table.Column<int>(nullable: false),
                    PosZ = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Spawns",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Spawns_LocationId",
                table: "Spawns",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_CreatureId",
                table: "Location",
                column: "CreatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spawns_Location_LocationId",
                table: "Spawns",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spawns_Location_LocationId",
                table: "Spawns");

            migrationBuilder.DropIndex(
                name: "IX_Spawns_LocationId",
                table: "Spawns");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Spawns");

            migrationBuilder.DropColumn(
                name: "SpawnIntervalMax",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "SpawnIntervalMin",
                table: "Creatures");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
