using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailConfirmation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailConfirmationToken",
                schema: "User",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailConfirmationTokenExpireAt",
                schema: "User",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailConfirmed",
                schema: "User",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailConfirmationToken",
                schema: "User",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailConfirmationTokenExpireAt",
                schema: "User",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsEmailConfirmed",
                schema: "User",
                table: "Users");
        }
    }
}
