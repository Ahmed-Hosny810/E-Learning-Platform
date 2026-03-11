using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_learningPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatingUserAnswerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedOptionText",
                table: "UserAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_SelectedOptionId",
                table: "UserAnswers",
                column: "SelectedOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_QuestionOptions_SelectedOptionId",
                table: "UserAnswers",
                column: "SelectedOptionId",
                principalTable: "QuestionOptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_QuestionOptions_SelectedOptionId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_SelectedOptionId",
                table: "UserAnswers");

            migrationBuilder.AddColumn<string>(
                name: "SelectedOptionText",
                table: "UserAnswers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
