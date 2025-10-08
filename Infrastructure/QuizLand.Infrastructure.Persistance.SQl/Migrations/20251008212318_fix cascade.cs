using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizLand.Infrastructure.Persistance.SQl.Migrations
{
    /// <inheritdoc />
    public partial class fixcascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoundQuestions_Rounds_RoundId",
                table: "RoundQuestions");

            migrationBuilder.AddForeignKey(
                name: "FK_RoundQuestions_Rounds_RoundId",
                table: "RoundQuestions",
                column: "RoundId",
                principalTable: "Rounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoundQuestions_Rounds_RoundId",
                table: "RoundQuestions");

            migrationBuilder.AddForeignKey(
                name: "FK_RoundQuestions_Rounds_RoundId",
                table: "RoundQuestions",
                column: "RoundId",
                principalTable: "Rounds",
                principalColumn: "Id");
        }
    }
}
