using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Office_supplies_management.Migrations
{
    /// <inheritdoc />
    public partial class updateforauthentication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "User",
                newName: "UserTypeID");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionID);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    UserTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.UserTypeID);
                });

            migrationBuilder.CreateTable(
                name: "PermissionUserType",
                columns: table => new
                {
                    PermissionsPermissionID = table.Column<int>(type: "int", nullable: false),
                    UserTypesUserTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionUserType", x => new { x.PermissionsPermissionID, x.UserTypesUserTypeID });
                    table.ForeignKey(
                        name: "FK_PermissionUserType_Permissions_PermissionsPermissionID",
                        column: x => x.PermissionsPermissionID,
                        principalTable: "Permissions",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionUserType_UserTypes_UserTypesUserTypeID",
                        column: x => x.UserTypesUserTypeID,
                        principalTable: "UserTypes",
                        principalColumn: "UserTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes_Permissions",
                columns: table => new
                {
                    UserType_PermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTypeID = table.Column<int>(type: "int", nullable: false),
                    PermissionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes_Permissions", x => x.UserType_PermissionID);
                    table.ForeignKey(
                        name: "FK_UserTypes_Permissions_Permissions_PermissionID",
                        column: x => x.PermissionID,
                        principalTable: "Permissions",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTypes_Permissions_UserTypes_UserTypeID",
                        column: x => x.UserTypeID,
                        principalTable: "UserTypes",
                        principalColumn: "UserTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_UserTypeID",
                table: "User",
                column: "UserTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionUserType_UserTypesUserTypeID",
                table: "PermissionUserType",
                column: "UserTypesUserTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTypes_Permissions_PermissionID",
                table: "UserTypes_Permissions",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTypes_Permissions_UserTypeID",
                table: "UserTypes_Permissions",
                column: "UserTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserTypes_UserTypeID",
                table: "User",
                column: "UserTypeID",
                principalTable: "UserTypes",
                principalColumn: "UserTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_UserTypes_UserTypeID",
                table: "User");

            migrationBuilder.DropTable(
                name: "PermissionUserType");

            migrationBuilder.DropTable(
                name: "UserTypes_Permissions");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "UserTypes");

            migrationBuilder.DropIndex(
                name: "IX_User_UserTypeID",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "UserTypeID",
                table: "User",
                newName: "UserType");
        }
    }
}
