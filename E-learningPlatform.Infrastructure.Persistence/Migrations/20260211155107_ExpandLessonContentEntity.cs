using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_learningPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ExpandLessonContentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "LessonContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileSizeBytes",
                table: "LessonContents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MimeType",
                table: "LessonContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProcessingStatus",
                table: "LessonContents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "LessonContents",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "LessonContents");

            migrationBuilder.DropColumn(
                name: "FileSizeBytes",
                table: "LessonContents");

            migrationBuilder.DropColumn(
                name: "MimeType",
                table: "LessonContents");

            migrationBuilder.DropColumn(
                name: "ProcessingStatus",
                table: "LessonContents");

            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "LessonContents");
        }
    }
}
