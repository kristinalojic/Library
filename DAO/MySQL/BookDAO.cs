using Library.Models;
using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library.DAO.MySQL
{
    class BookDAO : IBook
    {
        public async Task<bool> AddBookAsync(Book book)
        {
            using (var _db = new LibraryDbContext())
            {
                if (_db.Books.Any(b => b.Title == book.Title && b.Author == book.Author))
                    return false;
                _db.Books.Add(book);
                int affectedRows = await _db.SaveChangesAsync();

                return affectedRows > 0;
            }
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            using (var _db = new LibraryDbContext())
            {
                return await _db.Books.Include(b => b.Genre).ToListAsync();
            }
        }

        public async Task<bool> UpdateBookAsync(Book b)
        {
            using (var _context = new LibraryDbContext())
            {
                try
                {
                    var book = await _context.Books.FindAsync(b.Id);

                    if (book != null)
                    {
                        book.IsAvailable = b.IsAvailable;
                        book.Author = b.Author;
                        book.Title = b.Title;
                        book.YearOfPublication = b.YearOfPublication;
                        book.Copies = b.Copies;
                        book.GenreId = b.GenreId;
                        book.AvailableCopies = b.AvailableCopies;
                        int affectedRows = await _context.SaveChangesAsync();
                        return affectedRows > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Došlo je do greške: {ex.Message}");
                }
                return false;
            }
        }
    }
}
