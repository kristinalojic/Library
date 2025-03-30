using System;
using System.Collections.Generic;

namespace Library.Models.Entities;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public int YearOfPublication { get; set; }

    public sbyte Copies { get; set; }

    public sbyte AvailableCopies { get; set; }

    public bool IsAvailable { get; set; }

    public int GenreId { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
