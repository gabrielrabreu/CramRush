using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cramming.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class QuizAttempts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizAttempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    QuizTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IsPending = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAttempts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizAttemptQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    QuizAttemptId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Statement = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IsPending = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAttemptQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizAttemptQuestion_QuizAttempts_QuizAttemptId",
                        column: x => x.QuizAttemptId,
                        principalTable: "QuizAttempts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizAttemptQuestionOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    QuestionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Text = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSelected = table.Column<bool>(type: "INTEGER", nullable: false)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizAttemptQuestionOption");

            migrationBuilder.DropTable(
                name: "QuizAttemptQuestion");

            migrationBuilder.DropTable(
                name: "QuizAttempts");
        }
    }
}
