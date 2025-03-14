using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Office_supplies_management.Migrations
{
    /// <inheritdoc />
    public partial class completeModelSummary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SummaryID",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_SummaryID",
                table: "Requests",
                column: "SummaryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Summaries_SummaryID",
                table: "Requests",
                column: "SummaryID",
                principalTable: "Summaries",
                principalColumn: "SummaryID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Summaries_SummaryID",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_SummaryID",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "SummaryID",
                table: "Requests");
        }
    }
}
