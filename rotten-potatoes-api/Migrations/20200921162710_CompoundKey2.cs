using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rotten_potatoes_api.Migrations
{
    public partial class CompoundKey2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                nullable: false,
                defaultValue: new DateTime(2020, 9, 21, 12, 27, 10, 92, DateTimeKind.Local).AddTicks(6471),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Reviews",
                nullable: false,
                defaultValue: "");

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
                name: "User",
                table: "Reviews");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 9, 21, 12, 27, 10, 92, DateTimeKind.Local).AddTicks(6471));

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
