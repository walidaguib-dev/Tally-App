using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Merchandises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Bill_Of_Lading = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchandises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ImoNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlateNumber = table.Column<string>(type: "text", nullable: false),
                    Capacity = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ContactInfo = table.Column<string>(type: "text", nullable: false),
                    MerchandiseId = table.Column<int>(type: "integer", nullable: false),
                    MerchandiseId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Merchandises_MerchandiseId",
                        column: x => x.MerchandiseId,
                        principalTable: "Merchandises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clients_Merchandises_MerchandiseId1",
                        column: x => x.MerchandiseId1,
                        principalTable: "Merchandises",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TallySheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TeamsCount = table.Column<int>(type: "integer", nullable: false),
                    Shift = table.Column<string>(type: "text", nullable: false),
                    Zone = table.Column<string>(type: "text", nullable: false),
                    ShipId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TallySheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TallySheets_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TallySheetMerchandises",
                columns: table => new
                {
                    TallySheetId = table.Column<int>(type: "integer", nullable: false),
                    MerchandiseId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "TallySheetTrucks",
                columns: table => new
                {
                    TallySheetId = table.Column<int>(type: "integer", nullable: false),
                    TruckId = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TallySheetTrucks", x => new { x.TallySheetId, x.TruckId });
                    table.ForeignKey(
                        name: "FK_TallySheetTrucks_TallySheets_TallySheetId",
                        column: x => x.TallySheetId,
                        principalTable: "TallySheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TallySheetTrucks_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Observations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TallySheetId = table.Column<int>(type: "integer", nullable: true),
                    TallySheetMerchandiseId = table.Column<int>(type: "integer", nullable: true),
                    TallySheetMerchandiseTallySheetId = table.Column<int>(type: "integer", nullable: true),
                    TallySheetMerchandiseMerchandiseId = table.Column<int>(type: "integer", nullable: true),
                    TallySheetTruckId = table.Column<int>(type: "integer", nullable: true),
                    TallySheetTruckTallySheetId = table.Column<int>(type: "integer", nullable: true),
                    TallySheetTruckTruckId = table.Column<int>(type: "integer", nullable: true),
                    MerchandiseId = table.Column<int>(type: "integer", nullable: true),
                    TruckId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Observations_Merchandises_MerchandiseId",
                        column: x => x.MerchandiseId,
                        principalTable: "Merchandises",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Observations_TallySheetMerchandises_TallySheetMerchandiseTa~",
                        columns: x => new { x.TallySheetMerchandiseTallySheetId, x.TallySheetMerchandiseMerchandiseId },
                        principalTable: "TallySheetMerchandises",
                        principalColumns: new[] { "TallySheetId", "MerchandiseId" });
                    table.ForeignKey(
                        name: "FK_Observations_TallySheetTrucks_TallySheetTruckTallySheetId_T~",
                        columns: x => new { x.TallySheetTruckTallySheetId, x.TallySheetTruckTruckId },
                        principalTable: "TallySheetTrucks",
                        principalColumns: new[] { "TallySheetId", "TruckId" });
                    table.ForeignKey(
                        name: "FK_Observations_TallySheets_TallySheetId",
                        column: x => x.TallySheetId,
                        principalTable: "TallySheets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Observations_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pause",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    TallySheetTruckId = table.Column<int>(type: "integer", nullable: false),
                    TallySheetTruckTallySheetId = table.Column<int>(type: "integer", nullable: false),
                    TallySheetTruckTruckId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pause", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pause_TallySheetTrucks_TallySheetTruckTallySheetId_TallyShe~",
                        columns: x => new { x.TallySheetTruckTallySheetId, x.TallySheetTruckTruckId },
                        principalTable: "TallySheetTrucks",
                        principalColumns: new[] { "TallySheetId", "TruckId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_MerchandiseId",
                table: "Clients",
                column: "MerchandiseId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_MerchandiseId1",
                table: "Clients",
                column: "MerchandiseId1");

            migrationBuilder.CreateIndex(
                name: "IX_Observations_MerchandiseId",
                table: "Observations",
                column: "MerchandiseId");

            migrationBuilder.CreateIndex(
                name: "IX_Observations_TallySheetId",
                table: "Observations",
                column: "TallySheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Observations_TallySheetMerchandiseTallySheetId_TallySheetMe~",
                table: "Observations",
                columns: new[] { "TallySheetMerchandiseTallySheetId", "TallySheetMerchandiseMerchandiseId" });

            migrationBuilder.CreateIndex(
                name: "IX_Observations_TallySheetTruckTallySheetId_TallySheetTruckTru~",
                table: "Observations",
                columns: new[] { "TallySheetTruckTallySheetId", "TallySheetTruckTruckId" });

            migrationBuilder.CreateIndex(
                name: "IX_Observations_TruckId",
                table: "Observations",
                column: "TruckId");

            migrationBuilder.CreateIndex(
                name: "IX_Pause_TallySheetTruckTallySheetId_TallySheetTruckTruckId",
                table: "Pause",
                columns: new[] { "TallySheetTruckTallySheetId", "TallySheetTruckTruckId" });

            migrationBuilder.CreateIndex(
                name: "IX_TallySheetMerchandises_MerchandiseId",
                table: "TallySheetMerchandises",
                column: "MerchandiseId");

            migrationBuilder.CreateIndex(
                name: "IX_TallySheets_ShipId",
                table: "TallySheets",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_TallySheetTrucks_TruckId",
                table: "TallySheetTrucks",
                column: "TruckId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Observations");

            migrationBuilder.DropTable(
                name: "Pause");

            migrationBuilder.DropTable(
                name: "TallySheetMerchandises");

            migrationBuilder.DropTable(
                name: "TallySheetTrucks");

            migrationBuilder.DropTable(
                name: "Merchandises");

            migrationBuilder.DropTable(
                name: "TallySheets");

            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.DropTable(
                name: "Ships");
        }
    }
}
