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
using System.Windows.Input;
using Employee_And_Company_Management.Commands;
using Library.Views.Windows.Admin;
using Library.Views.Windows;

namespace Library.ViewModels.Admin
{
    public class EmployeesViewModel : BaseViewModel
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

        private ObservableCollection<Models.Entities.Employee> _filteredActiveEmployees;
        public ObservableCollection<Models.Entities.Employee> FilteredActiveEmployees
        {
            get => _filteredActiveEmployees;
            set
            {
                if (SetProperty(ref _filteredActiveEmployees, value))
                {
                    _filteredActiveEmployees.CollectionChanged += (s, e) => OnPropertyChanged(nameof(ActiveEmployeesCount));
                }
            }
        }

        private ObservableCollection<Models.Entities.Employee> _filteredInactiveEmployees;
        public ObservableCollection<Models.Entities.Employee> FilteredInactiveEmployees
        {
            get => _filteredInactiveEmployees;
            set
            {
                if (SetProperty(ref _filteredInactiveEmployees, value))
                {
                    _filteredInactiveEmployees.CollectionChanged += (s, e) => OnPropertyChanged(nameof(InactiveEmployeesCount));
                }
            }
        }

        public int ActiveEmployeesCount => FilteredActiveEmployees?.Count ?? 0;

        public int InactiveEmployeesCount => FilteredInactiveEmployees?.Count ?? 0;

