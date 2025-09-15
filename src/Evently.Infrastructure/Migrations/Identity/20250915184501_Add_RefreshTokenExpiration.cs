using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evently.Infrastructure.Migrations.Identity;

/// <inheritdoc />
public partial class Add_RefreshTokenExpiration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "RefreshToken",
            schema: "identity",
            table: "asp_net_users",
            type: "character varying(500)",
            maxLength: 500,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AddColumn<DateTime>(
            name: "RefreshTokenExpiresAtUtc",
            schema: "identity",
            table: "asp_net_users",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "RefreshTokenExpiresAtUtc",
            schema: "identity",
            table: "asp_net_users");

        migrationBuilder.AlterColumn<string>(
            name: "RefreshToken",
            schema: "identity",
            table: "asp_net_users",
            type: "text",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(500)",
            oldMaxLength: 500);
    }
}
