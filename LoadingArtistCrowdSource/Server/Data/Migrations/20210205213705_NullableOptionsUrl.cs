using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoadingArtistCrowdSource.Server.Data.Migrations
{
    public partial class NullableOptionsUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "URL",
                table: "CrowdSourcedFieldDefinitionOption",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "432ea055-ea01-443d-a6f7-e97d2c18d275",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEHFW/Fj/4UWqGOG3bEPZJcYIrSzFEZnjxgiB3R9DqRnwuoN1YZGFFM+woXbQmngY/w==");

            migrationBuilder.UpdateData(
                table: "Comic",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImportedDate",
                value: new DateTimeOffset(new DateTime(2021, 2, 5, 13, 37, 4, 635, DateTimeKind.Unspecified).AddTicks(9786), new TimeSpan(0, -8, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "URL",
                table: "CrowdSourcedFieldDefinitionOption",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "432ea055-ea01-443d-a6f7-e97d2c18d275",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEOg2Kct141BNSGd5ZtEK06KNtrKWiaaBKZI792TA77AjUPf3+cNbTva0sHkL236YAQ==");

            migrationBuilder.UpdateData(
                table: "Comic",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImportedDate",
                value: new DateTimeOffset(new DateTime(2021, 2, 1, 20, 58, 32, 618, DateTimeKind.Unspecified).AddTicks(8660), new TimeSpan(0, -8, 0, 0, 0)));
        }
    }
}
