using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evently.Infrastructure.Migrations.Application;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "public");

        migrationBuilder.CreateTable(
            name: "categories",
            schema: "public",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                IsArchived = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_categories", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "users",
            schema: "public",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                IdentityId = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_users", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "events",
            schema: "public",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                Location = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                StartsAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                EndsAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                Status = table.Column<int>(type: "integer", nullable: false),
                CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_events", x => x.Id);
                table.ForeignKey(
                    name: "FK_events_categories_CategoryId",
                    column: x => x.CategoryId,
                    principalSchema: "public",
                    principalTable: "categories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ticket_types",
            schema: "public",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                EventId = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                Price = table.Column<decimal>(type: "numeric", nullable: false),
                Quantity = table.Column<decimal>(type: "numeric", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ticket_types", x => x.Id);
                table.ForeignKey(
                    name: "FK_ticket_types_events_EventId",
                    column: x => x.EventId,
                    principalSchema: "public",
                    principalTable: "events",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_events_CategoryId",
            schema: "public",
            table: "events",
            column: "CategoryId");

        migrationBuilder.CreateIndex(
            name: "IX_ticket_types_EventId",
            schema: "public",
            table: "ticket_types",
            column: "EventId");

        migrationBuilder.CreateIndex(
            name: "IX_users_IdentityId",
            schema: "public",
            table: "users",
            column: "IdentityId",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ticket_types",
            schema: "public");

        migrationBuilder.DropTable(
            name: "users",
            schema: "public");

        migrationBuilder.DropTable(
            name: "events",
            schema: "public");

        migrationBuilder.DropTable(
            name: "categories",
            schema: "public");
    }
}
