using System;
using System.Collections.Generic;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public string Category { get; set; }
    public bool IsBorrowed { get; set; }
    public int? BorrowerId { get; set; }
  
    public DateTime? DueDate { get; set; }
}
