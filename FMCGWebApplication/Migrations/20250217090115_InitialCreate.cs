using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMCGWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TISCompanyMst",
                columns: table => new
                {
                    CompanyCode = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_STD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone_Off = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone_Resi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LST_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CST_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PanNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Www = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusiType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LSTTinno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CSTTinno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EInvAuthExpTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TISCompanyMst", x => x.CompanyCode);
                });

            migrationBuilder.CreateTable(
                name: "TISAreaMst",
                columns: table => new
                {
                    CompanyCode = table.Column<short>(type: "smallint", nullable: false),
                    AreaCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    AreaName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AreaGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EditLock = table.Column<bool>(type: "bit", nullable: false),
                    Machine = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PID = table.Column<int>(type: "int", nullable: true),
                    hWnd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TISAreaMst", x => new { x.AreaCode, x.CompanyCode });
                    table.ForeignKey(
                        name: "FK_TISAreaMst_TISCompanyMst_CompanyCode",
                        column: x => x.CompanyCode,
                        principalTable: "TISCompanyMst",
                        principalColumn: "CompanyCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TISAreaMst_CompanyCode",
                table: "TISAreaMst",
                column: "CompanyCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TISAreaMst");

            migrationBuilder.DropTable(
                name: "TISCompanyMst");
        }
    }
}
