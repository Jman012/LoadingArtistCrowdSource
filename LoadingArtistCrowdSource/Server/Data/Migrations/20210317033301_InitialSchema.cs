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
                    Id = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Permalink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComicPublishedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tooltip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrlSrc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageThumbnailUrlSrc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageWideThumbnailUrlSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ComicTranscript",
                columns: table => new
                {
                    ComicId = table.Column<int>(type: "int", nullable: false),
                    LastEditedBy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastEditedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TranscriptContent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicTranscript", x => x.ComicId);
                    table.ForeignKey(
                        name: "FK_ComicTranscript_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComicTranscript_Comic_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComicTranscriptHistory",
                columns: table => new
                {
                    ComicId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TranscriptContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiffWithPrevious = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicTranscriptHistory", x => new { x.ComicId, x.Id });
                    table.ForeignKey(
                        name: "FK_ComicTranscriptHistory_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComicTranscriptHistory_Comic_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComicHistoryLog",
                columns: table => new
                {
                    ComicId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CrowdSourcedFieldDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LogDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LogMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    ComicId = table.Column<int>(type: "int", nullable: false),
                    CrowdSourcedFieldDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompletionDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CompletedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CompletionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletionComment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrowdSourcedFieldDefinitionFeedback", x => new { x.ComicId, x.CrowdSourcedFieldDefinitionId, x.Id });
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
                        name: "FK_CrowdSourcedFieldDefinitionFeedback_Comic_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comic",
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
                    LogMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "CrowdSourcedFieldDefinitionOption",
                columns: table => new
                {
                    CrowdSourcedFieldDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
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
                values: new object[] { "432ea055-ea01-443d-a6f7-e97d2c18d275", 0, "3acb17f1-65fe-4eac-bc2b-26403b23b999", "jman012guy@gmail.com", true, true, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "JMAN012GUY@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEK1gJpnKWF92WxUNfQ0m0rbjpk9K5isdrfTJQzBieoSS5AJP4LQ6wxDHGwor1uT86A==", "", false, "", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Comic_Code",
                table: "Comic",
                column: "Code",
                unique: true);

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
                name: "IX_ComicTag_CreatedBy",
                table: "ComicTag",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ComicTranscript_LastEditedBy",
                table: "ComicTranscript",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ComicTranscriptHistory_CreatedBy",
                table: "ComicTranscriptHistory",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CrowdSourcedFieldDefinition_Code",
                table: "CrowdSourcedFieldDefinition",
                column: "Code",
                unique: true);

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
                name: "IX_CrowdSourcedFieldDefinitionFeedback_CrowdSourcedFieldDefinitionId",
                table: "CrowdSourcedFieldDefinitionFeedback",
                column: "CrowdSourcedFieldDefinitionId");

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
                name: "ComicTag");

            migrationBuilder.DropTable(
                name: "ComicTranscript");

            migrationBuilder.DropTable(
                name: "ComicTranscriptHistory");

            migrationBuilder.DropTable(
                name: "CrowdSourcedFieldDefinitionFeedback");

            migrationBuilder.DropTable(
                name: "CrowdSourcedFieldDefinitionHistoryLog");

            migrationBuilder.DropTable(
                name: "CrowdSourcedFieldDefinitionOption");

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
