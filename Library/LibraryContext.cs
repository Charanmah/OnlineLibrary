using Microsoft.EntityFrameworkCore;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }
    public DbSet<Borrower> Borrowers { get; set; }
    public DbSet<BorrowingHistory> BorrowingHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BorrowingHistory>()
            .HasOne(b => b.Book)
            .WithMany()
            .HasForeignKey(bh => bh.BookId);
    }
}
