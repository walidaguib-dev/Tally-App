using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixPendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                column: "ConcurrencyStamp",
                value: "11111111-aaaa-bbbb-cccc-111111111111");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22222222-2222-2222-2222-222222222222",
                column: "ConcurrencyStamp",
                value: "11111111-aaaa-bbbb-cccc-111111111111");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                column: "ConcurrencyStamp",
                value: "98cf1b2f-bdee-4680-aab5-9009b7bb70c2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22222222-2222-2222-2222-222222222222",
                column: "ConcurrencyStamp",
                value: "141bb769-8b74-4597-9cc7-38e47a9dab07");
        }
    }
}
