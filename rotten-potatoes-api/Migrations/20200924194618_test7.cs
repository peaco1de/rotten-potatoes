using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rotten_potatoes_api.Migrations
{
    public partial class test7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 24, 15, 46, 18, 87, DateTimeKind.Local).AddTicks(8243),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 9, 24, 15, 44, 8, 663, DateTimeKind.Local).AddTicks(749));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 24, 15, 44, 8, 663, DateTimeKind.Local).AddTicks(749),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 9, 24, 15, 46, 18, 87, DateTimeKind.Local).AddTicks(8243));
        }
    }
}
