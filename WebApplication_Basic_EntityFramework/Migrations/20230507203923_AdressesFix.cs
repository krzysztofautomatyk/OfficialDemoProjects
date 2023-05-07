using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication_Basic_EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AdressesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Addresses",
                newName: "PostalCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Addresses",
                newName: "ZipCode");
        }
    }
}
