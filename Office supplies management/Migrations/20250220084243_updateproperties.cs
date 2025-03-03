using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Office_supplies_management.Migrations
{
    /// <inheritdoc />
    public partial class updateproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Product_Requests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Product_Requests");
        }
    }
}
