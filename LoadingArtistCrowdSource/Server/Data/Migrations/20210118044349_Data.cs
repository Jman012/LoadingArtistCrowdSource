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
                value: "AQAAAAEAACcQAAAAEHV4EPqzBakuijFXJh6/a24sQyY60MZJIG++OHYqToQho5boCIcZKuBsbSjTwrODJw==");

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
                values: new object[] { "born", new DateTimeOffset(new DateTime(2021, 1, 17, 20, 43, 48, 605, DateTimeKind.Unspecified).AddTicks(4713), new TimeSpan(0, -8, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldDefinition",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DisplayOrder", "IsActive", "IsDeleted", "LastUpdatedBy", "LastUpdatedDate", "LongDescription", "Name", "ShortDescription", "Type" },
                values: new object[,]
                {
                    { new Guid("1e2a298d-82d7-45c2-b8ce-86d444121cbd"), "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 17, 20, 43, 48, 606, DateTimeKind.Unspecified).AddTicks(9572), new TimeSpan(0, -8, 0, 0, 0)), 1, true, false, null, null, "blah blah", "Panels", "The number of panels in the comic", 1 },
                    { new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 17, 20, 43, 48, 607, DateTimeKind.Unspecified).AddTicks(700), new TimeSpan(0, -8, 0, 0, 0)), 2, true, false, null, null, "blah blah blah blah", "Characters", "Which characters are present in the comic", 2 }
                });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldUserEntry",
                columns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "CreatedDate", "LastUpdatedDate" },
                values: new object[,]
                {
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("1e2a298d-82d7-45c2-b8ce-86d444121cbd"), new DateTimeOffset(new DateTime(2021, 1, 17, 20, 43, 48, 607, DateTimeKind.Unspecified).AddTicks(1927), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("1e2a298d-82d7-45c2-b8ce-86d444121cbd"), new DateTimeOffset(new DateTime(2021, 1, 17, 20, 43, 48, 607, DateTimeKind.Unspecified).AddTicks(2191), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("1e2a298d-82d7-45c2-b8ce-86d444121cbd"), new DateTimeOffset(new DateTime(2021, 1, 17, 20, 43, 48, 607, DateTimeKind.Unspecified).AddTicks(2198), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), new DateTimeOffset(new DateTime(2021, 1, 17, 20, 43, 48, 607, DateTimeKind.Unspecified).AddTicks(2201), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), new DateTimeOffset(new DateTime(2021, 1, 17, 20, 43, 48, 607, DateTimeKind.Unspecified).AddTicks(2204), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), new DateTimeOffset(new DateTime(2021, 1, 17, 20, 43, 48, 607, DateTimeKind.Unspecified).AddTicks(2207), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldVerifiedEntry",
                columns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "FirstCreatedBy", "VerificationDate" },
                values: new object[,]
                {
                    { 1, new Guid("1e2a298d-82d7-45c2-b8ce-86d444121cbd"), "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 17, 20, 43, 48, 607, DateTimeKind.Unspecified).AddTicks(5286), new TimeSpan(0, -8, 0, 0, 0)) },
                    { 1, new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 17, 20, 43, 48, 607, DateTimeKind.Unspecified).AddTicks(5553), new TimeSpan(0, -8, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldUserEntryValue",
                columns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id", "Value" },
                values: new object[,]
                {
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("1e2a298d-82d7-45c2-b8ce-86d444121cbd"), 0, "3" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("1e2a298d-82d7-45c2-b8ce-86d444121cbd"), 0, "3" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("1e2a298d-82d7-45c2-b8ce-86d444121cbd"), 0, "3" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), 0, "Hat Guy" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), 1, "Jes" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), 0, "Hat Guy" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), 1, "Jes" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), 0, "Hat Guy" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), 1, "Jes" }
                });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                columns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id", "Value" },
                values: new object[,]
                {
                    { 1, new Guid("1e2a298d-82d7-45c2-b8ce-86d444121cbd"), 0, "3" },
                    { 1, new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), 0, "Blue" },
                    { 1, new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), 1, "Hat Guy" }
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
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("1e2a298d-82d7-45c2-b8ce-86d444121cbd"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("1e2a298d-82d7-45c2-b8ce-86d444121cbd"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("1e2a298d-82d7-45c2-b8ce-86d444121cbd"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), 1 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), 1 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), 1 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, new Guid("1e2a298d-82d7-45c2-b8ce-86d444121cbd"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, new Guid("b0433325-1314-4486-9e17-4160cc3efb65"), 1 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("1e2a298d-82d7-45c2-b8ce-86d444121cbd") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("1e2a298d-82d7-45c2-b8ce-86d444121cbd") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("1e2a298d-82d7-45c2-b8ce-86d444121cbd") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("b0433325-1314-4486-9e17-4160cc3efb65") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("b0433325-1314-4486-9e17-4160cc3efb65") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("b0433325-1314-4486-9e17-4160cc3efb65") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntry",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, new Guid("1e2a298d-82d7-45c2-b8ce-86d444121cbd") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntry",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, new Guid("b0433325-1314-4486-9e17-4160cc3efb65") });

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
                keyValue: new Guid("1e2a298d-82d7-45c2-b8ce-86d444121cbd"));

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldDefinition",
                keyColumn: "Id",
                keyValue: new Guid("b0433325-1314-4486-9e17-4160cc3efb65"));

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
