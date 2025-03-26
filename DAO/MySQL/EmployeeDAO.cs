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
    class EmployeeDAO : IEmployee
    {
        public async Task<Employee?> FindByUsernameAndPasswordAsync(string username, string password)
        {
            using (var _db = new LibraryDbContext())
            {
                var employee = await _db.Employees
                                        .FirstOrDefaultAsync(e => e.Username == username && e.Password == password);
                return employee;
            }
        }

        public async Task PreloadDatabase()
        {
            using (var _db = new LibraryDbContext())
            {
                await _db.Employees.FirstOrDefaultAsync(); 
            }
        }
    }
}
