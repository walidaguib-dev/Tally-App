using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredTheTallySheetMerchandiseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "TallySheetMerchandises");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "TallySheetMerchandises",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "TallySheetMerchandises");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TallySheetMerchandises",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
