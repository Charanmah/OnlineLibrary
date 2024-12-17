using System.Collections.Generic;

public class Borrower
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<BorrowingHistory> BorrowingHistory { get; set; } = new List<BorrowingHistory>();
}

public class BorrowingHistory
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public DateTime BorrowedDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
}
