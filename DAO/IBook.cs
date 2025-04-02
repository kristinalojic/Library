using Library.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAO
{
    interface IBook
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<bool> AddBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book);
    }
}
