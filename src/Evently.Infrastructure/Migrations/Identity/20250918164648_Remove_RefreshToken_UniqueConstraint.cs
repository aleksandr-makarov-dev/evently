using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evently.Infrastructure.Migrations.Identity;

/// <inheritdoc />
public partial class Remove_RefreshToken_UniqueConstraint : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_asp_net_users_RefreshToken",
            schema: "identity",
            table: "asp_net_users");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "IX_asp_net_users_RefreshToken",
            schema: "identity",
            table: "asp_net_users",
            column: "RefreshToken",
            unique: true);
    }
}
