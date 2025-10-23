using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizLand.Infrastructure.Persistance.SQl.Migrations
{
    /// <inheritdoc />
    public partial class adduserTurnIdToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserTurnId",
                table: "Games",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_UserTurnId",
                table: "Games",
                column: "UserTurnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_UserTurnId",
                table: "Games",
                column: "UserTurnId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_UserTurnId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_UserTurnId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "UserTurnId",
                table: "Games");
        }
    }
}
