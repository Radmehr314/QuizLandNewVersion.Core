using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizLand.Infrastructure.Persistance.SQl.Migrations
{
    /// <inheritdoc />
    public partial class firstAwnserGamer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FirstAnswerGamerId",
                table: "Rounds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_FirstAnswerGamerId",
                table: "Rounds",
                column: "FirstAnswerGamerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_Gamers_FirstAnswerGamerId",
                table: "Rounds",
                column: "FirstAnswerGamerId",
                principalTable: "Gamers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Gamers_FirstAnswerGamerId",
                table: "Rounds");

            migrationBuilder.DropIndex(
                name: "IX_Rounds_FirstAnswerGamerId",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "FirstAnswerGamerId",
                table: "Rounds");
        }
    }
}
