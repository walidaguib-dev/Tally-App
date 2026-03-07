using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedTallySheetSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TallySheets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TallySheets_UserId",
                table: "TallySheets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TallySheets_AspNetUsers_UserId",
                table: "TallySheets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TallySheets_AspNetUsers_UserId",
                table: "TallySheets");

            migrationBuilder.DropIndex(
                name: "IX_TallySheets_UserId",
                table: "TallySheets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TallySheets");
        }
    }
}
