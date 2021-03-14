using Microsoft.EntityFrameworkCore.Migrations;

namespace LoadingArtistCrowdSource.Server.Data.Migrations
{
    public partial class FixForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComicTranscriptHistory_AspNetUsers_CreatedByUserId",
                table: "ComicTranscriptHistory");

            migrationBuilder.DropIndex(
                name: "IX_ComicTranscriptHistory_CreatedByUserId",
                table: "ComicTranscriptHistory");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ComicTranscriptHistory");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ComicTranscriptHistory",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ComicTranscriptHistory_CreatedBy",
                table: "ComicTranscriptHistory",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_ComicTranscriptHistory_AspNetUsers_CreatedBy",
                table: "ComicTranscriptHistory",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComicTranscriptHistory_AspNetUsers_CreatedBy",
                table: "ComicTranscriptHistory");

            migrationBuilder.DropIndex(
                name: "IX_ComicTranscriptHistory_CreatedBy",
                table: "ComicTranscriptHistory");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ComicTranscriptHistory",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "ComicTranscriptHistory",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ComicTranscriptHistory_CreatedByUserId",
                table: "ComicTranscriptHistory",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComicTranscriptHistory_AspNetUsers_CreatedByUserId",
                table: "ComicTranscriptHistory",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
