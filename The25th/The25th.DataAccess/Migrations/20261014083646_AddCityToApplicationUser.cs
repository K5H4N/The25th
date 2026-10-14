using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace The25th.DataAccess.Migrations;

/// <inheritdoc />
public partial class _20261014083646_AddCityToApplicationUser : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "City",
            table: "AspNetUsers",
            type: "nvarchar(max)",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "City",
            table: "AspNetUsers");
    }
}
