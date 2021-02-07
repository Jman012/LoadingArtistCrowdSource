using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoadingArtistCrowdSource.Server.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CompletionType",
                table: "CrowdSourcedFieldDefinitionFeedback",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "CrowdSourcedFieldDefinition",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "CrowdSourcedFieldDefinition",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Comic",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateTable(
                name: "CrowdSourcedFieldDefinitionOption",
                columns: table => new
                {
                    CrowdSourcedFieldDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrowdSourcedFieldDefinitionOption", x => new { x.CrowdSourcedFieldDefinitionId, x.Code });
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldDefinitionOption_CrowdSourcedFieldDefinition_CrowdSourcedFieldDefinitionId",
                        column: x => x.CrowdSourcedFieldDefinitionId,
                        principalTable: "CrowdSourcedFieldDefinition",
                        principalColumn: "Id");
                });

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
                columns: new[] { "Code", "ImageThumbnailUrlSrc", "ImportedDate" },
                values: new object[] { "born", "https://loadingartist.com/comic-thumbs/born.png", new DateTimeOffset(new DateTime(2021, 2, 1, 20, 58, 32, 618, DateTimeKind.Unspecified).AddTicks(8660), new TimeSpan(0, -8, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_CrowdSourcedFieldDefinition_Code",
                table: "CrowdSourcedFieldDefinition",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comic_Code",
                table: "Comic",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrowdSourcedFieldDefinitionOption");

            migrationBuilder.DropIndex(
                name: "IX_CrowdSourcedFieldDefinition_Code",
                table: "CrowdSourcedFieldDefinition");

            migrationBuilder.DropIndex(
                name: "IX_Comic_Code",
                table: "Comic");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "CrowdSourcedFieldDefinition");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Comic");

            migrationBuilder.DropColumn(
                name: "ImageThumbnailUrlSrc",
                table: "Comic");

            migrationBuilder.DropColumn(
                name: "ImageWideThumbnailUrlSrc",
                table: "Comic");

            migrationBuilder.AlterColumn<int>(
                name: "CompletionType",
                table: "CrowdSourcedFieldDefinitionFeedback",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "CrowdSourcedFieldDefinition",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
