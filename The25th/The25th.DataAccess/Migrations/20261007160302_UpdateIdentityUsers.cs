using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace The25th.DataAccess.Migrations;

/// <inheritdoc />
public partial class _20261007160302_UpdateIdentityUsers : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Name",
            table: "AspNetUsers",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "StreetAddress",
            table: "AspNetUsers",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "State",
            table: "AspNetUsers",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "PostalCode",
            table: "AspNetUsers",
            type: "nvarchar(max)",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Name",
            table: "AspNetUsers");

        migrationBuilder.DropColumn(
            name: "StreetAddress",
            table: "AspNetUsers");

        migrationBuilder.DropColumn(
            name: "State",
            table: "AspNetUsers");

        migrationBuilder.DropColumn(
            name: "PostalCode",
            table: "AspNetUsers");
    }
}
