using Library.Models.Entities;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library.DAO.MySQL
{
    class MemberDAO : IMember
    {
        public async Task<List<Member>> GetAllMembersAsync()
        {
            using (var _db = new LibraryDbContext())
            {
                return await _db.Members.Include(m => m.MembershipFee).ToListAsync();
            }
        }
    }
}
