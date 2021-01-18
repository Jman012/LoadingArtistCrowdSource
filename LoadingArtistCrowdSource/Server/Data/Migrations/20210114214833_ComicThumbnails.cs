using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoadingArtistCrowdSource.Server.Data.Migrations
{
    public partial class ComicThumbnails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageThumbnailUrlSrc",
                table: "Comic",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageWideThumbnailUrlSrc",
                table: "Comic",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "432ea055-ea01-443d-a6f7-e97d2c18d275",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEOB6XB5J+om08xKpZ0rghzJFwVhTWMCxHQEPssa2oxwWw4XtGVo8ydYrT9KeBQTVeQ==");

            migrationBuilder.UpdateData(
                table: "Comic",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageThumbnailUrlSrc", "ImportedDate" },
                values: new object[] { "https://loadingartist.com/comic-thumbs/born.png", new DateTimeOffset(new DateTime(2021, 1, 14, 13, 48, 33, 450, DateTimeKind.Unspecified).AddTicks(5575), new TimeSpan(0, -8, 0, 0, 0)) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageThumbnailUrlSrc",
                table: "Comic");

            migrationBuilder.DropColumn(
                name: "ImageWideThumbnailUrlSrc",
                table: "Comic");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "432ea055-ea01-443d-a6f7-e97d2c18d275",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAECj1ck3bM0AHKaarzX5DSSLNKUulcJBNwbMtMUT725YzRM3V789Tpk6VA9CjzC4bLg==");

            migrationBuilder.UpdateData(
                table: "Comic",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImportedDate",
                value: new DateTimeOffset(new DateTime(2021, 1, 13, 23, 0, 32, 442, DateTimeKind.Unspecified).AddTicks(5945), new TimeSpan(0, -8, 0, 0, 0)));
        }
    }
}
