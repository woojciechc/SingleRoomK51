using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SingleRoom.Data.Migrations
{
    public partial class AddParticipantMail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParticipantEmail",
                table: "Trip",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipantEmail",
                table: "Trip");
        }
    }
}
