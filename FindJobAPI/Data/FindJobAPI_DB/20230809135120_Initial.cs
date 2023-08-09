using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindJobAPI.Data.FindJobAPI_DB
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    admin_id = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => new { x.admin_id, x.username });
                });

            migrationBuilder.CreateTable(
                name: "Industry",
                columns: table => new
                {
                    industry_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    industry_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industry", x => x.industry_id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.type_id);
                });

            migrationBuilder.CreateTable(
                name: "Job_Industry",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    industry_id = table.Column<int>(type: "int", nullable: false),
                    job = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job_Industry", x => x.id);
                    table.ForeignKey(
                        name: "FK_Job_Industry_Industry_industry_id",
                        column: x => x.industry_id,
                        principalTable: "Industry",
                        principalColumn: "industry_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date_create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.email);
                    table.ForeignKey(
                        name: "FK_Account_Role_role_id",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employer",
                columns: table => new
                {
                    employer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    employer_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employer_about = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employer_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employer_image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employer_website = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employer", x => x.employer_id);
                    table.ForeignKey(
                        name: "FK_Employer_Account_email",
                        column: x => x.email,
                        principalTable: "Account",
                        principalColumn: "email");
                });

            migrationBuilder.CreateTable(
                name: "Seeker",
                columns: table => new
                {
                    seeker_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    seeker_image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    academic_level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    skills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url_cv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    website_seeker = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seeker", x => x.seeker_id);
                    table.ForeignKey(
                        name: "FK_Seeker_Account_email",
                        column: x => x.email,
                        principalTable: "Account",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    job_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employer_id = table.Column<int>(type: "int", nullable: false),
                    type_id = table.Column<int>(type: "int", nullable: false),
                    posted_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deadline = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.job_id);
                    table.ForeignKey(
                        name: "FK_Job_Employer_employer_id",
                        column: x => x.employer_id,
                        principalTable: "Employer",
                        principalColumn: "employer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_Type_type_id",
                        column: x => x.type_id,
                        principalTable: "Type",
                        principalColumn: "type_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Job_Detail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    job_id = table.Column<int>(type: "int", nullable: false),
                    job_title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    job_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    requirement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    minimum_salary = table.Column<float>(type: "real", nullable: false),
                    maximum_salary = table.Column<float>(type: "real", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    industry_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job_Detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Job_Detail_Industry_industry_id",
                        column: x => x.industry_id,
                        principalTable: "Industry",
                        principalColumn: "industry_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_Detail_Job_job_id",
                        column: x => x.job_id,
                        principalTable: "Job",
                        principalColumn: "job_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recruitment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    seeker_id = table.Column<int>(type: "int", nullable: false),
                    job_id = table.Column<int>(type: "int", nullable: false),
                    seeker_desire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    registation_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruitment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recruitment_Job_job_id",
                        column: x => x.job_id,
                        principalTable: "Job",
                        principalColumn: "job_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recruitment_Seeker_seeker_id",
                        column: x => x.seeker_id,
                        principalTable: "Seeker",
                        principalColumn: "seeker_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_role_id",
                table: "Account",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employer_email",
                table: "Employer",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_Job_employer_id",
                table: "Job",
                column: "employer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Job_type_id",
                table: "Job",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Detail_industry_id",
                table: "Job_Detail",
                column: "industry_id");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Detail_job_id",
                table: "Job_Detail",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Industry_industry_id",
                table: "Job_Industry",
                column: "industry_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_job_id",
                table: "Recruitment",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_seeker_id",
                table: "Recruitment",
                column: "seeker_id");

            migrationBuilder.CreateIndex(
                name: "IX_Seeker_email",
                table: "Seeker",
                column: "email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Job_Detail");

            migrationBuilder.DropTable(
                name: "Job_Industry");

            migrationBuilder.DropTable(
                name: "Recruitment");

            migrationBuilder.DropTable(
                name: "Industry");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Seeker");

            migrationBuilder.DropTable(
                name: "Employer");

            migrationBuilder.DropTable(
                name: "Type");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
