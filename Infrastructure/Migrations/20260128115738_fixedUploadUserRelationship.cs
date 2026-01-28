using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixedUploadUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_uploads_UserId",
                table: "uploads");

            migrationBuilder.CreateIndex(
                name: "IX_uploads_UserId",
                table: "uploads",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_uploads_UserId",
                table: "uploads");

            migrationBuilder.CreateIndex(
                name: "IX_uploads_UserId",
                table: "uploads",
                column: "UserId",
                unique: true);
        }
    }
}
