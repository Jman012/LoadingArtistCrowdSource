using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoadingArtistCrowdSource.Server.Data.Migrations
{
    public partial class Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Comic",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "432ea055-ea01-443d-a6f7-e97d2c18d275",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEIrCen18mpal4mSrTc485Sb4u2WjU44TFsuqN7p4jEiwynmC/wO3w2GTTF6S9ENdQw==");

            migrationBuilder.UpdateData(
                table: "Comic",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "ImportedDate" },
                values: new object[] { "born", new DateTimeOffset(new DateTime(2021, 1, 17, 14, 51, 26, 185, DateTimeKind.Unspecified).AddTicks(7517), new TimeSpan(0, -8, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldDefinition",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DisplayOrder", "IsActive", "IsDeleted", "LastUpdatedBy", "LastUpdatedDate", "LongDescription", "Name", "ShortDescription", "Type" },
                values: new object[,]
                {
                    { new Guid("93d24f79-0d09-4a7b-9de7-05d311974e39"), "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 17, 14, 51, 26, 187, DateTimeKind.Unspecified).AddTicks(2855), new TimeSpan(0, -8, 0, 0, 0)), 1, true, false, null, null, "blah blah", "Panels", "The number of panels in the comic", 1 },
                    { new Guid("1d4a64a1-0aa2-44b9-937f-e7398ce88664"), "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 17, 14, 51, 26, 187, DateTimeKind.Unspecified).AddTicks(4040), new TimeSpan(0, -8, 0, 0, 0)), 2, true, false, null, null, "blah blah blah blah", "Characters", "Which characters are present in the comic", 2 }
                });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldVerifiedEntry",
                columns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "FirstCreatedBy", "VerificationDate" },
                values: new object[] { 1, new Guid("93d24f79-0d09-4a7b-9de7-05d311974e39"), "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 17, 14, 51, 26, 187, DateTimeKind.Unspecified).AddTicks(5239), new TimeSpan(0, -8, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldVerifiedEntry",
                columns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "FirstCreatedBy", "VerificationDate" },
                values: new object[] { 1, new Guid("1d4a64a1-0aa2-44b9-937f-e7398ce88664"), "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 17, 14, 51, 26, 187, DateTimeKind.Unspecified).AddTicks(5513), new TimeSpan(0, -8, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                columns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id", "Value" },
                values: new object[] { 1, new Guid("93d24f79-0d09-4a7b-9de7-05d311974e39"), 0, "3" });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                columns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id", "Value" },
                values: new object[] { 1, new Guid("1d4a64a1-0aa2-44b9-937f-e7398ce88664"), 0, "Blue" });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                columns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id", "Value" },
                values: new object[] { 1, new Guid("1d4a64a1-0aa2-44b9-937f-e7398ce88664"), 1, "Hat Guy" });

            migrationBuilder.CreateIndex(
                name: "IX_Comic_Code",
                table: "Comic",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Comic_Code",
                table: "Comic");

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, new Guid("1d4a64a1-0aa2-44b9-937f-e7398ce88664"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, new Guid("1d4a64a1-0aa2-44b9-937f-e7398ce88664"), 1 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, new Guid("93d24f79-0d09-4a7b-9de7-05d311974e39"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntry",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, new Guid("1d4a64a1-0aa2-44b9-937f-e7398ce88664") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntry",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, new Guid("93d24f79-0d09-4a7b-9de7-05d311974e39") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldDefinition",
                keyColumn: "Id",
                keyValue: new Guid("1d4a64a1-0aa2-44b9-937f-e7398ce88664"));

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldDefinition",
                keyColumn: "Id",
                keyValue: new Guid("93d24f79-0d09-4a7b-9de7-05d311974e39"));

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Comic");

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
                column: "ImportedDate",
                value: new DateTimeOffset(new DateTime(2021, 1, 14, 13, 48, 33, 450, DateTimeKind.Unspecified).AddTicks(5575), new TimeSpan(0, -8, 0, 0, 0)));
        }
    }
}
