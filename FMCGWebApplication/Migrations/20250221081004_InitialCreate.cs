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
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "TISAccountMst",
                schema: "dbo",
                columns: table => new
                {
                    CompanyCode = table.Column<short>(type: "smallint", nullable: false),
                    AcCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    GroupCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    AcName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PTG = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    AcType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    TrnType = table.Column<byte>(type: "tinyint", nullable: true),
                    CreditAbbrv = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DebitAbbrv = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ARAPStatus = table.Column<byte>(type: "tinyint", nullable: true),
                    AreaCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    OGS = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone_STD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone_Off = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone_Resi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mobil = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FaxNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EMail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WWW = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LST_No = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CST_No = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PANNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LicNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CrDays = table.Column<short>(type: "smallint", nullable: true),
                    CrAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EditLock = table.Column<bool>(type: "bit", nullable: false),
                    Machine = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PID = table.Column<int>(type: "int", nullable: true),
                    hWnd = table.Column<int>(type: "int", nullable: true),
                    BillType = table.Column<byte>(type: "tinyint", nullable: true),
                    LSTTINNO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CSTTINNO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GSTNO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdharCardNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TISAccountMst", x => new { x.CompanyCode, x.GroupCode, x.AcCode });
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
                    table.PrimaryKey("PK_TISAreaMst", x => new { x.CompanyCode, x.AreaCode });
                });

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
                name: "TISGroupMst",
                schema: "dbo",
                columns: table => new
                {
                    CompanyCode = table.Column<short>(type: "smallint", nullable: false),
                    GroupCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CrSeq = table.Column<byte>(type: "tinyint", nullable: true),
                    DrSeq = table.Column<byte>(type: "tinyint", nullable: true),
                    PTG = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    EditLock = table.Column<bool>(type: "bit", nullable: false),
                    Machine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PID = table.Column<int>(type: "int", nullable: true),
                    hWnd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TISGroupMst", x => new { x.CompanyCode, x.GroupCode });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TISAccountMst",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TISAreaMst");

            migrationBuilder.DropTable(
                name: "TISCompanyMst");

            migrationBuilder.DropTable(
                name: "TISGroupMst",
                schema: "dbo");
        }
    }
}
