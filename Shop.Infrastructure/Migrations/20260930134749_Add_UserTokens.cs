using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_UserTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HashJwtToken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    HashRefreshToken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TokenExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshTokenExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Device = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_HashJwtToken",
                schema: "User",
                table: "UserTokens",
                column: "HashJwtToken");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_HashRefreshToken",
                schema: "User",
                table: "UserTokens",
                column: "HashRefreshToken");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                schema: "User",
                table: "UserTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "User");
        }
    }
}
