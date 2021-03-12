using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoadingArtistCrowdSource.Server.Data.Migrations
{
    public partial class Transcripts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComicTranscript",
                columns: table => new
                {
                    ComicId = table.Column<int>(type: "int", nullable: false),
                    LastEditedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastEditedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TranscriptContent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicTranscript", x => x.ComicId);
                    table.ForeignKey(
                        name: "FK_ComicTranscript_AspNetUsers_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComicTranscript_Comic_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComicTranscriptHistory",
                columns: table => new
                {
                    ComicId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TranscriptContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiffWithPrevious = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicTranscriptHistory", x => new { x.ComicId, x.Id });
                    table.ForeignKey(
                        name: "FK_ComicTranscriptHistory_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComicTranscriptHistory_Comic_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comic",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "432ea055-ea01-443d-a6f7-e97d2c18d275",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEK1gJpnKWF92WxUNfQ0m0rbjpk9K5isdrfTJQzBieoSS5AJP4LQ6wxDHGwor1uT86A==");

            migrationBuilder.CreateIndex(
                name: "IX_ComicTranscript_LastEditedByUserId",
                table: "ComicTranscript",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ComicTranscriptHistory_CreatedByUserId",
                table: "ComicTranscriptHistory",
                column: "CreatedByUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComicTranscript");

            migrationBuilder.DropTable(
                name: "ComicTranscriptHistory");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "432ea055-ea01-443d-a6f7-e97d2c18d275",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEGThm/+f+RsBogU8iIZQ5u47tZMj4opKQz2ntvmnM7WlhbLoX0gIOLVAbs1tRy65vw==");
        }
    }
}
