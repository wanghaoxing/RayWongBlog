using Microsoft.EntityFrameworkCore.Migrations;

namespace RayWongBlog.Infrastructure.Migrations
{
    public partial class addremark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Articles",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Articles");
        }
    }
}
