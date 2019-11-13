using Microsoft.EntityFrameworkCore.Migrations;

namespace PetrpkuWeb.Server.Migrations
{
    public partial class AddApprovedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSent",
                table: "Sents");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Publisheds");

            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "Checkeds");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Messages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "Messages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Messages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReadonly",
                table: "Messages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSent",
                table: "Messages",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsReadonly",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsSent",
                table: "Messages");

            migrationBuilder.AddColumn<bool>(
                name: "IsSent",
                table: "Sents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Publisheds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "Checkeds",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
