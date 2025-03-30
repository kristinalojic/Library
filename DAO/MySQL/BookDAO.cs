using Library.Models;
using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAO.MySQL
{
    class BookDAO : IBook
    {
        public async Task<List<Book>> GetAllBooksAsync()
        {
            using (var _db = new LibraryDbContext())
            {
                return await _db.Books.Include(b => b.Genre).ToListAsync();
            }
        }
    }
}
