using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoWebApi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "m_account",
                columns: table => new
                {
                    EMP_NO = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    USR_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PASSWORD = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CRE_DT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPD_DT = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_account", x => x.EMP_NO);
                });

            migrationBuilder.CreateTable(
                name: "m_app_roles",
                columns: table => new
                {
                    ROLE_ID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CRE_DT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPD_DT = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_app_roles", x => x.ROLE_ID);
                });

            migrationBuilder.CreateTable(
                name: "m_company",
                columns: table => new
                {
                    CPN_ID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    CPN_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CRE_DT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPD_DT = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_company", x => x.CPN_ID);
                });

            migrationBuilder.CreateTable(
                name: "m_emp_role_mappings",
                columns: table => new
                {
                    EMP_NO = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ROLE_ID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CRE_DT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPD_DT = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_emp_role_mappings", x => new { x.EMP_NO, x.ROLE_ID });
                });

            migrationBuilder.CreateTable(
                name: "m_employees",
                columns: table => new
                {
                    EMP_NO = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    USR_NAME = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    USR_NAME_KATA = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    SEX = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    DATE_OF_BIRTH = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HIRE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CPN_ID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    POST_CD = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    RETIED = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    CRE_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UPD_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CRE_DT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPD_DT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IMAGE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_employees", x => x.EMP_NO);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_account");

            migrationBuilder.DropTable(
                name: "m_app_roles");

            migrationBuilder.DropTable(
                name: "m_company");

            migrationBuilder.DropTable(
                name: "m_emp_role_mappings");

            migrationBuilder.DropTable(
                name: "m_employees");
        }
    }
}
