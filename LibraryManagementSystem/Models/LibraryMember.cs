
namespace LibraryManagementSystem.Models
{
    public class LibraryMember
    {
        public int LibraryMemberId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<BorrowingRecord> BorrowingRecords { get; set; }
    }
}
