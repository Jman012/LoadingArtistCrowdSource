using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoadingArtistCrowdSource.Server.Data.Migrations
{
    public partial class Options : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "CrowdSourcedFieldDefinition",
                keyColumn: "Id",
                keyValue: new Guid("28333534-6b32-4ad2-85ca-32a4c6ebbe8b"));

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldDefinition",
                keyColumn: "Id",
                keyValue: new Guid("aad19aa9-95db-4c0a-a582-5d3da03475d5"));

            migrationBuilder.CreateTable(
                name: "CrowdSourcedFieldDefinitionOption",
                columns: table => new
                {
                    CrowdSourcedFieldDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrowdSourcedFieldDefinitionOption", x => new { x.CrowdSourcedFieldDefinitionId, x.Id });
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
                value: "AQAAAAEAACcQAAAAEC0UEVQAwZGMnlyB0JTaYYJNmy7dh9mEb0SxO/zihagNCfU6+HxVJrhPqx8qrpSbpA==");

            migrationBuilder.UpdateData(
                table: "Comic",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImportedDate",
                value: new DateTimeOffset(new DateTime(2021, 1, 24, 16, 57, 17, 880, DateTimeKind.Unspecified).AddTicks(6222), new TimeSpan(0, -8, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldDefinition",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "DisplayOrder", "IsActive", "IsDeleted", "LastUpdatedBy", "LastUpdatedDate", "LongDescription", "Name", "ShortDescription", "Type" },
                values: new object[,]
                {
                    { new Guid("3cf9e9b9-ba40-40dc-b6e7-82238bb40b49"), "", "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 24, 16, 57, 17, 882, DateTimeKind.Unspecified).AddTicks(2300), new TimeSpan(0, -8, 0, 0, 0)), 1, true, false, null, null, "blah blah", "Panels", "The number of panels in the comic", "IntegerNumber" },
                    { new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), "", "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 24, 16, 57, 17, 882, DateTimeKind.Unspecified).AddTicks(3572), new TimeSpan(0, -8, 0, 0, 0)), 2, true, false, null, null, "blah blah blah blah", "Characters", "Which characters are present in the comic", "FreeformTextfield" }
                });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldUserEntry",
                columns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "CreatedDate", "LastUpdatedDate" },
                values: new object[,]
                {
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("3cf9e9b9-ba40-40dc-b6e7-82238bb40b49"), new DateTimeOffset(new DateTime(2021, 1, 24, 16, 57, 17, 882, DateTimeKind.Unspecified).AddTicks(4835), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("3cf9e9b9-ba40-40dc-b6e7-82238bb40b49"), new DateTimeOffset(new DateTime(2021, 1, 24, 16, 57, 17, 882, DateTimeKind.Unspecified).AddTicks(5174), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("3cf9e9b9-ba40-40dc-b6e7-82238bb40b49"), new DateTimeOffset(new DateTime(2021, 1, 24, 16, 57, 17, 882, DateTimeKind.Unspecified).AddTicks(5182), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), new DateTimeOffset(new DateTime(2021, 1, 24, 16, 57, 17, 882, DateTimeKind.Unspecified).AddTicks(5185), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), new DateTimeOffset(new DateTime(2021, 1, 24, 16, 57, 17, 882, DateTimeKind.Unspecified).AddTicks(5188), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), new DateTimeOffset(new DateTime(2021, 1, 24, 16, 57, 17, 882, DateTimeKind.Unspecified).AddTicks(5191), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldVerifiedEntry",
                columns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "FirstCreatedBy", "VerificationDate" },
                values: new object[,]
                {
                    { 1, new Guid("3cf9e9b9-ba40-40dc-b6e7-82238bb40b49"), "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 24, 16, 57, 17, 882, DateTimeKind.Unspecified).AddTicks(8356), new TimeSpan(0, -8, 0, 0, 0)) },
                    { 1, new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 24, 16, 57, 17, 882, DateTimeKind.Unspecified).AddTicks(8630), new TimeSpan(0, -8, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldUserEntryValue",
                columns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id", "Value" },
                values: new object[,]
                {
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("3cf9e9b9-ba40-40dc-b6e7-82238bb40b49"), 0, "3" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("3cf9e9b9-ba40-40dc-b6e7-82238bb40b49"), 0, "3" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("3cf9e9b9-ba40-40dc-b6e7-82238bb40b49"), 0, "3" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), 0, "Hat Guy" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), 1, "Jes" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), 0, "Hat Guy" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), 1, "Jes" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), 0, "Hat Guy" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), 1, "Jes" }
                });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                columns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id", "Value" },
                values: new object[,]
                {
                    { 1, new Guid("3cf9e9b9-ba40-40dc-b6e7-82238bb40b49"), 0, "3" },
                    { 1, new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), 0, "Blue" },
                    { 1, new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), 1, "Hat Guy" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrowdSourcedFieldDefinitionOption");

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("3cf9e9b9-ba40-40dc-b6e7-82238bb40b49"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("3cf9e9b9-ba40-40dc-b6e7-82238bb40b49"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("3cf9e9b9-ba40-40dc-b6e7-82238bb40b49"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), 1 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), 1 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), 1 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, new Guid("3cf9e9b9-ba40-40dc-b6e7-82238bb40b49"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"), 1 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("3cf9e9b9-ba40-40dc-b6e7-82238bb40b49") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("3cf9e9b9-ba40-40dc-b6e7-82238bb40b49") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("3cf9e9b9-ba40-40dc-b6e7-82238bb40b49") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntry",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, new Guid("3cf9e9b9-ba40-40dc-b6e7-82238bb40b49") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntry",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldDefinition",
                keyColumn: "Id",
                keyValue: new Guid("3cf9e9b9-ba40-40dc-b6e7-82238bb40b49"));

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldDefinition",
                keyColumn: "Id",
                keyValue: new Guid("9285e024-af5e-40e7-b1ea-52bf35481bf0"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "432ea055-ea01-443d-a6f7-e97d2c18d275",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEO1qaXdhky35IA9LeVVehFvqg5yP3ri0gHZC8m2tJ5hu2MVu7U/Uiwk60b+IeR07tg==");

            migrationBuilder.UpdateData(
                table: "Comic",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImportedDate",
                value: new DateTimeOffset(new DateTime(2021, 1, 23, 18, 4, 11, 458, DateTimeKind.Unspecified).AddTicks(5040), new TimeSpan(0, -8, 0, 0, 0)));

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
        }
    }
}
