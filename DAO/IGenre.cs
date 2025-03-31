using Library.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAO
{
    interface IGenre
    {
        Task<List<Genre>> GetAllGenresAsync();
    }
}
