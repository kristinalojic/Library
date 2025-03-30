using Library.DAO;
using Library.DAO.MySQL;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models.Entities;

namespace Library.ViewModels.Admin
{
    class EmployeesViewModel : BaseViewModel
    {
        private readonly IEmployee _employeeDAO;

        private ObservableCollection<Models.Entities.Employee> _activeEmployees;
        public ObservableCollection<Models.Entities.Employee> ActiveEmployees
        {
            get => _activeEmployees;
            set
            {
                if (SetProperty(ref _activeEmployees, value))
                {
                    OnPropertyChanged(nameof(ActiveEmployeesCount));
                    _activeEmployees.CollectionChanged += (s, e) => OnPropertyChanged(nameof(ActiveEmployeesCount));
                }
            }
        }

        private ObservableCollection<Models.Entities.Employee> _inactiveEmployees;
        public ObservableCollection<Models.Entities.Employee> InactiveEmployees
        {
            get => _inactiveEmployees;
            set
            {
                if (SetProperty(ref _inactiveEmployees, value))
                {
                    OnPropertyChanged(nameof(InactiveEmployeesCount));
                    _inactiveEmployees.CollectionChanged += (s, e) => OnPropertyChanged(nameof(InactiveEmployeesCount));
                }
            }
        }

        public int ActiveEmployeesCount => ActiveEmployees?.Count ?? 0;

        public int InactiveEmployeesCount => InactiveEmployees?.Count ?? 0;

        public EmployeesViewModel()
        {
            _employeeDAO = new EmployeeDAO();
            LoadEmployees();
        }

        private async void LoadEmployees()
        {
            var employees = await _employeeDAO.GetAllEmployeesAsync();
            ActiveEmployees = new ObservableCollection<Models.Entities.Employee>(employees.Where(e => e.IsAcive && e.AccountType == 2));
            InactiveEmployees = new ObservableCollection<Models.Entities.Employee>(employees.Where(e => !e.IsAcive && e.AccountType == 2));
        }
    }
}
