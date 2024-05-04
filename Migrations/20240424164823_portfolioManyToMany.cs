using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class portfolioManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0dd6f8bd-7c3d-433b-826b-d393f3c6a0db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9c5ecf3-460b-4c5f-acf4-2d2b57d5aebb");

            migrationBuilder.CreateTable(
                name: "PortFolios",
                columns: table => new
                {
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortFolios", x => new { x.AppUserId, x.StockId });
                    table.ForeignKey(
                        name: "FK_PortFolios_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortFolios_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7f52cec6-0fb2-48ce-b0c9-3e636877c7a3", null, "admin", "ADMIN" },
                    { "bafeb201-7f69-4cc2-8c5b-bbc3184e6328", null, "user", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortFolios_StockId",
                table: "PortFolios",
                column: "StockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortFolios");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f52cec6-0fb2-48ce-b0c9-3e636877c7a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bafeb201-7f69-4cc2-8c5b-bbc3184e6328");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0dd6f8bd-7c3d-433b-826b-d393f3c6a0db", null, "user", "USER" },
                    { "d9c5ecf3-460b-4c5f-acf4-2d2b57d5aebb", null, "admin", "ADMIN" }
                });
        }
    }
}
