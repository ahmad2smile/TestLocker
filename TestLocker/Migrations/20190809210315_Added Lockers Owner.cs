using Microsoft.EntityFrameworkCore.Migrations;

namespace TestLocker.Migrations
{
    public partial class AddedLockersOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Lockers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lockers_OwnerId",
                table: "Lockers",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lockers_AspNetUsers_OwnerId",
                table: "Lockers",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lockers_AspNetUsers_OwnerId",
                table: "Lockers");

            migrationBuilder.DropIndex(
                name: "IX_Lockers_OwnerId",
                table: "Lockers");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Lockers");
        }
    }
}
