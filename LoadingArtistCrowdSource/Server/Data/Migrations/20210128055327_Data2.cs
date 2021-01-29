using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoadingArtistCrowdSource.Server.Data.Migrations
{
    public partial class Data2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CrowdSourcedFieldDefinitionOption",
                table: "CrowdSourcedFieldDefinitionOption");

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

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CrowdSourcedFieldDefinitionOption");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "CrowdSourcedFieldDefinitionOption",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "CrowdSourcedFieldDefinition",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CrowdSourcedFieldDefinitionOption",
                table: "CrowdSourcedFieldDefinitionOption",
                columns: new[] { "CrowdSourcedFieldDefinitionId", "Code" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "432ea055-ea01-443d-a6f7-e97d2c18d275",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEJmlNmL33sR3szVGNhnfVTRbEJ4FLwaQBp8DXiyzmgtrQRVqApiRQ6z0hZnZULSyaw==");

            migrationBuilder.UpdateData(
                table: "Comic",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImportedDate",
                value: new DateTimeOffset(new DateTime(2021, 1, 27, 21, 53, 27, 295, DateTimeKind.Unspecified).AddTicks(5964), new TimeSpan(0, -8, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldDefinition",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "DisplayOrder", "IsActive", "IsDeleted", "LastUpdatedBy", "LastUpdatedDate", "LongDescription", "Name", "ShortDescription", "Type" },
                values: new object[,]
                {
                    { new Guid("82598782-5832-4097-8b74-9caf3cf86a9d"), "panels", "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 27, 21, 53, 27, 297, DateTimeKind.Unspecified).AddTicks(2259), new TimeSpan(0, -8, 0, 0, 0)), 1, true, false, null, null, "blah blah", "Panels", "The number of panels in the comic", "IntegerNumber" },
                    { new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), "characters", "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 27, 21, 53, 27, 297, DateTimeKind.Unspecified).AddTicks(3513), new TimeSpan(0, -8, 0, 0, 0)), 2, true, false, null, null, "blah blah blah blah", "Characters", "Which characters are present in the comic", "FreeformTextfield" }
                });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldUserEntry",
                columns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "CreatedDate", "LastUpdatedDate" },
                values: new object[,]
                {
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("82598782-5832-4097-8b74-9caf3cf86a9d"), new DateTimeOffset(new DateTime(2021, 1, 27, 21, 53, 27, 297, DateTimeKind.Unspecified).AddTicks(4742), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("82598782-5832-4097-8b74-9caf3cf86a9d"), new DateTimeOffset(new DateTime(2021, 1, 27, 21, 53, 27, 297, DateTimeKind.Unspecified).AddTicks(5008), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("82598782-5832-4097-8b74-9caf3cf86a9d"), new DateTimeOffset(new DateTime(2021, 1, 27, 21, 53, 27, 297, DateTimeKind.Unspecified).AddTicks(5016), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), new DateTimeOffset(new DateTime(2021, 1, 27, 21, 53, 27, 297, DateTimeKind.Unspecified).AddTicks(5019), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), new DateTimeOffset(new DateTime(2021, 1, 27, 21, 53, 27, 297, DateTimeKind.Unspecified).AddTicks(5022), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), new DateTimeOffset(new DateTime(2021, 1, 27, 21, 53, 27, 297, DateTimeKind.Unspecified).AddTicks(5025), new TimeSpan(0, -8, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldVerifiedEntry",
                columns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "FirstCreatedBy", "VerificationDate" },
                values: new object[,]
                {
                    { 1, new Guid("82598782-5832-4097-8b74-9caf3cf86a9d"), "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 27, 21, 53, 27, 297, DateTimeKind.Unspecified).AddTicks(7971), new TimeSpan(0, -8, 0, 0, 0)) },
                    { 1, new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 27, 21, 53, 27, 297, DateTimeKind.Unspecified).AddTicks(8282), new TimeSpan(0, -8, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldUserEntryValue",
                columns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id", "Value" },
                values: new object[,]
                {
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("82598782-5832-4097-8b74-9caf3cf86a9d"), 0, "3" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("82598782-5832-4097-8b74-9caf3cf86a9d"), 0, "3" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("82598782-5832-4097-8b74-9caf3cf86a9d"), 0, "3" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), 0, "Hat Guy" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), 1, "Jes" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), 0, "Hat Guy" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), 1, "Jes" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), 0, "Hat Guy" },
                    { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), 1, "Jes" }
                });

            migrationBuilder.InsertData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                columns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id", "Value" },
                values: new object[,]
                {
                    { 1, new Guid("82598782-5832-4097-8b74-9caf3cf86a9d"), 0, "3" },
                    { 1, new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), 0, "Blue" },
                    { 1, new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), 1, "Hat Guy" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrowdSourcedFieldDefinition_Code",
                table: "CrowdSourcedFieldDefinition",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CrowdSourcedFieldDefinitionOption",
                table: "CrowdSourcedFieldDefinitionOption");

            migrationBuilder.DropIndex(
                name: "IX_CrowdSourcedFieldDefinition_Code",
                table: "CrowdSourcedFieldDefinition");

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("82598782-5832-4097-8b74-9caf3cf86a9d"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("82598782-5832-4097-8b74-9caf3cf86a9d"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("82598782-5832-4097-8b74-9caf3cf86a9d"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), 1 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), 1 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntryValue",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), 1 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, new Guid("82598782-5832-4097-8b74-9caf3cf86a9d"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), 0 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntryValue",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id" },
                keyValues: new object[] { 1, new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"), 1 });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("82598782-5832-4097-8b74-9caf3cf86a9d") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("82598782-5832-4097-8b74-9caf3cf86a9d") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("82598782-5832-4097-8b74-9caf3cf86a9d") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d275", new Guid("c0e6fa2b-902a-4066-aa85-83f540155466") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d276", new Guid("c0e6fa2b-902a-4066-aa85-83f540155466") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldUserEntry",
                keyColumns: new[] { "ComicId", "CreatedBy", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, "432ea055-ea01-443d-a6f7-e97d2c18d277", new Guid("c0e6fa2b-902a-4066-aa85-83f540155466") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntry",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, new Guid("82598782-5832-4097-8b74-9caf3cf86a9d") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldVerifiedEntry",
                keyColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId" },
                keyValues: new object[] { 1, new Guid("c0e6fa2b-902a-4066-aa85-83f540155466") });

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldDefinition",
                keyColumn: "Id",
                keyValue: new Guid("82598782-5832-4097-8b74-9caf3cf86a9d"));

            migrationBuilder.DeleteData(
                table: "CrowdSourcedFieldDefinition",
                keyColumn: "Id",
                keyValue: new Guid("c0e6fa2b-902a-4066-aa85-83f540155466"));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "CrowdSourcedFieldDefinitionOption",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CrowdSourcedFieldDefinitionOption",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "CrowdSourcedFieldDefinition",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CrowdSourcedFieldDefinitionOption",
                table: "CrowdSourcedFieldDefinitionOption",
                columns: new[] { "CrowdSourcedFieldDefinitionId", "Id" });

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
    }
}
