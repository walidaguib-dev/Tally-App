using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTheEntireSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observations_TallySheetMerchandises_TallySheetMerchandiseTa~",
                table: "Observations");

            migrationBuilder.DropTable(
                name: "TallySheetMerchandises");

            migrationBuilder.RenameColumn(
                name: "TallySheetMerchandiseTallySheetId",
                table: "Observations",
                newName: "TallySheetClientTallySheetId");

            migrationBuilder.RenameColumn(
                name: "TallySheetMerchandiseMerchandiseId",
                table: "Observations",
                newName: "TallySheetClientClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Observations_TallySheetMerchandiseTallySheetId_TallySheetMe~",
                table: "Observations",
                newName: "IX_Observations_TallySheetClientTallySheetId_TallySheetClientC~");

            migrationBuilder.CreateTable(
                name: "TallySheetClients",
                columns: table => new
                {
                    TallySheetId = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TallySheetClients", x => new { x.TallySheetId, x.ClientId });
                    table.ForeignKey(
                        name: "FK_TallySheetClients_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TallySheetClients_TallySheets_TallySheetId",
                        column: x => x.TallySheetId,
                        principalTable: "TallySheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TallySheetClients_ClientId",
                table: "TallySheetClients",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Observations_TallySheetClients_TallySheetClientTallySheetId~",
                table: "Observations",
                columns: new[] { "TallySheetClientTallySheetId", "TallySheetClientClientId" },
                principalTable: "TallySheetClients",
                principalColumns: new[] { "TallySheetId", "ClientId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observations_TallySheetClients_TallySheetClientTallySheetId~",
                table: "Observations");

            migrationBuilder.DropTable(
                name: "TallySheetClients");

            migrationBuilder.RenameColumn(
                name: "TallySheetClientTallySheetId",
                table: "Observations",
                newName: "TallySheetMerchandiseTallySheetId");

            migrationBuilder.RenameColumn(
                name: "TallySheetClientClientId",
                table: "Observations",
                newName: "TallySheetMerchandiseMerchandiseId");

            migrationBuilder.RenameIndex(
                name: "IX_Observations_TallySheetClientTallySheetId_TallySheetClientC~",
                table: "Observations",
                newName: "IX_Observations_TallySheetMerchandiseTallySheetId_TallySheetMe~");

            migrationBuilder.CreateTable(
                name: "TallySheetMerchandises",
                columns: table => new
                {
                    TallySheetId = table.Column<int>(type: "integer", nullable: false),
                    MerchandiseId = table.Column<int>(type: "integer", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TallySheetMerchandises", x => new { x.TallySheetId, x.MerchandiseId });
                    table.ForeignKey(
                        name: "FK_TallySheetMerchandises_Merchandises_MerchandiseId",
                        column: x => x.MerchandiseId,
                        principalTable: "Merchandises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TallySheetMerchandises_TallySheets_TallySheetId",
                        column: x => x.TallySheetId,
                        principalTable: "TallySheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TallySheetMerchandises_MerchandiseId",
                table: "TallySheetMerchandises",
                column: "MerchandiseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Observations_TallySheetMerchandises_TallySheetMerchandiseTa~",
                table: "Observations",
                columns: new[] { "TallySheetMerchandiseTallySheetId", "TallySheetMerchandiseMerchandiseId" },
                principalTable: "TallySheetMerchandises",
                principalColumns: new[] { "TallySheetId", "MerchandiseId" });
        }
    }
}
