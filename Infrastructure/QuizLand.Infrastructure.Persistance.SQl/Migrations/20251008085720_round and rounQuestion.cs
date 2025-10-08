using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizLand.Infrastructure.Persistance.SQl.Migrations
{
    /// <inheritdoc />
    public partial class roundandrounQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoundQuestionAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RandQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GamerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer = table.Column<int>(type: "int", nullable: false),
                    RoundQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsTrue = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundQuestionAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoundQuestionAnswer_Gamers_GamerId",
                        column: x => x.GamerId,
                        principalTable: "Gamers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoundQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionNumber = table.Column<int>(type: "int", nullable: false),
                    RoundId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoundQuestions_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SelectingGamerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: true),
                    RoundNumber = table.Column<int>(type: "int", nullable: false),
                    RoundStatus = table.Column<int>(type: "int", nullable: false),
                    FirstRandQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SecondRandQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ThirdRandQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rounds_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rounds_Gamers_SelectingGamerId",
                        column: x => x.SelectingGamerId,
                        principalTable: "Gamers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rounds_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rounds_RoundQuestions_FirstRandQuestionId",
                        column: x => x.FirstRandQuestionId,
                        principalTable: "RoundQuestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rounds_RoundQuestions_SecondRandQuestionId",
                        column: x => x.SecondRandQuestionId,
                        principalTable: "RoundQuestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rounds_RoundQuestions_ThirdRandQuestionId",
                        column: x => x.ThirdRandQuestionId,
                        principalTable: "RoundQuestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoundQuestionAnswer_GamerId",
                table: "RoundQuestionAnswer",
                column: "GamerId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundQuestionAnswer_RoundQuestionId",
                table: "RoundQuestionAnswer",
                column: "RoundQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundQuestions_QuestionId",
                table: "RoundQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundQuestions_RoundId",
                table: "RoundQuestions",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_CourseId",
                table: "Rounds",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_FirstRandQuestionId",
                table: "Rounds",
                column: "FirstRandQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_GameId",
                table: "Rounds",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_SecondRandQuestionId",
                table: "Rounds",
                column: "SecondRandQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_SelectingGamerId",
                table: "Rounds",
                column: "SelectingGamerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_ThirdRandQuestionId",
                table: "Rounds",
                column: "ThirdRandQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoundQuestionAnswer_RoundQuestions_RoundQuestionId",
                table: "RoundQuestionAnswer",
                column: "RoundQuestionId",
                principalTable: "RoundQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundQuestions_Rounds_RoundId",
                table: "RoundQuestions",
                column: "RoundId",
                principalTable: "Rounds",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_RoundQuestions_FirstRandQuestionId",
                table: "Rounds");

            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_RoundQuestions_SecondRandQuestionId",
                table: "Rounds");

            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_RoundQuestions_ThirdRandQuestionId",
                table: "Rounds");

            migrationBuilder.DropTable(
                name: "RoundQuestionAnswer");

            migrationBuilder.DropTable(
                name: "RoundQuestions");

            migrationBuilder.DropTable(
                name: "Rounds");
        }
    }
}
