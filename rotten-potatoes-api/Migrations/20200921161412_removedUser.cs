using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rotten_potatoes_api.Migrations
{
    public partial class removedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Reviews");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddDate",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 9, 19, 17, 22, 54, 533, DateTimeKind.Local).AddTicks(4959));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Reviews",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Reviews");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 19, 17, 22, 54, 533, DateTimeKind.Local).AddTicks(4959),
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                columns: new[] { "Game", "User" });
        }
    }
}
