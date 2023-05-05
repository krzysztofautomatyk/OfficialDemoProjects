using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication_Basic_EntityFramework.CustomMigrations
{
    public class Create_Copy_Delete_Columns_with_Data
    {

        /// <inheritdoc />
        public partial class mod1 : Migration
        {
            /// <inheritdoc />
            protected override void Up(MigrationBuilder migrationBuilder)
            {

                // Add new column
                migrationBuilder.AddColumn<string>(
                    name: "FullName",
                    table: "Users",
                    type: "nvarchar(max)",
                    nullable: true);
                    
                // Get data from exists columns
                migrationBuilder.Sql("UPDATE Users SET FullName = FirstName + ' ' + LastName");
            
                // Drop exists columns
                migrationBuilder.DropColumn(                    
                    name: "FirstName",
                    table: "Users");

                // Drop exists columns
                migrationBuilder.DropColumn(                    
                    name: "LastName",
                    table: "Users");

            }

            /// <inheritdoc />
            protected override void Down(MigrationBuilder migrationBuilder)
            {

                // Add new column
                migrationBuilder.AddColumn<string>(
                    name: "LastName",
                    table: "Users",
                    type: "nvarchar(max)",
                    nullable: true);

                // Add new column
                migrationBuilder.AddColumn<string>(
                    name: "FirstName",
                    table: "Users",
                    type: "nvarchar(max)",
                    nullable: true);

                // update data

           
                // Drop exists columns
                migrationBuilder.DropColumn(
                    name: "FullName",
                    table: "Users");
            }
        }
    }
}
