using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedTeamsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Clients_ClientId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_ManifestLine_ManifestLineId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "ManifestLine");

            migrationBuilder.DropTable(
                name: "Manifests");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ClientId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ManifestLineId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ManifestLineId",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "TallySheets",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    SupervisorId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_AspNetUsers_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    TeamId = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamMembers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamMembers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TallySheets_TeamId",
                table: "TallySheets",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_TeamId",
                table: "TeamMembers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_UserId",
                table: "TeamMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_SupervisorId",
                table: "Teams",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TallySheets_Teams_TeamId",
                table: "TallySheets",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TallySheets_Teams_TeamId",
                table: "TallySheets");

            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_TallySheets_TeamId",
                table: "TallySheets");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "TallySheets");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Cars",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManifestLineId",
                table: "Cars",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Manifests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShipId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manifests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manifests_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManifestLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    ManifestId = table.Column<int>(type: "integer", nullable: false),
                    DeclaredQuantity = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManifestLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManifestLine_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManifestLine_Manifests_ManifestId",
                        column: x => x.ManifestId,
                        principalTable: "Manifests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ClientId",
                table: "Cars",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ManifestLineId",
                table: "Cars",
                column: "ManifestLineId");

            migrationBuilder.CreateIndex(
                name: "IX_ManifestLine_ClientId",
                table: "ManifestLine",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ManifestLine_ManifestId",
                table: "ManifestLine",
                column: "ManifestId");

            migrationBuilder.CreateIndex(
                name: "IX_Manifests_ShipId",
                table: "Manifests",
                column: "ShipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Clients_ClientId",
                table: "Cars",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_ManifestLine_ManifestLineId",
                table: "Cars",
                column: "ManifestLineId",
                principalTable: "ManifestLine",
                principalColumn: "Id");
        }
    }
}
