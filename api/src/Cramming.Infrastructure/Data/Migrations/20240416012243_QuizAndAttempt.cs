using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cramming.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class QuizAndAttempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizAttempt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuizTitle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsPending = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAttempt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuizId = table.Column<Guid>(type: "uuid", nullable: false),
                    Statement = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizQuestion_Quiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizAttemptQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuizAttemptId = table.Column<Guid>(type: "uuid", nullable: false),
                    Statement = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsPending = table.Column<bool>(type: "boolean", nullable: false),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAttemptQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizAttemptQuestion_QuizAttempt_QuizAttemptId",
                        column: x => x.QuizAttemptId,
                        principalTable: "QuizAttempt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestionOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestionOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizQuestionOption_QuizQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuizQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizAttemptQuestionOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    IsSelected = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAttemptQuestionOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizAttemptQuestionOption_QuizAttemptQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuizAttemptQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttemptQuestion_QuizAttemptId",
                table: "QuizAttemptQuestion",
                column: "QuizAttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttemptQuestionOption_QuestionId",
                table: "QuizAttemptQuestionOption",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestion_QuizId",
                table: "QuizQuestion",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestionOption_QuestionId",
                table: "QuizQuestionOption",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizAttemptQuestionOption");

            migrationBuilder.DropTable(
                name: "QuizQuestionOption");

            migrationBuilder.DropTable(
                name: "QuizAttemptQuestion");

            migrationBuilder.DropTable(
                name: "QuizQuestion");

            migrationBuilder.DropTable(
                name: "QuizAttempt");

            migrationBuilder.DropTable(
                name: "Quiz");
        }
    }
}
