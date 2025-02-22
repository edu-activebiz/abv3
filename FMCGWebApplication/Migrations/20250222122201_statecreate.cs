using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMCGWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class statecreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TISState",
                schema: "dbo",
                columns: table => new
                {
                    StateName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TISState", x => x.StateName);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TISState",
                schema: "dbo");
        }
    }
}
