using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cramming.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class StaticQuizzes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StaticQuizzes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticQuizzes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaticQuizQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    QuizId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Statement = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticQuizQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaticQuizQuestion_StaticQuizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "StaticQuizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaticQuizQuestionOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    QuestionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Text = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticQuizQuestionOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaticQuizQuestionOption_StaticQuizQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "StaticQuizQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaticQuizQuestion_QuizId",
                table: "StaticQuizQuestion",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticQuizQuestionOption_QuestionId",
                table: "StaticQuizQuestionOption",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaticQuizQuestionOption");

            migrationBuilder.DropTable(
                name: "StaticQuizQuestion");

            migrationBuilder.DropTable(
                name: "StaticQuizzes");
        }
    }
}
