using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication_Basic_EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class addtionalAddDateManual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WorkItemsState",
                columns: new[] { "State" },
                values: new object[,]
                {
                    {   "On Hold" },
                    {   "Rejected" } 
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkItemsState",
                keyColumn: "State",
                keyValue: "On Hold", "Rejected");
        }
    }
}
