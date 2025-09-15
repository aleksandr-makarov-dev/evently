using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evently.Infrastructure.Migrations.Identity;

/// <inheritdoc />
public partial class RefreshToken_Nullable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "RefreshTokenExpiresAtUtc",
            schema: "identity",
            table: "asp_net_users",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<string>(
            name: "RefreshToken",
            schema: "identity",
            table: "asp_net_users",
            type: "character varying(500)",
            maxLength: 500,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(500)",
            oldMaxLength: 500);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "RefreshTokenExpiresAtUtc",
            schema: "identity",
            table: "asp_net_users",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "RefreshToken",
            schema: "identity",
            table: "asp_net_users",
            type: "character varying(500)",
            maxLength: 500,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "character varying(500)",
            oldMaxLength: 500,
            oldNullable: true);
    }
}
