using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Persistence.Migrations
{
    public partial class AddedCustomerModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    File = table.Column<string>(type: "text", nullable: true),
                    Comment_Name = table.Column<string>(type: "text", nullable: true),
                    Comment_Description = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    Complaints_Name = table.Column<string>(type: "text", nullable: true),
                    Complaints_Description = table.Column<string>(type: "text", nullable: true),
                    Complaints_File = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    History_Description = table.Column<string>(type: "text", nullable: true),
                    HisFile = table.Column<string>(type: "text", nullable: true),
                    Qadamgoh_Name = table.Column<string>(type: "text", nullable: true),
                    Qadamgoh_Description = table.Column<string>(type: "text", nullable: true),
                    Qadamgoh_File = table.Column<string>(type: "text", nullable: true),
                    RestZone_Name = table.Column<string>(type: "text", nullable: true),
                    RestZone_Description = table.Column<string>(type: "text", nullable: true),
                    BestRestZone = table.Column<bool>(type: "boolean", nullable: true),
                    RestZoneFiles_Name = table.Column<string>(type: "text", nullable: true),
                    RestZoneId = table.Column<int>(type: "integer", nullable: true),
                    Workshop_Name = table.Column<string>(type: "text", nullable: true),
                    Workshop_Description = table.Column<string>(type: "text", nullable: true),
                    WFile = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseModels_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseModels_BaseModels_RestZoneId",
                        column: x => x.RestZoneId,
                        principalTable: "BaseModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseModels_RestZoneId",
                table: "BaseModels",
                column: "RestZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseModels_UserId",
                table: "BaseModels",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseModels");
        }
    }
}
