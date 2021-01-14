using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoadingArtistCrowdSource.Server.Data.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Permalink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComicPublishedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tooltip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrlSrc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImportedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ImportedBy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastUpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comic_AspNetUsers_ImportedBy",
                        column: x => x.ImportedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comic_AspNetUsers_LastUpdatedBy",
                        column: x => x.LastUpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CrowdSourcedFieldDefinition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LongDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastUpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrowdSourcedFieldDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldDefinition_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldDefinition_AspNetUsers_LastUpdatedBy",
                        column: x => x.LastUpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComicHistoryLog",
                columns: table => new
                {
                    ComicId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CrowdSourcedFieldDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LogDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicHistoryLog", x => new { x.ComicId, x.Id });
                    table.ForeignKey(
                        name: "FK_ComicHistoryLog_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComicHistoryLog_Comic_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comic",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComicHistoryLog_CrowdSourcedFieldDefinition_CrowdSourcedFieldDefinitionId",
                        column: x => x.CrowdSourcedFieldDefinitionId,
                        principalTable: "CrowdSourcedFieldDefinition",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CrowdSourcedFieldDefinitionFeedback",
                columns: table => new
                {
                    CrowdSourcedFieldDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompletionDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CompletedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CompletionType = table.Column<int>(type: "int", nullable: true),
                    CompletionComment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrowdSourcedFieldDefinitionFeedback", x => new { x.CrowdSourcedFieldDefinitionId, x.Id });
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldDefinitionFeedback_AspNetUsers_CompletedBy",
                        column: x => x.CompletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldDefinitionFeedback_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldDefinitionFeedback_CrowdSourcedFieldDefinition_CrowdSourcedFieldDefinitionId",
                        column: x => x.CrowdSourcedFieldDefinitionId,
                        principalTable: "CrowdSourcedFieldDefinition",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CrowdSourcedFieldDefinitionHistoryLog",
                columns: table => new
                {
                    CrowdSourcedFieldDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LogDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LogMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrowdSourcedFieldDefinitionHistoryLog", x => new { x.CrowdSourcedFieldDefinitionId, x.Id });
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldDefinitionHistoryLog_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldDefinitionHistoryLog_CrowdSourcedFieldDefinition_CrowdSourcedFieldDefinitionId",
                        column: x => x.CrowdSourcedFieldDefinitionId,
                        principalTable: "CrowdSourcedFieldDefinition",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CrowdSourcedFieldUserEntry",
                columns: table => new
                {
                    ComicId = table.Column<int>(type: "int", nullable: false),
                    CrowdSourcedFieldDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrowdSourcedFieldUserEntry", x => new { x.ComicId, x.CrowdSourcedFieldDefinitionId, x.CreatedBy });
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldUserEntry_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldUserEntry_Comic_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comic",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldUserEntry_CrowdSourcedFieldDefinition_CrowdSourcedFieldDefinitionId",
                        column: x => x.CrowdSourcedFieldDefinitionId,
                        principalTable: "CrowdSourcedFieldDefinition",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CrowdSourcedFieldVerifiedEntry",
                columns: table => new
                {
                    ComicId = table.Column<int>(type: "int", nullable: false),
                    CrowdSourcedFieldDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstCreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VerificationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrowdSourcedFieldVerifiedEntry", x => new { x.ComicId, x.CrowdSourcedFieldDefinitionId });
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldVerifiedEntry_AspNetUsers_FirstCreatedBy",
                        column: x => x.FirstCreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldVerifiedEntry_Comic_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comic",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldVerifiedEntry_CrowdSourcedFieldDefinition_CrowdSourcedFieldDefinitionId",
                        column: x => x.CrowdSourcedFieldDefinitionId,
                        principalTable: "CrowdSourcedFieldDefinition",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CrowdSourcedFieldUserEntryValue",
                columns: table => new
                {
                    ComicId = table.Column<int>(type: "int", nullable: false),
                    CrowdSourcedFieldDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrowdSourcedFieldUserEntryValue", x => new { x.ComicId, x.CrowdSourcedFieldDefinitionId, x.CreatedBy, x.Id });
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldUserEntryValue_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldUserEntryValue_Comic_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldUserEntryValue_CrowdSourcedFieldDefinition_CrowdSourcedFieldDefinitionId",
                        column: x => x.CrowdSourcedFieldDefinitionId,
                        principalTable: "CrowdSourcedFieldDefinition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldUserEntryValue_CrowdSourcedFieldUserEntry_ComicId_CrowdSourcedFieldDefinitionId_CreatedBy",
                        columns: x => new { x.ComicId, x.CrowdSourcedFieldDefinitionId, x.CreatedBy },
                        principalTable: "CrowdSourcedFieldUserEntry",
                        principalColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "CreatedBy" });
                });

            migrationBuilder.CreateTable(
                name: "CrowdSourcedFieldVerifiedEntryValue",
                columns: table => new
                {
                    ComicId = table.Column<int>(type: "int", nullable: false),
                    CrowdSourcedFieldDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrowdSourcedFieldVerifiedEntryValue", x => new { x.ComicId, x.CrowdSourcedFieldDefinitionId, x.Id });
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldVerifiedEntryValue_Comic_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comic",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldVerifiedEntryValue_CrowdSourcedFieldDefinition_CrowdSourcedFieldDefinitionId",
                        column: x => x.CrowdSourcedFieldDefinitionId,
                        principalTable: "CrowdSourcedFieldDefinition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CrowdSourcedFieldVerifiedEntryValue_CrowdSourcedFieldVerifiedEntry_ComicId_CrowdSourcedFieldDefinitionId",
                        columns: x => new { x.ComicId, x.CrowdSourcedFieldDefinitionId },
                        principalTable: "CrowdSourcedFieldVerifiedEntry",
                        principalColumns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId" });
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "432ea055-ea01-443d-a6f7-e97d2c18d275", 0, "3acb17f1-65fe-4eac-bc2b-26403b23b999", "jman012guy@gmail.com", true, true, null, "JMAN012GUY@GMAIL.COM", "JMAN012GUY@GMAIL.COM", "AQAAAAEAACcQAAAAECj1ck3bM0AHKaarzX5DSSLNKUulcJBNwbMtMUT725YzRM3V789Tpk6VA9CjzC4bLg==", "", false, "", false, "jman012guy@gmail.com" });

            migrationBuilder.InsertData(
                table: "Comic",
                columns: new[] { "Id", "ComicPublishedDate", "Description", "ImageUrlSrc", "ImportedBy", "ImportedDate", "LastUpdatedBy", "LastUpdatedDate", "Permalink", "Title", "Tooltip" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2011, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -8, 0, 0, 0)), null, "https://loadingartist.com/wp-content/uploads/2011/07/2011-01-04-born.png", "432ea055-ea01-443d-a6f7-e97d2c18d275", new DateTimeOffset(new DateTime(2021, 1, 13, 23, 0, 32, 442, DateTimeKind.Unspecified).AddTicks(5945), new TimeSpan(0, -8, 0, 0, 0)), null, null, "https://loadingartist.com/comic/born/", "Born", "Born" });

            migrationBuilder.CreateIndex(
                name: "IX_Comic_ImportedBy",
                table: "Comic",
                column: "ImportedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Comic_LastUpdatedBy",
                table: "Comic",
                column: "LastUpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ComicHistoryLog_CreatedBy",
                table: "ComicHistoryLog",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ComicHistoryLog_CrowdSourcedFieldDefinitionId",
                table: "ComicHistoryLog",
                column: "CrowdSourcedFieldDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_CrowdSourcedFieldDefinition_CreatedBy",
                table: "CrowdSourcedFieldDefinition",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CrowdSourcedFieldDefinition_LastUpdatedBy",
                table: "CrowdSourcedFieldDefinition",
                column: "LastUpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CrowdSourcedFieldDefinitionFeedback_CompletedBy",
                table: "CrowdSourcedFieldDefinitionFeedback",
                column: "CompletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CrowdSourcedFieldDefinitionFeedback_CreatedBy",
                table: "CrowdSourcedFieldDefinitionFeedback",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CrowdSourcedFieldDefinitionHistoryLog_CreatedBy",
                table: "CrowdSourcedFieldDefinitionHistoryLog",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CrowdSourcedFieldUserEntry_CreatedBy",
                table: "CrowdSourcedFieldUserEntry",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CrowdSourcedFieldUserEntry_CrowdSourcedFieldDefinitionId",
                table: "CrowdSourcedFieldUserEntry",
                column: "CrowdSourcedFieldDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_CrowdSourcedFieldUserEntryValue_CreatedBy",
                table: "CrowdSourcedFieldUserEntryValue",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CrowdSourcedFieldUserEntryValue_CrowdSourcedFieldDefinitionId",
                table: "CrowdSourcedFieldUserEntryValue",
                column: "CrowdSourcedFieldDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_CrowdSourcedFieldVerifiedEntry_CrowdSourcedFieldDefinitionId",
                table: "CrowdSourcedFieldVerifiedEntry",
                column: "CrowdSourcedFieldDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_CrowdSourcedFieldVerifiedEntry_FirstCreatedBy",
                table: "CrowdSourcedFieldVerifiedEntry",
                column: "FirstCreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CrowdSourcedFieldVerifiedEntryValue_CrowdSourcedFieldDefinitionId",
                table: "CrowdSourcedFieldVerifiedEntryValue",
                column: "CrowdSourcedFieldDefinitionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComicHistoryLog");

            migrationBuilder.DropTable(
                name: "CrowdSourcedFieldDefinitionFeedback");

            migrationBuilder.DropTable(
                name: "CrowdSourcedFieldDefinitionHistoryLog");

            migrationBuilder.DropTable(
                name: "CrowdSourcedFieldUserEntryValue");

            migrationBuilder.DropTable(
                name: "CrowdSourcedFieldVerifiedEntryValue");

            migrationBuilder.DropTable(
                name: "CrowdSourcedFieldUserEntry");

            migrationBuilder.DropTable(
                name: "CrowdSourcedFieldVerifiedEntry");

            migrationBuilder.DropTable(
                name: "Comic");

            migrationBuilder.DropTable(
                name: "CrowdSourcedFieldDefinition");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "432ea055-ea01-443d-a6f7-e97d2c18d275");
        }
    }
}
