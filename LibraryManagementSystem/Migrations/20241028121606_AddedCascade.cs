using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddedCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowingRecords_Books_BookId",
                table: "BorrowingRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowingRecords_Library_Members_Borrower",
                table: "BorrowingRecords");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowingRecords_Books_BookId",
                table: "BorrowingRecords",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowingRecords_Library_Members_Borrower",
                table: "BorrowingRecords",
                column: "Borrower",
                principalTable: "Library_Members",
                principalColumn: "LibraryMemberId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowingRecords_Books_BookId",
                table: "BorrowingRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowingRecords_Library_Members_Borrower",
                table: "BorrowingRecords");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowingRecords_Books_BookId",
                table: "BorrowingRecords",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowingRecords_Library_Members_Borrower",
                table: "BorrowingRecords",
                column: "Borrower",
                principalTable: "Library_Members",
                principalColumn: "LibraryMemberId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
