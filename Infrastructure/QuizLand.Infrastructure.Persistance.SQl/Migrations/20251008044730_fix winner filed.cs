using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizLand.Infrastructure.Persistance.SQl.Migrations
{
    /// <inheritdoc />
    public partial class fixwinnerfiled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_WinnerId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_WinnerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "Games");

            migrationBuilder.CreateIndex(
                name: "IX_Games_WinnerUserId",
                table: "Games",
                column: "WinnerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_WinnerUserId",
                table: "Games",
                column: "WinnerUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_WinnerUserId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_WinnerUserId",
                table: "Games");

            migrationBuilder.AddColumn<Guid>(
                name: "WinnerId",
                table: "Games",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Games_WinnerId",
                table: "Games",
                column: "WinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_WinnerId",
                table: "Games",
                column: "WinnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
