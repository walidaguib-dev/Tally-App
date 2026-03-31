using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedThePausesSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TallySheetTruckId",
                table: "Pauses",
                newName: "TruckId");

            migrationBuilder.CreateIndex(
                name: "IX_Pauses_TruckId",
                table: "Pauses",
                column: "TruckId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pauses_Trucks_TruckId",
                table: "Pauses",
                column: "TruckId",
                principalTable: "Trucks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pauses_Trucks_TruckId",
                table: "Pauses");

            migrationBuilder.DropIndex(
                name: "IX_Pauses_TruckId",
                table: "Pauses");

            migrationBuilder.RenameColumn(
                name: "TruckId",
                table: "Pauses",
                newName: "TallySheetTruckId");
        }
    }
}
