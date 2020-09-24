using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rotten_potatoes_api.Migrations
{
    public partial class reviewIdDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 24, 19, 3, 27, 122, DateTimeKind.Local).AddTicks(9081),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 9, 24, 15, 53, 39, 871, DateTimeKind.Local).AddTicks(7376));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 24, 15, 53, 39, 871, DateTimeKind.Local).AddTicks(7376),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 9, 24, 19, 3, 27, 122, DateTimeKind.Local).AddTicks(9081));
        }
    }
}
