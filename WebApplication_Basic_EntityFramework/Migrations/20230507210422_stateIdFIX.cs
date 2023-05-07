using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication_Basic_EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class stateIdFIX : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_WorkItemsState_WorkItemStateId",
                table: "WorkItems");

            migrationBuilder.RenameColumn(
                name: "WorkItemStateId",
                table: "WorkItems",
                newName: "StateId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkItems_WorkItemStateId",
                table: "WorkItems",
                newName: "IX_WorkItems_StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_WorkItemsState_StateId",
                table: "WorkItems",
                column: "StateId",
                principalTable: "WorkItemsState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_WorkItemsState_StateId",
                table: "WorkItems");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "WorkItems",
                newName: "WorkItemStateId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkItems_StateId",
                table: "WorkItems",
                newName: "IX_WorkItems_WorkItemStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_WorkItemsState_WorkItemStateId",
                table: "WorkItems",
                column: "WorkItemStateId",
                principalTable: "WorkItemsState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
