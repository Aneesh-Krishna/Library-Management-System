
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int AvailableCopies { get; set; }

        public DateTime? PublicationDate { get; set; }

        public ICollection<BorrowingRecord>? BorrowingRecords { get; set; }
    }
}
