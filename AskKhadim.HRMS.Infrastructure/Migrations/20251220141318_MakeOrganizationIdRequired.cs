using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace AskKhadim.HRMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeOrganizationIdRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_core_users_employee_id",
                table: "core_users");

            migrationBuilder.DropIndex(
                name: "UQ_core_users_email",
                table: "core_users");

            migrationBuilder.DropIndex(
                name: "UQ_core_users_employee_id",
                table: "core_users");

            migrationBuilder.DropColumn(
                name: "replaced_by_token",
                table: "refresh_tokens");

            migrationBuilder.DropColumn(
                name: "token",
                table: "refresh_tokens");

            migrationBuilder.AddColumn<Geometry>(
                name: "geo_point",
                table: "user_profile",
                type: "geography",
                nullable: true);

            migrationBuilder.AddColumn<Geometry>(
                name: "location",
                table: "user_attendance",
                type: "geography",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "replaced_by_token_hash",
                table: "refresh_tokens",
                type: "varbinary(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "revoked_reason",
                table: "refresh_tokens",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "token_hash",
                table: "refresh_tokens",
                type: "varbinary(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "organization_id",
                table: "core_users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "security_answer_hash",
                table: "core_users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "security_question",
                table: "core_users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "two_fa_preference",
                table: "core_users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "organization_invitations",
                columns: table => new
                {
                    invitation_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    organization_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    designation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    invite_token_hash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    expires_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    accepted_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true),
                    invited_by = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_invitations", x => x.invitation_id);
                    table.ForeignKey(
                        name: "FK_organization_invitations_core_users_invited_by",
                        column: x => x.invited_by,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organization_invitations_organizations_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organizations",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organization_invitations_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_user_id",
                table: "user_roles",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_profile_geo_point",
                table: "user_profile",
                column: "geo_point");

            migrationBuilder.CreateIndex(
                name: "IX_user_attendance_location",
                table: "user_attendance",
                column: "location");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_user_id",
                table: "refresh_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_core_users_org",
                table: "core_users",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "UQ_core_users_org_email",
                table: "core_users",
                columns: new[] { "organization_id", "email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_core_users_org_employee",
                table: "core_users",
                columns: new[] { "organization_id", "employee_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_organization_invitations_invited_by",
                table: "organization_invitations",
                column: "invited_by");

            migrationBuilder.CreateIndex(
                name: "IX_organization_invitations_organization_id_email",
                table: "organization_invitations",
                columns: new[] { "organization_id", "email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_organization_invitations_role_id",
                table: "organization_invitations",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_core_users_organizations_organization_id",
                table: "core_users",
                column: "organization_id",
                principalTable: "organizations",
                principalColumn: "organization_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_refresh_tokens_core_users_user_id",
                table: "refresh_tokens",
                column: "user_id",
                principalTable: "core_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_roles_core_users_user_id",
                table: "user_roles",
                column: "user_id",
                principalTable: "core_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_core_users_organizations_organization_id",
                table: "core_users");

            migrationBuilder.DropForeignKey(
                name: "FK_refresh_tokens_core_users_user_id",
                table: "refresh_tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_core_users_user_id",
                table: "user_roles");

            migrationBuilder.DropTable(
                name: "organization_invitations");

            migrationBuilder.DropIndex(
                name: "IX_user_roles_user_id",
                table: "user_roles");

            migrationBuilder.DropIndex(
                name: "IX_user_profile_geo_point",
                table: "user_profile");

            migrationBuilder.DropIndex(
                name: "IX_user_attendance_location",
                table: "user_attendance");

            migrationBuilder.DropIndex(
                name: "IX_refresh_tokens_user_id",
                table: "refresh_tokens");

            migrationBuilder.DropIndex(
                name: "IX_core_users_org",
                table: "core_users");

            migrationBuilder.DropIndex(
                name: "UQ_core_users_org_email",
                table: "core_users");

            migrationBuilder.DropIndex(
                name: "UQ_core_users_org_employee",
                table: "core_users");

            migrationBuilder.DropColumn(
                name: "geo_point",
                table: "user_profile");

            migrationBuilder.DropColumn(
                name: "location",
                table: "user_attendance");

            migrationBuilder.DropColumn(
                name: "replaced_by_token_hash",
                table: "refresh_tokens");

            migrationBuilder.DropColumn(
                name: "revoked_reason",
                table: "refresh_tokens");

            migrationBuilder.DropColumn(
                name: "token_hash",
                table: "refresh_tokens");

            migrationBuilder.DropColumn(
                name: "organization_id",
                table: "core_users");

            migrationBuilder.DropColumn(
                name: "security_answer_hash",
                table: "core_users");

            migrationBuilder.DropColumn(
                name: "security_question",
                table: "core_users");

            migrationBuilder.DropColumn(
                name: "two_fa_preference",
                table: "core_users");

            migrationBuilder.AddColumn<string>(
                name: "replaced_by_token",
                table: "refresh_tokens",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "token",
                table: "refresh_tokens",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_core_users_employee_id",
                table: "core_users",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "UQ_core_users_email",
                table: "core_users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_core_users_employee_id",
                table: "core_users",
                column: "employee_id",
                unique: true);
        }
    }
}
