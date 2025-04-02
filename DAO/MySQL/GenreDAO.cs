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
    class GenreDAO : IGenre
    {
        public async Task<List<Genre>> GetAllGenresAsync()
        {
            using (var _db = new LibraryDbContext())
            {
                return await _db.Genres.ToListAsync();
            }
        }

        public async Task<int> GetIdByNameAsync(string name)
        {
            using (var _db = new LibraryDbContext())
            {
                var genre =  await _db.Genres.FirstOrDefaultAsync(g => g.Name == name);
                return genre != null ? genre.Id :0;
            }
        }
    }
}
