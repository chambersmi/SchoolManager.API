using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManager.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedOnDeleteToModelBuilder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAddresses_Addresses_AddressesAddressID",
                table: "StudentAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAddresses_Students_StudentsStudentID",
                table: "StudentAddresses");

            migrationBuilder.RenameColumn(
                name: "StudentsStudentID",
                table: "StudentAddresses",
                newName: "StudentID");

            migrationBuilder.RenameColumn(
                name: "AddressesAddressID",
                table: "StudentAddresses",
                newName: "AddressID");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAddresses_StudentsStudentID",
                table: "StudentAddresses",
                newName: "IX_StudentAddresses_StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAddresses_Addresses_AddressID",
                table: "StudentAddresses",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAddresses_Students_StudentID",
                table: "StudentAddresses",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAddresses_Addresses_AddressID",
                table: "StudentAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAddresses_Students_StudentID",
                table: "StudentAddresses");

            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "StudentAddresses",
                newName: "StudentsStudentID");

            migrationBuilder.RenameColumn(
                name: "AddressID",
                table: "StudentAddresses",
                newName: "AddressesAddressID");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAddresses_StudentID",
                table: "StudentAddresses",
                newName: "IX_StudentAddresses_StudentsStudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAddresses_Addresses_AddressesAddressID",
                table: "StudentAddresses",
                column: "AddressesAddressID",
                principalTable: "Addresses",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAddresses_Students_StudentsStudentID",
                table: "StudentAddresses",
                column: "StudentsStudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
