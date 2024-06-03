using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFirstApproachCRUD.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDEpt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeptHead",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeptHead",
                table: "Departments");
        }
    }
}
