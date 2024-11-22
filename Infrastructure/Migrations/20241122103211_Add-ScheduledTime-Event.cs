using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameClubAndEvent.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddScheduledTimeEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledTime",
                table: "Events",
                type: "TEXT",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduledTime",
                table: "Events");
        }
    }
}
