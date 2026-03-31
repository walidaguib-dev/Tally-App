using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addManifestFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Cars",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ManifestLineId",
                table: "Cars",
                type: "integer",
                nullable: true
            );

            migrationBuilder.CreateTable(
                name: "Manifests",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    ShipId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manifests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manifests_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ManifestLine",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    DeclaredQuantity = table.Column<decimal>(type: "numeric", nullable: false),
                    ManifestId = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManifestLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManifestLine_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_ManifestLine_Manifests_ManifestId",
                        column: x => x.ManifestId,
                        principalTable: "Manifests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ClientId",
                table: "Cars",
                column: "ClientId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ManifestLineId",
                table: "Cars",
                column: "ManifestLineId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ManifestLine_ClientId",
                table: "ManifestLine",
                column: "ClientId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ManifestLine_ManifestId",
                table: "ManifestLine",
                column: "ManifestId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Manifests_ShipId",
                table: "Manifests",
                column: "ShipId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Clients_ClientId",
                table: "Cars",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_ManifestLine_ManifestLineId",
                table: "Cars",
                column: "ManifestLineId",
                principalTable: "ManifestLine",
                principalColumn: "Id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Cars_Clients_ClientId", table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_ManifestLine_ManifestLineId",
                table: "Cars"
            );

            migrationBuilder.DropTable(name: "ManifestLine");

            migrationBuilder.DropTable(name: "Manifests");

            migrationBuilder.DropIndex(name: "IX_Cars_ClientId", table: "Cars");

            migrationBuilder.DropIndex(name: "IX_Cars_ManifestLineId", table: "Cars");

            migrationBuilder.DropColumn(name: "ClientId", table: "Cars");

            migrationBuilder.DropColumn(name: "ManifestLineId", table: "Cars");
        }
    }
}
