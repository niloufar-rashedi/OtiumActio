using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OtiumActio.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Activity");

            migrationBuilder.EnsureSchema(
                name: "User");

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "Activity",
                columns: table => new
                {
                    Cat_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cat_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Category", x => x.Cat_Id);
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                schema: "Activity",
                columns: table => new
                {
                    Ac_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ac_Description = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Ac_Participants = table.Column<byte>(type: "tinyint", nullable: true),
                    Ac_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ac_CategoryId = table.Column<int>(type: "int", nullable: true),
                    Ac_Created = table.Column<DateTime>(type: "datetime", nullable: true),
                    AC_Modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Activity", x => x.Ac_Id);
                    table.ForeignKey(
                        name: "FK_CategoryId",
                        column: x => x.Ac_CategoryId,
                        principalSchema: "Activity",
                        principalTable: "Category",
                        principalColumn: "Cat_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityCategory",
                schema: "Activity",
                columns: table => new
                {
                    Acat_CategoryId = table.Column<int>(type: "int", nullable: false),
                    Acat_ActivityId = table.Column<int>(type: "int", nullable: true),
                    Acat_Created = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_AcatActivityId",
                        column: x => x.Acat_ActivityId,
                        principalSchema: "Activity",
                        principalTable: "Activity",
                        principalColumn: "Ac_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcatCategoryId",
                        column: x => x.Acat_CategoryId,
                        principalSchema: "Activity",
                        principalTable: "Category",
                        principalColumn: "Cat_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                schema: "User",
                columns: table => new
                {
                    Prtc_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prtc_ActivityId = table.Column<int>(type: "int", nullable: true),
                    Prtc_FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Prtc_LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Prtc_Age = table.Column<int>(type: "int", nullable: true),
                    Prtc_FavouritCategory = table.Column<int>(type: "int", nullable: true),
                    PrtcUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrtcPasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PrtcPasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PrtcCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrtcModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Participant", x => x.Prtc_Id);
                    table.ForeignKey(
                        name: "FK__Tbl_Parti__Prtc___787EE5A0",
                        column: x => x.Prtc_ActivityId,
                        principalSchema: "Activity",
                        principalTable: "Activity",
                        principalColumn: "Ac_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Tbl_Parti__Prtc___797309D9",
                        column: x => x.Prtc_FavouritCategory,
                        principalSchema: "Activity",
                        principalTable: "Category",
                        principalColumn: "Cat_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_Ac_CategoryId",
                schema: "Activity",
                table: "Activity",
                column: "Ac_CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityCategory_Acat_ActivityId",
                schema: "Activity",
                table: "ActivityCategory",
                column: "Acat_ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityCategory_Acat_CategoryId",
                schema: "Activity",
                table: "ActivityCategory",
                column: "Acat_CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_Prtc_ActivityId",
                schema: "User",
                table: "Participant",
                column: "Prtc_ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_Prtc_FavouritCategory",
                schema: "User",
                table: "Participant",
                column: "Prtc_FavouritCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityCategory",
                schema: "Activity");

            migrationBuilder.DropTable(
                name: "Participant",
                schema: "User");

            migrationBuilder.DropTable(
                name: "Activity",
                schema: "Activity");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "Activity");
        }
    }
}
