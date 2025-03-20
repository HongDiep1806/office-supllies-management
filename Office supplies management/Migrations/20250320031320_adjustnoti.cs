using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Office_supplies_management.Migrations
{
    /// <inheritdoc />
    public partial class adjustnoti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add new columns to the Request table
            migrationBuilder.AddColumn<DateTime>(
                name: "DateDepLeadApprove",
                table: "Requests",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<string>(
                name: "NoteDepLead",
                table: "Requests",
                nullable: false,
                defaultValue: "note");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSupLeadApprove",
                table: "Requests",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<string>(
                name: "NoteSupLead",
                table: "Requests",
                nullable: false,
                defaultValue: "note");

            // Add new columns to the Notification table
            migrationBuilder.AddColumn<int>(
                name: "RequestID",
                table: "Notifications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sender",
                table: "Notifications",
                nullable: false,
                defaultValue: 0);

            // Add new columns to the Product table
            migrationBuilder.AddColumn<int>(
                name: "UserIDCreate",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Products",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<int>(
                name: "UserIDAdjust",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AdjustDate",
                table: "Products",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove new columns from the Request table
            migrationBuilder.DropColumn(name: "DateDepLeadApprove", table: "Requests");
            migrationBuilder.DropColumn(name: "NoteDepLead", table: "Requests");
            migrationBuilder.DropColumn(name: "DateSupLeadApprove", table: "Requests");
            migrationBuilder.DropColumn(name: "NoteSupLead", table: "Requests");

            // Remove new columns from the Notification table
            migrationBuilder.DropColumn(name: "RequestID", table: "Notifications");
            migrationBuilder.DropColumn(name: "Sender", table: "Notifications");

            // Remove new columns from the Product table
            migrationBuilder.DropColumn(name: "UserIDCreate", table: "Products");
            migrationBuilder.DropColumn(name: "CreateDate", table: "Products");
            migrationBuilder.DropColumn(name: "UserIDAdjust", table: "Products");
            migrationBuilder.DropColumn(name: "AdjustDate", table: "Products");
        }
    }
}
