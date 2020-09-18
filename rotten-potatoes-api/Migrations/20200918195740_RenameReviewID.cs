using Microsoft.EntityFrameworkCore.Migrations;

namespace rotten_potatoes_api.Migrations
{
    public partial class RenameReviewID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn("ReviewId", "Reviews", "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn("Id", "Reviews", "ReviewId");
        }
    }
}
