using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoadingArtistCrowdSource.Server.Data.Migrations
{
    public partial class Tags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComicTag",
                columns: table => new
                {
                    ComicId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicTag", x => new { x.ComicId, x.Value });
                    table.ForeignKey(
                        name: "FK_ComicTag_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComicTag_Comic_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComicTag_CreatedBy",
                table: "ComicTag",
                column: "CreatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComicTag");
        }
    }
}
