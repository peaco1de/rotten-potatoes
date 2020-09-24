using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rotten_potatoes_api.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 24, 14, 49, 43, 520, DateTimeKind.Local).AddTicks(6527),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 9, 24, 14, 47, 22, 72, DateTimeKind.Local).AddTicks(9569));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 24, 14, 47, 22, 72, DateTimeKind.Local).AddTicks(9569),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 9, 24, 14, 49, 43, 520, DateTimeKind.Local).AddTicks(6527));
        }
    }
}
