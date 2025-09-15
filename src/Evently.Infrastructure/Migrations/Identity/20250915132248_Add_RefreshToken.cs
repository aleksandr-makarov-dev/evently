using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evently.Infrastructure.Migrations.Identity;

/// <inheritdoc />
public partial class Add_RefreshToken : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "RefreshToken",
            schema: "identity",
            table: "asp_net_users",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.CreateIndex(
            name: "IX_asp_net_users_RefreshToken",
            schema: "identity",
            table: "asp_net_users",
            column: "RefreshToken",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_asp_net_users_RefreshToken",
            schema: "identity",
            table: "asp_net_users");

        migrationBuilder.DropColumn(
            name: "RefreshToken",
            schema: "identity",
            table: "asp_net_users");
    }
}
