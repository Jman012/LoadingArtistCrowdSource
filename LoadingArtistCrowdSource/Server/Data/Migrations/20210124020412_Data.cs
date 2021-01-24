using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoadingArtistCrowdSource.Server.Data.Migrations
{
    public partial class Data : Migration
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
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                value: "AQAAAAEAACcQAAAAEO1qaXdhky35IA9LeVVehFvqg5yP3ri0gHZC8m2tJ5hu2MVu7U/Uiwk60b+IeR07tg==");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "432ea055-ea01-443d-a6f7-e97d2c18d276", 0, "3acb17f1-65fe-4eac-bc2b-26403b23b998", "jman012guy+1@gmail.com", true, true, null, "JMAN012GUY+1@GMAIL.COM", "JAMAMP1", "", "", false, "", false, "jamamp1" },
                    { "432ea055-ea01-443d-a6f7-e97d2c18d277", 0, "3acb17f1-65fe-4eac-bc2b-26403b23b997", "jman012guy+2@gmail.com", true, true, null, "JMAN012GUY+2@GMAIL.COM", "JAMAMP2", "", "", false, "", false, "jamamp2" }
                });

            migrationBuilder.UpdateData(
                table: "Comic",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "ImportedDate" },
                values: new object[] { "born", new DateTimeOffset(new DateTime(2021, 1, 23, 18, 4, 11, 458, DateTimeKind.Unspecified).AddTicks(5040), new TimeSpan(0, -8, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldDefinition",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "DisplayOrder", "IsActive", "IsDeleted", "LastUpdatedBy", "LastUpdatedDate", "LongDescription", "Name", "ShortDescription", "Type" },
                values: new object[,]
                {
                    { new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5"), "", "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 23, 18, 4, 11, 460, DateTimeKind.Unspecified).AddTicks(366), new TimeSpan(0, -8, 0, 0, 0)), 1, true, false, null, null, "blah blah", "Panels", "The number of panels in the comic", "IntegerNumber" },
                    { new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), "", "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 23, 18, 4, 11, 460, DateTimeKind.Unspecified).AddTicks(1571), new TimeSpan(0, -8, 0, 0, 0)), 2, true, false, null, null, "blah blah blah blah", "Characters", "Which characters are present in the comic", "FreeformTextfield" }
                });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldUserEntry",
                columns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "CreatedDate", "LastUpdatedDate" },
                values: new object[,]
                {
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5"), new DateTimeOffset(new DateTime(2021, 1, 23, 18, 4, 11, 460, DateTimeKind.Unspecified).AddTicks(2810), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5"), new DateTimeOffset(new DateTime(2021, 1, 23, 18, 4, 11, 460, DateTimeKind.Unspecified).AddTicks(3079), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5"), new DateTimeOffset(new DateTime(2021, 1, 23, 18, 4, 11, 460, DateTimeKind.Unspecified).AddTicks(3086), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), new DateTimeOffset(new DateTime(2021, 1, 23, 18, 4, 11, 460, DateTimeKind.Unspecified).AddTicks(3089), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), new DateTimeOffset(new DateTime(2021, 1, 23, 18, 4, 11, 460, DateTimeKind.Unspecified).AddTicks(3092), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), new DateTimeOffset(new DateTime(2021, 1, 23, 18, 4, 11, 460, DateTimeKind.Unspecified).AddTicks(3095), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldVerifiedEntry",
                columns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "FirstCreatedBy", "VerificationDate" },
                values: new object[,]
                {
                    { 1, new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5"), "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 23, 18, 4, 11, 460, DateTimeKind.Unspecified).AddTicks(6073), new TimeSpan(0, -8, 0, 0, 0)) },
                    { 1, new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 23, 18, 4, 11, 460, DateTimeKind.Unspecified).AddTicks(6342), new TimeSpan(0, -8, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldUserEntryValue",
                columns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id", "Value" },
                values: new object[,]
                {
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5"), 0, "3" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5"), 0, "3" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5"), 0, "3" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), 0, "Hat Guy" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), 1, "Jes" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), 0, "Hat Guy" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), 1, "Jes" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), 0, "Hat Guy" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), 1, "Jes" }
                });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                columns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id", "Value" },
                values: new object[,]
                {
                    { 1, new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5"), 0, "3" },
                    { 1, new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), 0, "Blue" },
                    { 1, new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), 1, "Hat Guy" }
                });

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
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), 1 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), 1 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), 1 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"), 1 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntry",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntry",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "432ea055-ea01-443d-a6f7-e97d2c18d276");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "432ea055-ea01-443d-a6f7-e97d2c18d277");

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldDefinition",
                keyColumn: "Id",
                keyValue: new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"));

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldDefinition",
                keyColumn: "Id",
                keyValue: new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5"));

            migrationBuilder.DropColumn(
                name: "Code",
                table: "CrowdSourcedFieldDefinition");

            migrationBuilder.DropColumn(
                name: "Code",
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
