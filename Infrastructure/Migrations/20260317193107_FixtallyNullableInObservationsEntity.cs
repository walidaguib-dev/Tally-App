using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixtallyNullableInObservationsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observations_TallySheetTrucks_TallySheetTruckTallySheetId_T~",
                table: "Observations");

            migrationBuilder.DropForeignKey(
                name: "FK_Observations_TallySheets_TallySheetId",
                table: "Observations");

            migrationBuilder.DropIndex(
                name: "IX_Observations_TallySheetTruckTallySheetId_TallySheetTruckTru~",
                table: "Observations");

            migrationBuilder.DropColumn(
                name: "TallySheetMerchandiseId",
                table: "Observations");

            migrationBuilder.DropColumn(
                name: "TallySheetTruckId",
                table: "Observations");

            migrationBuilder.DropColumn(
                name: "TallySheetTruckTallySheetId",
                table: "Observations");

            migrationBuilder.RenameColumn(
                name: "TallySheetTruckTruckId",
                table: "Observations",
                newName: "ClientId");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "Timestamp",
                table: "Observations",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "TallySheetId",
                table: "Observations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Observations_ClientId",
                table: "Observations",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Observations_Clients_ClientId",
                table: "Observations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Observations_TallySheets_TallySheetId",
                table: "Observations",
                column: "TallySheetId",
                principalTable: "TallySheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observations_Clients_ClientId",
                table: "Observations");

            migrationBuilder.DropForeignKey(
                name: "FK_Observations_TallySheets_TallySheetId",
                table: "Observations");

            migrationBuilder.DropIndex(
                name: "IX_Observations_ClientId",
                table: "Observations");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Observations",
                newName: "TallySheetTruckTruckId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Observations",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone");

            migrationBuilder.AlterColumn<int>(
                name: "TallySheetId",
                table: "Observations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "TallySheetMerchandiseId",
                table: "Observations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TallySheetTruckId",
                table: "Observations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TallySheetTruckTallySheetId",
                table: "Observations",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Observations_TallySheetTruckTallySheetId_TallySheetTruckTru~",
                table: "Observations",
                columns: new[] { "TallySheetTruckTallySheetId", "TallySheetTruckTruckId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Observations_TallySheetTrucks_TallySheetTruckTallySheetId_T~",
                table: "Observations",
                columns: new[] { "TallySheetTruckTallySheetId", "TallySheetTruckTruckId" },
                principalTable: "TallySheetTrucks",
                principalColumns: new[] { "TallySheetId", "TruckId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Observations_TallySheets_TallySheetId",
                table: "Observations",
                column: "TallySheetId",
                principalTable: "TallySheets",
                principalColumn: "Id");
        }
    }
}
