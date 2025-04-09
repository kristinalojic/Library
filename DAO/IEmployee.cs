using Library.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAO
{
    interface IEmployee
    {
        Task<Employee?> FindByUsernameAndPasswordAsync(string username, string password);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<bool> UpdateEmployeeAsync(Employee employee);
        Task<bool> AddEmployeeAsync(Employee employee);
    }
}
