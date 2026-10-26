using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusForever.Database.Character.Migrations
{
    /// <inheritdoc />
    public partial class CharacterBindPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ushort>(
                name: "bindPoint",
                table: "character",
                type: "smallint(5) unsigned",
                nullable: false,
                defaultValue: (ushort)0);

            migrationBuilder.AddColumn<ushort>(
                name: "bindPointWorldId",
                table: "character",
                type: "smallint(5) unsigned",
                nullable: false,
                defaultValue: (ushort)0);

            migrationBuilder.AddColumn<float>(
                name: "bindPointLocationX",
                table: "character",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "bindPointLocationY",
                table: "character",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "bindPointLocationZ",
                table: "character",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "bindPointRotationX",
                table: "character",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "bindPointRotationY",
                table: "character",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "bindPointRotationZ",
                table: "character",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bindPoint",
                table: "character");

            migrationBuilder.DropColumn(
                name: "bindPointWorldId",
                table: "character");

            migrationBuilder.DropColumn(
                name: "bindPointLocationX",
                table: "character");

            migrationBuilder.DropColumn(
                name: "bindPointLocationY",
                table: "character");

            migrationBuilder.DropColumn(
                name: "bindPointLocationZ",
                table: "character");

            migrationBuilder.DropColumn(
                name: "bindPointRotationX",
                table: "character");

            migrationBuilder.DropColumn(
                name: "bindPointRotationY",
                table: "character");

            migrationBuilder.DropColumn(
                name: "bindPointRotationZ",
                table: "character");
        }
    }
}
