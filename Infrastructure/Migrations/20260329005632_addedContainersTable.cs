using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedContainersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SequenceNumber = table.Column<int>(type: "integer", nullable: false),
                    ContainerNumber = table.Column<string>(type: "text", nullable: false),
                    Bill_of_lading = table.Column<string>(type: "text", nullable: true),
                    ContainerSize = table.Column<int>(type: "integer", nullable: false),
                    ContainerType = table.Column<int>(type: "integer", nullable: false),
                    ContainerStatus = table.Column<int>(type: "integer", nullable: false),
                    SealNumber = table.Column<string>(type: "text", nullable: true),
                    userId = table.Column<string>(type: "text", nullable: false),
                    TallySheetId = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Containers_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Containers_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Containers_TallySheets_TallySheetId",
                        column: x => x.TallySheetId,
                        principalTable: "TallySheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Containers_ClientId",
                table: "Containers",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_ContainerNumber",
                table: "Containers",
                column: "ContainerNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Containers_TallySheetId",
                table: "Containers",
                column: "TallySheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_userId",
                table: "Containers",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Containers");
        }
    }
}
