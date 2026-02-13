using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedBillOfLadingToClients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bill_Of_Lading",
                table: "Merchandises");

            migrationBuilder.AddColumn<List<string>>(
                name: "Bill_Of_Lading",
                table: "Clients",
                type: "text[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bill_Of_Lading",
                table: "Clients");

            migrationBuilder.AddColumn<List<string>>(
                name: "Bill_Of_Lading",
                table: "Merchandises",
                type: "text[]",
                nullable: false);
        }
    }
}
