using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rotten_potatoes_api.Migrations
{
    public partial class CompoundKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 19, 17, 22, 54, 533, DateTimeKind.Local).AddTicks(4959));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                columns: new[] { "Game", "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "AddDate",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");
        }
    }
}
