using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZombieController.Migrations
{
    public partial class AddCommand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    CommandId = table.Column<Guid>(nullable: false),
                    CommandText = table.Column<string>(nullable: true),
                    ZombieId = table.Column<Guid>(nullable: true),
                    RunAfter = table.Column<DateTime>(nullable: false),
                    RunBefore = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.CommandId);
                    table.ForeignKey(
                        name: "FK_Commands_Zombies_ZombieId",
                        column: x => x.ZombieId,
                        principalTable: "Zombies",
                        principalColumn: "ZombieId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commands_ZombieId",
                table: "Commands",
                column: "ZombieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commands");
        }
    }
}