        private Models.Entities.Employee _selectedEmployee;
        public Models.Entities.Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set => SetProperty(ref _selectedEmployee, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    SetProperty(ref _searchText, value);
                    FilterEmployees();
                }
            }
        }

        private string _searchText2;
        public string SearchText2
        {
            get => _searchText2;
            set
            {
                if (_searchText2 != value)
                {
                    SetProperty(ref _searchText2, value);
                    FilterEmployees();
                }
            }
        }

        public ICommand AddEmployeeCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ActivateCommand { get; set; }
        public ICommand UpdateEmployeeCommand { get; set; }

        private EmployeesViewModel()
        {
            _employeeDAO = new EmployeeDAO();
            AddEmployeeCommand = new RelayCommand(AddEmployee, CanAddEmployee);
            DeleteCommand = new RelayCommand(async obj => await DeleteSelectedEmployee(obj), CanDeleteSelectedEmployee);
            ActivateCommand = new RelayCommand(async obj => await ActivateSelectedEmployee(obj), CanActivateSelectedEmployee);
            UpdateEmployeeCommand = new RelayCommand(UpdateSelectedEmployee, CanUpdateSelectedEmployee);
        }

        public static async Task<EmployeesViewModel> CreateAsync(int id)
        {
            var settings = new SettingsDAO();
            var setting = await settings.GetSettingByIdAsync(id);
            if (setting != null)
            {
                ChangeLanguage(setting.Language);
            }
            var viewModel = new EmployeesViewModel();
            await viewModel.LoadEmployeesAsync();
            return viewModel;
        }

        public async Task LoadEmployeesAsync()
        {
            var employees = await _employeeDAO.GetAllEmployeesAsync();
            ActiveEmployees = new ObservableCollection<Models.Entities.Employee>(employees.Where(e => e.IsAcive && e.AccountType == 2));
            InactiveEmployees = new ObservableCollection<Models.Entities.Employee>(employees.Where(e => !e.IsAcive && e.AccountType == 2));
            FilterEmployees();
        }

        private void AddEmployee(object? obj)
        {
            AddEmployeeWindow window = new AddEmployeeWindow(this);
            window.Show();
        }

        private bool CanAddEmployee(object? obj) => true;

        private void UpdateSelectedEmployee(object? obj)
        {
            var _pomEmployee = SelectedEmployee;
            if (_pomEmployee != null)
            {
                UpdateEmployeeWindow window = new UpdateEmployeeWindow(this, _pomEmployee);
                window.Show();
            }
            else
            {
                var messageBox = new CustomMessageBox(TryGetResource("SelectEmployee"));
                messageBox.ShowDialog();
            }
        }

        private bool CanUpdateSelectedEmployee(object? obj) => true;

        private bool CanDeleteSelectedEmployee(object parameter) => true;

        private async Task DeleteSelectedEmployee(object parameter)
        {
            var _pomEmployee = SelectedEmployee;
            if (_pomEmployee != null)
            {
                DeleteConfirmationDialog dialog = new DeleteConfirmationDialog(TryGetResource("RemoveEmployeeConfirmation"));
                bool answer = dialog.ShowDialog() ?? false;

                if (answer)
                {
                    _pomEmployee.IsAcive = false;
                    ActiveEmployees.Remove(_pomEmployee);
                    InactiveEmployees.Add(_pomEmployee);

                    await _employeeDAO.UpdateEmployeeAsync(_pomEmployee);
                    FilterEmployees();
                }
            }
            else
            {
                var messageBox = new CustomMessageBox(TryGetResource("SelectEmployee"));
                messageBox.ShowDialog();
            }
        }

        private bool CanActivateSelectedEmployee(object parameter)
        {
            return true;
        }

        private async Task ActivateSelectedEmployee(object parameter)
        {
            var _pomEmployee = SelectedEmployee;
            if (_pomEmployee != null)
            {
                DeleteConfirmationDialog dialog = new DeleteConfirmationDialog(TryGetResource("ActivateEmployeeConfirmation"));
                bool answer = dialog.ShowDialog() ?? false;

                if (answer)
                {
                    _pomEmployee.IsAcive = true;
                    ActiveEmployees.Add(_pomEmployee);
                    InactiveEmployees.Remove(_pomEmployee);

                    await _employeeDAO.UpdateEmployeeAsync(_pomEmployee);
                    FilterEmployees();
                }
            }
            else
            {
                var messageBox = new CustomMessageBox(TryGetResource("SelectEmployee"));
                messageBox.ShowDialog();
            }
        }

        private void FilterEmployees()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredActiveEmployees = new ObservableCollection<Models.Entities.Employee>(_activeEmployees);
            }
            else
            {
                var searchParts = SearchText?.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (searchParts?.Length == 2)
                {
                    string firstName = searchParts[0];
                    string lastName = searchParts[1];

                    FilteredActiveEmployees = new ObservableCollection<Models.Entities.Employee>(
                        _activeEmployees.Where(e =>
                            e.Name.StartsWith(firstName, StringComparison.OrdinalIgnoreCase) &&
                            e.Surname.StartsWith(lastName, StringComparison.OrdinalIgnoreCase)));
                }
                else
                {
                    FilteredActiveEmployees = new ObservableCollection<Models.Entities.Employee>(
                        _activeEmployees.Where(e =>
                            e.Name.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase) ||
                            e.Surname.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase)));
                }
            }
            if (string.IsNullOrWhiteSpace(SearchText2))
            {
                FilteredInactiveEmployees = new ObservableCollection<Models.Entities.Employee>(_inactiveEmployees);
            }
            else
            {
                var searchParts = SearchText2?.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (searchParts?.Length == 2)
                {
                    string firstName = searchParts[0];
                    string lastName = searchParts[1];

                    FilteredInactiveEmployees = new ObservableCollection<Models.Entities.Employee>(
                        _inactiveEmployees.Where(e =>
                            e.Name.StartsWith(firstName, StringComparison.OrdinalIgnoreCase) &&
                            e.Surname.StartsWith(lastName, StringComparison.OrdinalIgnoreCase)));
                }
                else
                {
                    FilteredInactiveEmployees = new ObservableCollection<Models.Entities.Employee>(
                        _inactiveEmployees.Where(e =>
                            e.Name.StartsWith(SearchText2, StringComparison.OrdinalIgnoreCase) ||
                            e.Surname.StartsWith(SearchText2, StringComparison.OrdinalIgnoreCase)));
                }
            }
        }
    }
}
