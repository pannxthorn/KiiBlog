using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiiBlog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FLEX",
                columns: table => new
                {
                    FLEX_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FLEX_CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FLEX_NAME = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    CREATED_BY_ID = table.Column<int>(type: "int", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LAST_UPDATE_ID = table.Column<int>(type: "int", nullable: false),
                    LAST_UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ROW_UN = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FLEX", x => x.FLEX_ID);
                });

            migrationBuilder.CreateTable(
                name: "PLAYER",
                columns: table => new
                {
                    PLAYER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PLAYER_NO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PLAYER_NAME = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PLAYER_PROFILE = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    CONTRACT_TYPE_ID = table.Column<int>(type: "int", nullable: false),
                    CONTRACT_TYPE_CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CONTRACT_TYPE_NAME = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TRANSFER_STATUS_ID = table.Column<int>(type: "int", nullable: false),
                    TRANSFER_STATUS_CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TRANSFER_STATUS_NAME = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    CREATED_BY_ID = table.Column<int>(type: "int", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LAST_UPDATE_ID = table.Column<int>(type: "int", nullable: false),
                    LAST_UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ROW_UN = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLAYER", x => x.PLAYER_ID);
                });

            migrationBuilder.CreateTable(
                name: "FLEX_ITEM",
                columns: table => new
                {
                    FLEX_ITEM_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FLEX_ID = table.Column<int>(type: "int", nullable: false),
                    FLEX_ITEM_CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FLEX_ITEM_NAME = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    CREATED_BY_ID = table.Column<int>(type: "int", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LAST_UPDATE_ID = table.Column<int>(type: "int", nullable: false),
                    LAST_UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ROW_UN = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FLEX_ITEM", x => x.FLEX_ITEM_ID);
                    table.ForeignKey(
                        name: "FK_FLEX_ITEM_FLEX_FLEX_ID",
                        column: x => x.FLEX_ID,
                        principalTable: "FLEX",
                        principalColumn: "FLEX_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FLEX_FLEX_CODE",
                table: "FLEX",
                column: "FLEX_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_FLEX_ITEM_FLEX_ID",
                table: "FLEX_ITEM",
                column: "FLEX_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLEX_ITEM_FLEX_ITEM_CODE",
                table: "FLEX_ITEM",
                column: "FLEX_ITEM_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_PLAYER_PLAYER_NAME",
                table: "PLAYER",
                column: "PLAYER_NAME");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FLEX_ITEM");

            migrationBuilder.DropTable(
                name: "PLAYER");

            migrationBuilder.DropTable(
                name: "FLEX");
        }
    }
}
