using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIMTool.Migrations
{
    public partial class Update_Delete_Action : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "PROJECT_ID",
                table: "PROJECT_EMPLOYEE");

            migrationBuilder.AddForeignKey(
                name: "PROJECT_ID",
                table: "PROJECT_EMPLOYEE",
                column: "ProjectId",
                principalTable: "PROJECT",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "PROJECT_ID",
                table: "PROJECT_EMPLOYEE");

            migrationBuilder.AddForeignKey(
                name: "PROJECT_ID",
                table: "PROJECT_EMPLOYEE",
                column: "ProjectId",
                principalTable: "PROJECT",
                principalColumn: "ID");
        }
    }
}
