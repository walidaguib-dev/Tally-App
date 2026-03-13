using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNullFromTallySheetPropInPauseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pauses_TallySheets_TallySheetId",
                table: "Pauses");

            migrationBuilder.AlterColumn<int>(
                name: "TallySheetId",
                table: "Pauses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pauses_TallySheets_TallySheetId",
                table: "Pauses",
                column: "TallySheetId",
                principalTable: "TallySheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pauses_TallySheets_TallySheetId",
                table: "Pauses");

            migrationBuilder.AlterColumn<int>(
                name: "TallySheetId",
                table: "Pauses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Pauses_TallySheets_TallySheetId",
                table: "Pauses",
                column: "TallySheetId",
                principalTable: "TallySheets",
                principalColumn: "Id");
        }
    }
}
