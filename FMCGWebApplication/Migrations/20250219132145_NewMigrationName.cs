using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMCGWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TISAreaMst_TISCompanyMst_CompanyCode",
                table: "TISAreaMst");

            migrationBuilder.DropIndex(
                name: "IX_TISAreaMst_CompanyCode",
                table: "TISAreaMst");

            migrationBuilder.EnsureSchema(
                name: "dbo");

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
                name: "TISGroupMst",
                schema: "dbo");

            migrationBuilder.CreateIndex(
                name: "IX_TISAreaMst_CompanyCode",
                table: "TISAreaMst",
                column: "CompanyCode");

            migrationBuilder.AddForeignKey(
                name: "FK_TISAreaMst_TISCompanyMst_CompanyCode",
                table: "TISAreaMst",
                column: "CompanyCode",
                principalTable: "TISCompanyMst",
                principalColumn: "CompanyCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
