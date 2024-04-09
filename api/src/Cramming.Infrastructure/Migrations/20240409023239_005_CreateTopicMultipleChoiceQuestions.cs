using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cramming.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _005_CreateTopicMultipleChoiceQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopicMultipleChoiceQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicMultipleChoiceQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicMultipleChoiceQuestions_TopicQuestions_Id",
                        column: x => x.Id,
                        principalTable: "TopicQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicMultipleChoiceQuestionOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    QuestionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Statement = table.Column<string>(type: "TEXT", nullable: false),
                    IsAnswer = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicMultipleChoiceQuestionOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicMultipleChoiceQuestionOptions_TopicMultipleChoiceQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "TopicMultipleChoiceQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicMultipleChoiceQuestionOptions_QuestionId",
                table: "TopicMultipleChoiceQuestionOptions",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopicMultipleChoiceQuestionOptions");

            migrationBuilder.DropTable(
                name: "TopicMultipleChoiceQuestions");
        }
    }
}
