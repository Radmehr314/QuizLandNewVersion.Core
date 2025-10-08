using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizLand.Infrastructure.Persistance.SQl.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CodeLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Otp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Device = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    SendedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supporters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supporters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarId = table.Column<long>(type: "bigint", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XP = table.Column<long>(type: "bigint", nullable: false),
                    Level = table.Column<long>(type: "bigint", nullable: false),
                    Coin = table.Column<long>(type: "bigint", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveDeviceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenVersion = table.Column<long>(type: "bigint", nullable: false),
                    IsBan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Avatars_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Avatars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstOption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondOption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThirdOption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FourthOption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectOption = table.Column<int>(type: "int", nullable: false),
                    CountClickFirstOption = table.Column<long>(type: "bigint", nullable: true),
                    CountClickSecondOption = table.Column<long>(type: "bigint", nullable: true),
                    CountClickThirdOption = table.Column<long>(type: "bigint", nullable: true),
                    CountClickFourthOption = table.Column<long>(type: "bigint", nullable: true),
                    DescriptiveAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CountOfJoinedClients = table.Column<int>(type: "int", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WinnerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MatchClients = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Users_WinnerUserId",
                        column: x => x.WinnerUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketStatus = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    AssigneeSupporterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastActivityAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Supporters_AssigneeSupporterId",
                        column: x => x.AssigneeSupporterId,
                        principalTable: "Supporters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Gamers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsOwner = table.Column<bool>(type: "bit", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gamers_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Gamers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketMessages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticketid = table.Column<long>(type: "bigint", nullable: false),
                    SenderUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SenderSupporterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Visibility = table.Column<int>(type: "int", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReplyTo = table.Column<long>(type: "bigint", nullable: true),
                    IsSupporter = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketMessages_Supporters_SenderSupporterId",
                        column: x => x.SenderSupporterId,
                        principalTable: "Supporters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketMessages_TicketMessages_ReplyTo",
                        column: x => x.ReplyTo,
                        principalTable: "TicketMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketMessages_Tickets_Ticketid",
                        column: x => x.Ticketid,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketMessages_Users_SenderUserId",
                        column: x => x.SenderUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoundQuestionAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RandQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GamerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer = table.Column<int>(type: "int", nullable: false),
                    RoundQuestionId = table.Column<long>(type: "bigint", nullable: false),
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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    FirstAnswerGamerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: true),
                    RoundNumber = table.Column<int>(type: "int", nullable: false),
                    RoundStatus = table.Column<int>(type: "int", nullable: false),
                    FirstRandQuestionId = table.Column<long>(type: "bigint", nullable: true),
                    SecondRandQuestionId = table.Column<long>(type: "bigint", nullable: true),
                    ThirdRandQuestionId = table.Column<long>(type: "bigint", nullable: true),
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
                        name: "FK_Rounds_Gamers_FirstAnswerGamerId",
                        column: x => x.FirstAnswerGamerId,
                        principalTable: "Gamers",
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
                name: "IX_Gamers_GameId",
                table: "Gamers",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamers_UserId",
                table: "Gamers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_WinnerUserId",
                table: "Games",
                column: "WinnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_CourseId",
                table: "Question",
                column: "CourseId");

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
                name: "IX_Rounds_FirstAnswerGamerId",
                table: "Rounds",
                column: "FirstAnswerGamerId");

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

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessages_ReplyTo",
                table: "TicketMessages",
                column: "ReplyTo");

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessages_SenderSupporterId",
                table: "TicketMessages",
                column: "SenderSupporterId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessages_SenderUserId",
                table: "TicketMessages",
                column: "SenderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessages_Ticketid",
                table: "TicketMessages",
                column: "Ticketid");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssigneeSupporterId",
                table: "Tickets",
                column: "AssigneeSupporterId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AvatarId",
                table: "Users",
                column: "AvatarId");

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
                name: "FK_Gamers_Games_GameId",
                table: "Gamers");

            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Games_GameId",
                table: "Rounds");

            migrationBuilder.DropForeignKey(
                name: "FK_Gamers_Users_UserId",
                table: "Gamers");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Course_CourseId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Course_CourseId",
                table: "Rounds");

            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Gamers_FirstAnswerGamerId",
                table: "Rounds");

            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Gamers_SelectingGamerId",
                table: "Rounds");

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
                name: "CodeLogs");

            migrationBuilder.DropTable(
                name: "RoundQuestionAnswer");

            migrationBuilder.DropTable(
                name: "TicketMessages");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Supporters");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Avatars");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Gamers");

            migrationBuilder.DropTable(
                name: "RoundQuestions");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Rounds");
        }
    }
}
