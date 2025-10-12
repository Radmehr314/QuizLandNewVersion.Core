using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizLand.Infrastructure.Persistance.SQl.Migrations
{
    /// <inheritdoc />
    public partial class rounQuestionAnswerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoundQuestionAnswer_Gamers_GamerId",
                table: "RoundQuestionAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundQuestionAnswer_RoundQuestions_RoundQuestionId",
                table: "RoundQuestionAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoundQuestionAnswer",
                table: "RoundQuestionAnswer");

            migrationBuilder.DropColumn(
                name: "RandQuestionId",
                table: "RoundQuestionAnswer");

            migrationBuilder.RenameTable(
                name: "RoundQuestionAnswer",
                newName: "RoundQuestionAnswers");

            migrationBuilder.RenameColumn(
                name: "IsTrue",
                table: "RoundQuestionAnswers",
                newName: "IsCorrect");

            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "RoundQuestionAnswers",
                newName: "SelectedOption");

            migrationBuilder.RenameIndex(
                name: "IX_RoundQuestionAnswer_RoundQuestionId",
                table: "RoundQuestionAnswers",
                newName: "IX_RoundQuestionAnswers_RoundQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_RoundQuestionAnswer_GamerId",
                table: "RoundQuestionAnswers",
                newName: "IX_RoundQuestionAnswers_GamerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoundQuestionAnswers",
                table: "RoundQuestionAnswers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoundQuestionAnswers_Gamers_GamerId",
                table: "RoundQuestionAnswers",
                column: "GamerId",
                principalTable: "Gamers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundQuestionAnswers_RoundQuestions_RoundQuestionId",
                table: "RoundQuestionAnswers",
                column: "RoundQuestionId",
                principalTable: "RoundQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoundQuestionAnswers_Gamers_GamerId",
                table: "RoundQuestionAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundQuestionAnswers_RoundQuestions_RoundQuestionId",
                table: "RoundQuestionAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoundQuestionAnswers",
                table: "RoundQuestionAnswers");

            migrationBuilder.RenameTable(
                name: "RoundQuestionAnswers",
                newName: "RoundQuestionAnswer");

            migrationBuilder.RenameColumn(
                name: "SelectedOption",
                table: "RoundQuestionAnswer",
                newName: "Answer");

            migrationBuilder.RenameColumn(
                name: "IsCorrect",
                table: "RoundQuestionAnswer",
                newName: "IsTrue");

            migrationBuilder.RenameIndex(
                name: "IX_RoundQuestionAnswers_RoundQuestionId",
                table: "RoundQuestionAnswer",
                newName: "IX_RoundQuestionAnswer_RoundQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_RoundQuestionAnswers_GamerId",
                table: "RoundQuestionAnswer",
                newName: "IX_RoundQuestionAnswer_GamerId");

            migrationBuilder.AddColumn<Guid>(
                name: "RandQuestionId",
                table: "RoundQuestionAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoundQuestionAnswer",
                table: "RoundQuestionAnswer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoundQuestionAnswer_Gamers_GamerId",
                table: "RoundQuestionAnswer",
                column: "GamerId",
                principalTable: "Gamers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundQuestionAnswer_RoundQuestions_RoundQuestionId",
                table: "RoundQuestionAnswer",
                column: "RoundQuestionId",
                principalTable: "RoundQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
