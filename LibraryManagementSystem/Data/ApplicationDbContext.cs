using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Models;
namespace LibraryManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<LibraryMember> LibraryMembers { get; set; }
        public DbSet<BorrowingRecord> BorrowingRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BorrowingRecord>()
                .HasOne(br  => br.LibraryMember)
                .WithMany(b => b.BorrowingRecords)
                .HasForeignKey(br => br.LibraryMemberId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BorrowingRecord>()
                .HasOne(br => br.Book)
                .WithMany(b => b.BorrowingRecords)
                .HasForeignKey(br => br.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BorrowingRecord>(entity =>
            {
                entity.HasKey(br => br.BorrowingRecordId);
                entity.Property(br => br.BookId).IsRequired().HasColumnName("BookId");
                entity.Property(br => br.LibraryMemberId).IsRequired().HasColumnName("Borrower");
                entity.Property(br => br.BorrowedDate).IsRequired().HasColumnName("Borrowed_Date");
                entity.Property(br => br.DueDate).IsRequired().HasColumnName("Due_On");
                entity.Property(br => br.ReturnedDate).IsRequired(false).HasColumnName("Returned_On");
            });

            modelBuilder.Entity<LibraryMember>(entity =>
            {
                entity.ToTable("Library_Members");
                entity.HasKey(lm => lm.LibraryMemberId);
                entity.Property(lm => lm.FullName).IsRequired().HasMaxLength(100).HasColumnName("Full_Name");
                entity.Property(lm => lm.Email).IsRequired().HasColumnName("Email");
                entity.Property(lm => lm.PhoneNumber).IsRequired().HasMaxLength(15).HasColumnName("Contact");
                entity.HasIndex(lm => lm.Email).IsUnique();
                entity.HasIndex(lm => lm.PhoneNumber).IsUnique();
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Books");
                entity.HasKey(b => b.BookId);
                entity.Property(b => b.Title).IsRequired().HasMaxLength(50).HasColumnName("Title");
                entity.Property(b => b.Author).IsRequired().HasMaxLength(100).HasColumnName("Author");
                entity.Property(b => b.AvailableCopies).IsRequired().HasColumnName("Copies_Available");
                entity.Property(b => b.PublicationDate).IsRequired(false);
                entity.HasIndex(b => b.Title).IsUnique();
            });

            base.OnModelCreating(modelBuilder);

        }
    }
}
