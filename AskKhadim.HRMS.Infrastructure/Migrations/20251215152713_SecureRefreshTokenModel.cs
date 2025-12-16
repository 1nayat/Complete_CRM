using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AskKhadim.HRMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecureRefreshTokenModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "audit_logs",
                columns: table => new
                {
                    audit_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    actor_user_id = table.Column<long>(type: "bigint", nullable: true),
                    actor_role = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    action_type = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    entity_type = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    entity_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    old_value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    new_value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    timestamp = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    correlation_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audit_logs", x => x.audit_id);
                });

            migrationBuilder.CreateTable(
                name: "core_users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    employee_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    password_hash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    email_verified = table.Column<bool>(type: "bit", nullable: false),
                    last_login = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true),
                    notice_period_days = table.Column<int>(type: "int", nullable: false),
                    linkedin_profile_url = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_core_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "legal_consents",
                columns: table => new
                {
                    consent_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    organization_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    consent_key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    consent_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    accepted_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    accepted_by_ip = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_legal_consents", x => x.consent_id);
                });

            migrationBuilder.CreateTable(
                name: "organizations",
                columns: table => new
                {
                    organization_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    organization_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    industry = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    tax_registration_number = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    year_established = table.Column<int>(type: "int", nullable: true),
                    company_size = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    website_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    brief_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    primary_products = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    target_market = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    revenue_range = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    preferred_plan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    expected_user_count = table.Column<int>(type: "int", nullable: true),
                    preferred_language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    time_zone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizations", x => x.organization_id);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    expires_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    revoked = table.Column<bool>(type: "bit", nullable: false),
                    revoked_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true),
                    created_by_ip = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    replaced_by_token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_tokens", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    department_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    department_code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    department_name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    parent_department_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    department_head_id = table.Column<long>(type: "bigint", nullable: true),
                    cost_center = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    location = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.department_id);
                    table.ForeignKey(
                        name: "FK_departments_head",
                        column: x => x.department_head_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_departments_parent",
                        column: x => x.parent_department_id,
                        principalTable: "departments",
                        principalColumn: "department_id");
                });

            migrationBuilder.CreateTable(
                name: "user_appraisal_history",
                columns: table => new
                {
                    appraisal_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    appraisal_period_start = table.Column<DateOnly>(type: "date", nullable: false),
                    appraisal_period_end = table.Column<DateOnly>(type: "date", nullable: false),
                    overall_rating = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    appraisal_document_url = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    appraised_by = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_appraisal_history", x => x.appraisal_id);
                    table.ForeignKey(
                        name: "FK_user_appraisal_history_appraised_by",
                        column: x => x.appraised_by,
                        principalTable: "core_users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_appraisal_history_user",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_assets",
                columns: table => new
                {
                    asset_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    asset_type = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    asset_name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    brand = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    model = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    serial_number = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    asset_tag = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    purchase_date = table.Column<DateOnly>(type: "date", nullable: true),
                    warranty_expiry_date = table.Column<DateOnly>(type: "date", nullable: true),
                    assigned_date = table.Column<DateOnly>(type: "date", nullable: true),
                    return_date = table.Column<DateOnly>(type: "date", nullable: true),
                    condition_on_assignment = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    condition_on_return = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    estimated_value = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "Assigned"),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    assigned_by = table.Column<long>(type: "bigint", nullable: true),
                    returned_to = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_assets", x => x.asset_id);
                    table.ForeignKey(
                        name: "FK_user_assets_user",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_attendance",
                columns: table => new
                {
                    attendance_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    work_date = table.Column<DateOnly>(type: "date", nullable: false),
                    punch_in = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true),
                    punch_out = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true),
                    duration_minutes = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "Present"),
                    device_info = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_attendance", x => x.attendance_id);
                    table.ForeignKey(
                        name: "FK_user_attendance_user",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_bank_details_secure",
                columns: table => new
                {
                    bank_detail_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    bank_name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    account_number_encrypted = table.Column<byte[]>(type: "varbinary(1024)", maxLength: 1024, nullable: true),
                    ifsc_code_encrypted = table.Column<byte[]>(type: "varbinary(512)", maxLength: 512, nullable: true),
                    branch_name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    is_primary = table.Column<bool>(type: "bit", nullable: false),
                    is_salary_account = table.Column<bool>(type: "bit", nullable: false),
                    account_holder_name_encrypted = table.Column<byte[]>(type: "varbinary(1024)", maxLength: 1024, nullable: true),
                    is_verified = table.Column<bool>(type: "bit", nullable: false),
                    verified_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_bank_details_secure", x => x.bank_detail_id);
                    table.ForeignKey(
                        name: "FK_user_bank_details_secure_core_users",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_certifications",
                columns: table => new
                {
                    certification_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    certification_name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    issuing_organization = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    issue_date = table.Column<DateOnly>(type: "date", nullable: true),
                    expiry_date = table.Column<DateOnly>(type: "date", nullable: true),
                    credential_id = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    credential_url = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_certifications", x => x.certification_id);
                    table.ForeignKey(
                        name: "FK_user_certifications_user",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_documents",
                columns: table => new
                {
                    document_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    document_type = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    document_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    document_url = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    file_size_kb = table.Column<int>(type: "int", nullable: true),
                    mime_type = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    upload_date = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    uploaded_by = table.Column<long>(type: "bigint", nullable: true),
                    expiry_date = table.Column<DateOnly>(type: "date", nullable: true),
                    is_verified = table.Column<bool>(type: "bit", nullable: false),
                    verified_by = table.Column<long>(type: "bigint", nullable: true),
                    verified_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_documents", x => x.document_id);
                    table.ForeignKey(
                        name: "FK_user_documents_uploaded_by",
                        column: x => x.uploaded_by,
                        principalTable: "core_users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_documents_user",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_documents_verified_by",
                        column: x => x.verified_by,
                        principalTable: "core_users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_education",
                columns: table => new
                {
                    education_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    degree_type = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    degree_name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    specialization = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    institution_name = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    university_name = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    start_year = table.Column<short>(type: "smallint", nullable: true),
                    end_year = table.Column<short>(type: "smallint", nullable: true),
                    grade_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    grade_value = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    is_highest = table.Column<bool>(type: "bit", nullable: false),
                    certificate_url = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_education", x => x.education_id);
                    table.ForeignKey(
                        name: "FK_user_education_user",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_experience",
                columns: table => new
                {
                    experience_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    company_name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    designation = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    department = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    employment_type = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    is_current = table.Column<bool>(type: "bit", nullable: false),
                    duration_years = table.Column<decimal>(type: "numeric(17,6)", nullable: true, computedColumnSql: "(datediff(month,[start_date],isnull([end_date],CONVERT([date],sysutcdatetime())))/(12.0))", stored: false),
                    job_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reporting_to = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    reason_for_leaving = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    salary_drawn = table.Column<decimal>(type: "decimal(14,2)", nullable: true),
                    currency = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false, defaultValue: "INR"),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_experience", x => x.experience_id);
                    table.ForeignKey(
                        name: "FK_user_experience_user",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_health_insurance",
                columns: table => new
                {
                    health_insurance_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    provider_name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    policy_number_encrypted = table.Column<byte[]>(type: "varbinary(1024)", maxLength: 1024, nullable: true),
                    coverage_details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_health_insurance", x => x.health_insurance_id);
                    table.ForeignKey(
                        name: "FK_user_health_insurance_user",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_languages",
                columns: table => new
                {
                    language_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    language = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    proficiency = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "Basic"),
                    is_primary = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_languages", x => x.language_id);
                    table.ForeignKey(
                        name: "FK_user_languages_user",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_leave_balance",
                columns: table => new
                {
                    leave_balance_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    leave_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    balance = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    year = table.Column<short>(type: "smallint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_leave_balance", x => x.leave_balance_id);
                    table.ForeignKey(
                        name: "FK_user_leave_balance_user",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_leave_requests",
                columns: table => new
                {
                    leave_request_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    leave_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    days = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    reason = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, defaultValue: "Pending"),
                    requested_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    processed_by = table.Column<long>(type: "bigint", nullable: true),
                    processed_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_leave_requests", x => x.leave_request_id);
                    table.ForeignKey(
                        name: "FK_user_leave_requests_processed_by",
                        column: x => x.processed_by,
                        principalTable: "core_users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_leave_requests_user",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_performance_ratings",
                columns: table => new
                {
                    rating_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    rating_period_start = table.Column<DateOnly>(type: "date", nullable: false),
                    rating_period_end = table.Column<DateOnly>(type: "date", nullable: false),
                    rating_score = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    rating_level = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rated_by = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_performance_ratings", x => x.rating_id);
                    table.ForeignKey(
                        name: "FK_user_performance_ratings_rated_by",
                        column: x => x.rated_by,
                        principalTable: "core_users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_performance_ratings_user",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_profile",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    user_uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    first_name = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    middle_name = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true),
                    last_name = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    personal_email = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    alternate_phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: true),
                    gender = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    blood_group = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    marital_status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    nationality = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    father_name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    mother_name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    spouse_name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    current_address_line1 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    current_address_line2 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    current_city = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    current_district = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    current_state = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    current_country = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true, defaultValue: "India"),
                    current_pincode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    permanent_address_line1 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    permanent_address_line2 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    permanent_city = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    permanent_district = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    permanent_state = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    permanent_country = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true, defaultValue: "India"),
                    permanent_pincode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    is_same_address = table.Column<bool>(type: "bit", nullable: false),
                    geo_last_updated = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true),
                    emergency_contact_name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    emergency_contact_relationship = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    emergency_contact_phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    emergency_contact_alternate_phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    emergency_contact_address = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    profile_photo_url = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    resume_url = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_profile", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_user_profile_core_users",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_salary",
                columns: table => new
                {
                    salary_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    salary_encrypted = table.Column<byte[]>(type: "varbinary(2048)", maxLength: 2048, nullable: false),
                    currency = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false, defaultValue: "INR"),
                    effective_from = table.Column<DateOnly>(type: "date", nullable: false),
                    effective_to = table.Column<DateOnly>(type: "date", nullable: true),
                    is_current = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    created_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_salary", x => x.salary_id);
                    table.ForeignKey(
                        name: "FK_user_salary_core_users",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_sensitive_identifiers",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    pan_hash = table.Column<byte[]>(type: "varbinary(64)", maxLength: 64, nullable: true),
                    aadhaar_hash = table.Column<byte[]>(type: "varbinary(64)", maxLength: 64, nullable: true),
                    pan_encrypted = table.Column<byte[]>(type: "varbinary(1024)", maxLength: 1024, nullable: true),
                    aadhaar_encrypted = table.Column<byte[]>(type: "varbinary(2048)", maxLength: 2048, nullable: true),
                    passport_encrypted = table.Column<byte[]>(type: "varbinary(1024)", maxLength: 1024, nullable: true),
                    govt_id_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    govt_id_number_encrypted = table.Column<byte[]>(type: "varbinary(2048)", maxLength: 2048, nullable: true),
                    govt_id_issue_date = table.Column<DateOnly>(type: "date", nullable: true),
                    govt_id_expiry_date = table.Column<DateOnly>(type: "date", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_sensitive_identifiers", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_user_sensitive_identifiers_core_users",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_skills",
                columns: table => new
                {
                    skill_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    skill_name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    skill_category = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    proficiency_level = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    years_of_experience = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    last_used_year = table.Column<short>(type: "smallint", nullable: true),
                    is_primary = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_skills", x => x.skill_id);
                    table.ForeignKey(
                        name: "FK_user_skills_user",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_training_records",
                columns: table => new
                {
                    training_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    training_name = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    provider = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    completion_status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "Planned"),
                    score = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    certificate_url = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_training_records", x => x.training_id);
                    table.ForeignKey(
                        name: "FK_user_training_records_user",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invitations",
                columns: table => new
                {
                    invitation_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    email = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    organization_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    token_hash = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    expires_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    used = table.Column<bool>(type: "bit", nullable: false),
                    used_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true),
                    used_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invitations", x => x.invitation_id);
                    table.ForeignKey(
                        name: "FK_invitations_organizations_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organizations",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organization_addresses",
                columns: table => new
                {
                    address_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    organization_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    address_line1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    address_line2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    city = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    state_province = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    postal_code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    country = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_addresses", x => x.address_id);
                    table.ForeignKey(
                        name: "FK_organization_addresses_organizations_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organizations",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organization_contacts",
                columns: table => new
                {
                    contact_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    organization_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    job_title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    alt_phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    is_primary = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_contacts", x => x.contact_id);
                    table.ForeignKey(
                        name: "FK_organization_contacts_organizations_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organizations",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organization_files",
                columns: table => new
                {
                    file_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    organization_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    file_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    file_url = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    content_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    size_bytes = table.Column<long>(type: "bigint", nullable: true),
                    uploaded_by = table.Column<long>(type: "bigint", nullable: true),
                    uploaded_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_files", x => x.file_id);
                    table.ForeignKey(
                        name: "FK_organization_files_organizations_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organizations",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_role_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    organization_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    assigned_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    assigned_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => x.user_role_id);
                    table.ForeignKey(
                        name: "FK_user_roles_organizations_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organizations",
                        principalColumn: "organization_id");
                    table.ForeignKey(
                        name: "FK_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_employment_history",
                columns: table => new
                {
                    history_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    department_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    designation = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    reporting_manager_id = table.Column<long>(type: "bigint", nullable: true),
                    employment_type = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    work_location = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    effective_from = table.Column<DateOnly>(type: "date", nullable: false),
                    effective_to = table.Column<DateOnly>(type: "date", nullable: true),
                    change_reason = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    changed_by = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_employment_history", x => x.history_id);
                    table.ForeignKey(
                        name: "FK_user_employment_history_changed_by",
                        column: x => x.changed_by,
                        principalTable: "core_users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_employment_history_department",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "department_id");
                    table.ForeignKey(
                        name: "FK_user_employment_history_reporting_manager",
                        column: x => x.reporting_manager_id,
                        principalTable: "core_users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_employment_history_user",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_hr",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    user_uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    department_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    designation = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    reporting_manager_id = table.Column<long>(type: "bigint", nullable: true),
                    work_location = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    work_type = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "On Site"),
                    employment_type = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "Full Time"),
                    employment_status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "Active"),
                    onboarding_date = table.Column<DateOnly>(type: "date", nullable: true),
                    joining_date = table.Column<DateOnly>(type: "date", nullable: true),
                    probation_start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    probation_end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    confirmation_date = table.Column<DateOnly>(type: "date", nullable: true),
                    exit_date = table.Column<DateOnly>(type: "date", nullable: true),
                    prior_total_experience_years = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    prior_relevant_experience_years = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    total_experience_years = table.Column<decimal>(type: "numeric(18,6)", nullable: true, computedColumnSql: "([prior_total_experience_years]+isnull(datediff(month,[joining_date],isnull([exit_date],CONVERT([date],sysutcdatetime())))/(12.0),(0)))", stored: false),
                    access_type = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "Limited Access"),
                    access_level = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_hr", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_user_hr_core_users",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_hr_departments",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "department_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_user_hr_reporting_manager",
                        column: x => x.reporting_manager_id,
                        principalTable: "core_users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_core_users_employee_id",
                table: "core_users",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_core_users_last_login",
                table: "core_users",
                column: "last_login");

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

            migrationBuilder.CreateIndex(
                name: "UQ_core_users_user_uuid",
                table: "core_users",
                column: "user_uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_departments_active",
                table: "departments",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "IX_departments_code",
                table: "departments",
                column: "department_code");

            migrationBuilder.CreateIndex(
                name: "IX_departments_head",
                table: "departments",
                column: "department_head_id");

            migrationBuilder.CreateIndex(
                name: "IX_departments_name",
                table: "departments",
                column: "department_name");

            migrationBuilder.CreateIndex(
                name: "IX_departments_parent",
                table: "departments",
                column: "parent_department_id");

            migrationBuilder.CreateIndex(
                name: "UQ_departments_code",
                table: "departments",
                column: "department_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_invitations_email",
                table: "invitations",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_invitations_organization_id",
                table: "invitations",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_organization_addresses_organization_id",
                table: "organization_addresses",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_organization_contacts_organization_id",
                table: "organization_contacts",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_organization_files_organization_id",
                table: "organization_files",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "UQ__roles__783254B1FAA3D0AF",
                table: "roles",
                column: "role_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_appraisal_history_appraised_by",
                table: "user_appraisal_history",
                column: "appraised_by");

            migrationBuilder.CreateIndex(
                name: "IX_user_appraisal_history_period",
                table: "user_appraisal_history",
                columns: new[] { "appraisal_period_start", "appraisal_period_end" });

            migrationBuilder.CreateIndex(
                name: "IX_user_appraisal_history_user",
                table: "user_appraisal_history",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_assets_status",
                table: "user_assets",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_user_assets_type",
                table: "user_assets",
                column: "asset_type");

            migrationBuilder.CreateIndex(
                name: "IX_user_assets_user",
                table: "user_assets",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ_user_assets_asset_tag",
                table: "user_assets",
                column: "asset_tag",
                unique: true,
                filter: "([asset_tag] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UQ_user_assets_serial_number",
                table: "user_assets",
                column: "serial_number",
                unique: true,
                filter: "([serial_number] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_user_attendance_user_date",
                table: "user_attendance",
                columns: new[] { "user_id", "work_date" });

            migrationBuilder.CreateIndex(
                name: "IX_user_bank_details_is_primary",
                table: "user_bank_details_secure",
                column: "is_primary");

            migrationBuilder.CreateIndex(
                name: "IX_user_bank_details_user",
                table: "user_bank_details_secure",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_certifications_expiry",
                table: "user_certifications",
                column: "expiry_date");

            migrationBuilder.CreateIndex(
                name: "IX_user_certifications_user",
                table: "user_certifications",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_documents_expiry",
                table: "user_documents",
                column: "expiry_date");

            migrationBuilder.CreateIndex(
                name: "IX_user_documents_type",
                table: "user_documents",
                column: "document_type");

            migrationBuilder.CreateIndex(
                name: "IX_user_documents_uploaded_by",
                table: "user_documents",
                column: "uploaded_by");

            migrationBuilder.CreateIndex(
                name: "IX_user_documents_user",
                table: "user_documents",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_documents_verified",
                table: "user_documents",
                column: "is_verified");

            migrationBuilder.CreateIndex(
                name: "IX_user_documents_verified_by",
                table: "user_documents",
                column: "verified_by");

            migrationBuilder.CreateIndex(
                name: "IX_user_education_end_year",
                table: "user_education",
                column: "end_year");

            migrationBuilder.CreateIndex(
                name: "IX_user_education_user",
                table: "user_education",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_employment_history_changed_by",
                table: "user_employment_history",
                column: "changed_by");

            migrationBuilder.CreateIndex(
                name: "IX_user_employment_history_department_id",
                table: "user_employment_history",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_employment_history_effective_dates",
                table: "user_employment_history",
                columns: new[] { "effective_from", "effective_to" });

            migrationBuilder.CreateIndex(
                name: "IX_user_employment_history_reporting_manager_id",
                table: "user_employment_history",
                column: "reporting_manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_employment_history_user",
                table: "user_employment_history",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_experience_current",
                table: "user_experience",
                column: "is_current");

            migrationBuilder.CreateIndex(
                name: "IX_user_experience_dates",
                table: "user_experience",
                columns: new[] { "start_date", "end_date" });

            migrationBuilder.CreateIndex(
                name: "IX_user_experience_user",
                table: "user_experience",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_health_insurance_active",
                table: "user_health_insurance",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "IX_user_health_insurance_user",
                table: "user_health_insurance",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_hr_department",
                table: "user_hr",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_hr_designation",
                table: "user_hr",
                column: "designation");

            migrationBuilder.CreateIndex(
                name: "IX_user_hr_employment_status",
                table: "user_hr",
                column: "employment_status");

            migrationBuilder.CreateIndex(
                name: "IX_user_hr_employment_type",
                table: "user_hr",
                column: "employment_type");

            migrationBuilder.CreateIndex(
                name: "IX_user_hr_joining_date",
                table: "user_hr",
                column: "joining_date");

            migrationBuilder.CreateIndex(
                name: "IX_user_hr_probation_end",
                table: "user_hr",
                column: "probation_end_date");

            migrationBuilder.CreateIndex(
                name: "IX_user_hr_reporting_manager",
                table: "user_hr",
                column: "reporting_manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_languages_language",
                table: "user_languages",
                column: "language");

            migrationBuilder.CreateIndex(
                name: "IX_user_languages_user",
                table: "user_languages",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_leave_balance_user",
                table: "user_leave_balance",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ_user_leave_balance_user_type_year",
                table: "user_leave_balance",
                columns: new[] { "user_id", "leave_type", "year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_leave_requests_processed_by",
                table: "user_leave_requests",
                column: "processed_by");

            migrationBuilder.CreateIndex(
                name: "IX_user_leave_requests_start_date",
                table: "user_leave_requests",
                column: "start_date");

            migrationBuilder.CreateIndex(
                name: "IX_user_leave_requests_status",
                table: "user_leave_requests",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_user_leave_requests_user",
                table: "user_leave_requests",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_performance_ratings_period",
                table: "user_performance_ratings",
                columns: new[] { "rating_period_start", "rating_period_end" });

            migrationBuilder.CreateIndex(
                name: "IX_user_performance_ratings_rated_by",
                table: "user_performance_ratings",
                column: "rated_by");

            migrationBuilder.CreateIndex(
                name: "IX_user_performance_ratings_user",
                table: "user_performance_ratings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_profile_city",
                table: "user_profile",
                column: "current_city");

            migrationBuilder.CreateIndex(
                name: "IX_user_profile_phone",
                table: "user_profile",
                column: "phone");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_organization_id",
                table: "user_roles",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_salary_current",
                table: "user_salary",
                column: "is_current");

            migrationBuilder.CreateIndex(
                name: "IX_user_salary_user",
                table: "user_salary",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UX_user_sensitive_identifiers_aadhaar_hash",
                table: "user_sensitive_identifiers",
                column: "aadhaar_hash",
                unique: true,
                filter: "([aadhaar_hash] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UX_user_sensitive_identifiers_pan_hash",
                table: "user_sensitive_identifiers",
                column: "pan_hash",
                unique: true,
                filter: "([pan_hash] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_user_skills_skill_name",
                table: "user_skills",
                column: "skill_name");

            migrationBuilder.CreateIndex(
                name: "IX_user_skills_user",
                table: "user_skills",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_training_records_status",
                table: "user_training_records",
                column: "completion_status");

            migrationBuilder.CreateIndex(
                name: "IX_user_training_records_user",
                table: "user_training_records",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit_logs");

            migrationBuilder.DropTable(
                name: "invitations");

            migrationBuilder.DropTable(
                name: "legal_consents");

            migrationBuilder.DropTable(
                name: "organization_addresses");

            migrationBuilder.DropTable(
                name: "organization_contacts");

            migrationBuilder.DropTable(
                name: "organization_files");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "user_appraisal_history");

            migrationBuilder.DropTable(
                name: "user_assets");

            migrationBuilder.DropTable(
                name: "user_attendance");

            migrationBuilder.DropTable(
                name: "user_bank_details_secure");

            migrationBuilder.DropTable(
                name: "user_certifications");

            migrationBuilder.DropTable(
                name: "user_documents");

            migrationBuilder.DropTable(
                name: "user_education");

            migrationBuilder.DropTable(
                name: "user_employment_history");

            migrationBuilder.DropTable(
                name: "user_experience");

            migrationBuilder.DropTable(
                name: "user_health_insurance");

            migrationBuilder.DropTable(
                name: "user_hr");

            migrationBuilder.DropTable(
                name: "user_languages");

            migrationBuilder.DropTable(
                name: "user_leave_balance");

            migrationBuilder.DropTable(
                name: "user_leave_requests");

            migrationBuilder.DropTable(
                name: "user_performance_ratings");

            migrationBuilder.DropTable(
                name: "user_profile");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "user_salary");

            migrationBuilder.DropTable(
                name: "user_sensitive_identifiers");

            migrationBuilder.DropTable(
                name: "user_skills");

            migrationBuilder.DropTable(
                name: "user_training_records");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "organizations");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "core_users");
        }
    }
}
