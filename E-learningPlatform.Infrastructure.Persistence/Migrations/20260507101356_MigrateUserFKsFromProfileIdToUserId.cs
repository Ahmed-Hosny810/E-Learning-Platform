using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_learningPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MigrateUserFKsFromProfileIdToUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseReview_Courses_CourseId",
                table: "CourseReview");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseReview_Enrollments_EnrollmentId",
                table: "CourseReview");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseReview_UserProfiles_UserProfileId",
                table: "CourseReview");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionComments_UserProfiles_UserProfileId",
                table: "DiscussionComments");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionPosts_UserProfiles_UserProfileId",
                table: "DiscussionPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_UserProfiles_UserProfileId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_UserProfiles_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_UserProfiles_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_UserProfiles_UserProfileId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Enrollments_EnrollmentId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizAttempts_Enrollments_EnrollmentId",
                table: "QuizAttempts");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserProfileId_IsRead_CreatedAt",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_UserProfileId_CourseId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_DiscussionPosts_UserProfileId",
                table: "DiscussionPosts");

            migrationBuilder.DropIndex(
                name: "IX_DiscussionComments_UserProfileId",
                table: "DiscussionComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseReview",
                table: "CourseReview");

            migrationBuilder.DropIndex(
                name: "IX_CourseReview_CourseId",
                table: "CourseReview");

            migrationBuilder.DropIndex(
                name: "IX_CourseReview_UserProfileId",
                table: "CourseReview");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "DiscussionPosts");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "DiscussionComments");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "CourseReview");

            migrationBuilder.RenameTable(
                name: "CourseReview",
                newName: "CourseReviews");

            migrationBuilder.RenameIndex(
                name: "IX_CourseReview_EnrollmentId",
                table: "CourseReviews",
                newName: "IX_CourseReviews_EnrollmentId");

            migrationBuilder.AlterColumn<int>(
                name: "TotalPoints",
                table: "QuizAttempts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "QuizAttempts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "QuizAttempts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "QuizAttempts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "FailureReason",
                table: "Payments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaidAt",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Notifications",
                type: "nvarchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Messages",
                type: "nvarchar(150)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverId",
                table: "Messages",
                type: "nvarchar(150)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Enrollments",
                type: "nvarchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DiscussionPosts",
                type: "nvarchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DiscussionComments",
                type: "nvarchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewText",
                table: "CourseReviews",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                table: "CourseReviews",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "InstructorResponse",
                table: "CourseReviews",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CourseReviews",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseReviews",
                table: "CourseReviews",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempts_StudentId_QuizId",
                table: "QuizAttempts",
                columns: new[] { "StudentId", "QuizId" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId_IsRead_CreatedAt",
                table: "Notifications",
                columns: new[] { "UserId", "IsRead", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UserId_CourseId",
                table: "Enrollments",
                columns: new[] { "UserId", "CourseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionPosts_UserId",
                table: "DiscussionPosts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionComments_UserId",
                table: "DiscussionComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseReviews_CourseId_IsApproved",
                table: "CourseReviews",
                columns: new[] { "CourseId", "IsApproved" });

            migrationBuilder.CreateIndex(
                name: "IX_CourseReviews_CreatedAt",
                table: "CourseReviews",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_CourseReviews_Rating",
                table: "CourseReviews",
                column: "Rating");

            migrationBuilder.CreateIndex(
                name: "IX_CourseReviews_UserId",
                table: "CourseReviews",
                column: "UserId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_CourseReviews_Rating",
                table: "CourseReviews",
                sql: "Rating >= 1 AND Rating <= 5");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReviews_Courses_CourseId",
                table: "CourseReviews",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReviews_Enrollments_EnrollmentId",
                table: "CourseReviews",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReviews_UserProfiles_UserId",
                table: "CourseReviews",
                column: "UserId",
                principalTable: "UserProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionComments_UserProfiles_UserId",
                table: "DiscussionComments",
                column: "UserId",
                principalTable: "UserProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionPosts_UserProfiles_UserId",
                table: "DiscussionPosts",
                column: "UserId",
                principalTable: "UserProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_UserProfiles_UserId",
                table: "Enrollments",
                column: "UserId",
                principalTable: "UserProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_UserProfiles_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "UserProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_UserProfiles_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "UserProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_UserProfiles_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "UserProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Enrollments_EnrollmentId",
                table: "Payments",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizAttempts_Enrollments_EnrollmentId",
                table: "QuizAttempts",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseReviews_Courses_CourseId",
                table: "CourseReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseReviews_Enrollments_EnrollmentId",
                table: "CourseReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseReviews_UserProfiles_UserId",
                table: "CourseReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionComments_UserProfiles_UserId",
                table: "DiscussionComments");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionPosts_UserProfiles_UserId",
                table: "DiscussionPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_UserProfiles_UserId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_UserProfiles_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_UserProfiles_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_UserProfiles_UserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Enrollments_EnrollmentId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizAttempts_Enrollments_EnrollmentId",
                table: "QuizAttempts");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserProfiles_UserId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_QuizAttempts_StudentId_QuizId",
                table: "QuizAttempts");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserId_IsRead_CreatedAt",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_UserId_CourseId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_DiscussionPosts_UserId",
                table: "DiscussionPosts");

            migrationBuilder.DropIndex(
                name: "IX_DiscussionComments_UserId",
                table: "DiscussionComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseReviews",
                table: "CourseReviews");

            migrationBuilder.DropIndex(
                name: "IX_CourseReviews_CourseId_IsApproved",
                table: "CourseReviews");

            migrationBuilder.DropIndex(
                name: "IX_CourseReviews_CreatedAt",
                table: "CourseReviews");

            migrationBuilder.DropIndex(
                name: "IX_CourseReviews_Rating",
                table: "CourseReviews");

            migrationBuilder.DropIndex(
                name: "IX_CourseReviews_UserId",
                table: "CourseReviews");

            migrationBuilder.DropCheckConstraint(
                name: "CK_CourseReviews_Rating",
                table: "CourseReviews");

            migrationBuilder.DropColumn(
                name: "FailureReason",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaidAt",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DiscussionPosts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DiscussionComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CourseReviews");

            migrationBuilder.RenameTable(
                name: "CourseReviews",
                newName: "CourseReview");

            migrationBuilder.RenameIndex(
                name: "IX_CourseReviews_EnrollmentId",
                table: "CourseReview",
                newName: "IX_CourseReview_EnrollmentId");

            migrationBuilder.AlterColumn<int>(
                name: "TotalPoints",
                table: "QuizAttempts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "QuizAttempts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "QuizAttempts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "QuizAttempts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "Messages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)");

            migrationBuilder.AlterColumn<int>(
                name: "ReceiverId",
                table: "Messages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)");

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "DiscussionPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "DiscussionComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ReviewText",
                table: "CourseReview",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                table: "CourseReview",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "InstructorResponse",
                table: "CourseReview",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "CourseReview",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseReview",
                table: "CourseReview",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserProfileId_IsRead_CreatedAt",
                table: "Notifications",
                columns: new[] { "UserProfileId", "IsRead", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UserProfileId_CourseId",
                table: "Enrollments",
                columns: new[] { "UserProfileId", "CourseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionPosts_UserProfileId",
                table: "DiscussionPosts",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionComments_UserProfileId",
                table: "DiscussionComments",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseReview_CourseId",
                table: "CourseReview",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseReview_UserProfileId",
                table: "CourseReview",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReview_Courses_CourseId",
                table: "CourseReview",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReview_Enrollments_EnrollmentId",
                table: "CourseReview",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReview_UserProfiles_UserProfileId",
                table: "CourseReview",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionComments_UserProfiles_UserProfileId",
                table: "DiscussionComments",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionPosts_UserProfiles_UserProfileId",
                table: "DiscussionPosts",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_UserProfiles_UserProfileId",
                table: "Enrollments",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_UserProfiles_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_UserProfiles_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_UserProfiles_UserProfileId",
                table: "Notifications",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Enrollments_EnrollmentId",
                table: "Payments",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizAttempts_Enrollments_EnrollmentId",
                table: "QuizAttempts",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
