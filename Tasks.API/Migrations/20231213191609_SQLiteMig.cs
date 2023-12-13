using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasks.API.Migrations
{
    /// <inheritdoc />
    public partial class SQLiteMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    TITLE = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    DUE_DATE = table.Column<DateTime>(type: "datetime", nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_DUE_DATE_STATUS",
                table: "Tasks",
                columns: new[] { "DUE_DATE", "STATUS" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
