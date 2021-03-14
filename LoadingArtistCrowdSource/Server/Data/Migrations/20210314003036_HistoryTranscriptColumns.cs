using Microsoft.EntityFrameworkCore.Migrations;

namespace LoadingArtistCrowdSource.Server.Data.Migrations
{
    public partial class HistoryTranscriptColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComicTranscript_AspNetUsers_LastEditedByUserId",
                table: "ComicTranscript");

            migrationBuilder.RenameColumn(
                name: "LastEditedByUserId",
                table: "ComicTranscript",
                newName: "LastEditedBy");

            migrationBuilder.RenameIndex(
                name: "IX_ComicTranscript_LastEditedByUserId",
                table: "ComicTranscript",
                newName: "IX_ComicTranscript_LastEditedBy");

            migrationBuilder.AddColumn<string>(
                name: "NewValue",
                table: "CrowdSourcedFieldDefinitionHistoryLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldValue",
                table: "CrowdSourcedFieldDefinitionHistoryLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ComicTranscriptHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_ComicTranscript_AspNetUsers_LastEditedBy",
                table: "ComicTranscript",
                column: "LastEditedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComicTranscript_AspNetUsers_LastEditedBy",
                table: "ComicTranscript");

            migrationBuilder.DropColumn(
                name: "NewValue",
                table: "CrowdSourcedFieldDefinitionHistoryLog");

            migrationBuilder.DropColumn(
                name: "OldValue",
                table: "CrowdSourcedFieldDefinitionHistoryLog");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ComicTranscriptHistory");

            migrationBuilder.RenameColumn(
                name: "LastEditedBy",
                table: "ComicTranscript",
                newName: "LastEditedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ComicTranscript_LastEditedBy",
                table: "ComicTranscript",
                newName: "IX_ComicTranscript_LastEditedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComicTranscript_AspNetUsers_LastEditedByUserId",
                table: "ComicTranscript",
                column: "LastEditedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
