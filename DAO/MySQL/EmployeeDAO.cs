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

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            using (var _db = new LibraryDbContext())
            {
                return await _db.Employees.ToListAsync();
            }
        }

        public async Task<bool> UpdateEmployeeAsync(Employee e)
        {
            using (var _context = new LibraryDbContext())
            {
                try
                {
                    var employee = await _context.Employees.FindAsync(e.Id);

                    if (employee != null)
                    {
                        if (_context.Employees.Any(em => (em.Username == e.Username && em.Id != e.Id) || (em.Jbmg == e.Jbmg && em.Id != e.Id) 
                        || (em.Phone == e.Phone && em.Id != employee.Id) || (em.Email == e.Email && em.Id != employee.Id)))
                            return false;
                        employee.Name = e.Name;
                        employee.Surname = e.Surname;
                        employee.Username = e.Username;
                        employee.Password = e.Password;
                        employee.IsAcive = e.IsAcive;
                        employee.AccountType = e.AccountType;
                        employee.Jbmg = e.Jbmg;
                        employee.Phone = e.Phone;
                        employee.Email = e.Email;
                        employee.Address = e.Address;
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

        public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            using (var _db = new LibraryDbContext())
            {
                if (_db.Employees.Any(e => e.Username == employee.Username || e.Jbmg == employee.Jbmg || e.Phone == employee.Phone || e.Email == employee.Email))
                    return false;
                _db.Employees.Add(employee);
                int affectedRows = await _db.SaveChangesAsync();

                return affectedRows > 0;
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
