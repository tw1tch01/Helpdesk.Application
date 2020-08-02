using Microsoft.EntityFrameworkCore.Migrations;

namespace Helpdesk.Persistence.Migrations.Tickets
{
    public partial class RemovedUniqueIndexOnTicketName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tickets_Name",
                table: "Tickets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Name",
                table: "Tickets",
                column: "Name",
                unique: true);
        }
    }
}
