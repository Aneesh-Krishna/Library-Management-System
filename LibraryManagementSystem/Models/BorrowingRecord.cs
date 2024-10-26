
namespace LibraryManagementSystem.Models
{
    public class BorrowingRecord
    {
        public int BorrowingRecordId { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public int BookId { get; set; }
        public int LibraryMemberId { get; set; }
        public Book Book { get; set; }
        public LibraryMember LibraryMember { get; set; }
    }
}
