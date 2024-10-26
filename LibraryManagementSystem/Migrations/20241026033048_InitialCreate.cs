using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Copies_Available = table.Column<int>(type: "int", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "Library_Members",
                columns: table => new
                {
                    LibraryMemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Full_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Library_Members", x => x.LibraryMemberId);
                });

            migrationBuilder.CreateTable(
                name: "BorrowingRecords",
                columns: table => new
                {
                    BorrowingRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Borrowed_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Due_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Returned_On = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Borrower = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowingRecords", x => x.BorrowingRecordId);
                    table.ForeignKey(
                        name: "FK_BorrowingRecords_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BorrowingRecords_Library_Members_Borrower",
                        column: x => x.Borrower,
                        principalTable: "Library_Members",
                        principalColumn: "LibraryMemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title",
                table: "Books",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowingRecords_BookId",
                table: "BorrowingRecords",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowingRecords_Borrower",
                table: "BorrowingRecords",
                column: "Borrower");

            migrationBuilder.CreateIndex(
                name: "IX_Library_Members_Contact",
                table: "Library_Members",
                column: "Contact",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Library_Members_Email",
                table: "Library_Members",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowingRecords");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Library_Members");
        }
    }
}
