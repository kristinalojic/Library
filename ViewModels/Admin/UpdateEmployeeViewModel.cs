using Employee_And_Company_Management.Commands;
using Library.DAO.MySQL;
using Library.DAO;
using Library.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using Library.Models.Entities;

namespace Library.ViewModels.Admin
{
    class UpdateEmployeeViewModel : BaseViewModel, IDataErrorInfo
    {
        private Window _currentWindow;

        private readonly IEmployee _employeeDAO;

        private readonly EmployeesViewModel _employeesViewModel;

        private Models.Entities.Employee _selecteEmployee;

        private string _name;
        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _surname;

        public string? Surname
        {
            get => _surname;
            set => SetProperty(ref _surname, value);
        }

        public string _username;

        public string? Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string? _jmbg;

        public string? JMBG
        {
            get => _jmbg;
            set => SetProperty(ref _jmbg, value);
        }

        private string? _phone;

        public string? Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        private string? _email;

        public string? Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string? _address;

        public string? Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        public ICommand CancelCommand { get; set; }
        public ICommand SubmitCommand { get; set; }

        List<Models.Entities.Employee> employees;


        public UpdateEmployeeViewModel(Window window, EmployeesViewModel employeesViewModel, Models.Entities.Employee employee)
        {
            _selecteEmployee = employee;
            _currentWindow = window;
            _employeesViewModel = employeesViewModel;
            CancelCommand = new RelayCommand(Cancel, CanCancel);
            SubmitCommand = new RelayCommand(async obj => await Submit(obj), CanSubmit);
            _employeeDAO = new EmployeeDAO();
            SetData();
            LoadEmployees();
        }

        async void LoadEmployees()
        {
            employees = await _employeeDAO.GetAllEmployeesAsync();
        }

        private void SetData()
        {
            Name = _selecteEmployee.Name;
            Surname = _selecteEmployee.Surname;
            Username = _selecteEmployee.Username;
            JMBG = _selecteEmployee.Jbmg;
            Phone = _selecteEmployee.Phone;
            Email = _selecteEmployee.Email;
            Address = _selecteEmployee.Address;
        }

        private void Cancel(object? obj)
        {
            _currentWindow.Close();
        }

        private bool CanCancel(object? obj) => true;

        private async Task Submit(object? obj)
        {
            _isValidationEnabled = true;
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Surname));
            OnPropertyChanged(nameof(Username));
            OnPropertyChanged(nameof(JMBG));
            OnPropertyChanged(nameof(Phone));
            OnPropertyChanged(nameof(Address));
            OnPropertyChanged(nameof(Email));

            if (string.IsNullOrEmpty(this[nameof(Name)]) &&
                string.IsNullOrEmpty(this[nameof(Surname)]) &&
                string.IsNullOrEmpty(this[nameof(Username)]) &&
                string.IsNullOrEmpty(this[nameof(JMBG)]) &&
                string.IsNullOrEmpty(this[nameof(Phone)]) &&
                string.IsNullOrEmpty(this[nameof(Email)]) &&
                string.IsNullOrEmpty(this[nameof(Address)])
                )
            {
                var NewEmployee = new Models.Entities.Employee
                {
                    Id = _selecteEmployee.Id,
                    Name = Name,
                    Surname = Surname,
                    Username = Username,
                    Password = Username,
                    Jbmg = JMBG,
                    Phone = Phone,
                    Email = Email,
                    Address = Address,
                    IsAcive = _selecteEmployee.IsAcive,
                    AccountType = _selecteEmployee.AccountType

                };
                if (await _employeeDAO.UpdateEmployeeAsync(NewEmployee))
                {
                    var messageBox = new CustomMessageBox("Zaposleni je azuriran.");
                    messageBox.ShowDialog();
                    await _employeesViewModel.LoadEmployeesAsync();
                }
                else
                {
                    var messageBox = new CustomMessageBox("Nije moguće azurirati zaposlenog");
                    messageBox.ShowDialog();
                }
            }
        }

        private bool CanSubmit(object? obj) => true;

        public string Error => null;

        private bool _isValidationEnabled = false;

        public string this[string columnName]
        {
            get
            {
                if (!_isValidationEnabled) return string.Empty;
                int jmbg = 0;
                return columnName switch
                {
                    nameof(Name) when string.IsNullOrWhiteSpace(Name) => "Ime ne moze biti prazno.",
                    nameof(Surname) when string.IsNullOrWhiteSpace(Surname) => "Prezime ne moze biti prazno.",
                    nameof(Username) when string.IsNullOrWhiteSpace(Username) => "Korisnicko ime ne moze biti prazno.",
                    nameof(Username) when employees.Any(e => string.Equals(e.Username, Username, StringComparison.OrdinalIgnoreCase) && e.Username != _selecteEmployee.Username) => "Korisnicko ime je zauzeto.",
                    nameof(JMBG) when string.IsNullOrWhiteSpace(JMBG) => "JMBG ne moze biti prazan.",
                    nameof(JMBG) when !int.TryParse(JMBG, out jmbg) => "JMBG nije validan.",
                    nameof(JMBG) when employees.Any(e => e.Jbmg == JMBG && e.Jbmg != _selecteEmployee.Jbmg) => "Postoji korisnik sa JMBGom.",
                    nameof(Phone) when string.IsNullOrWhiteSpace(Phone) => "Broj telefona ne moze biti prazan.",
                    nameof(Phone) when !int.TryParse(Phone, out jmbg) => "Broj telefona nije validan.",
                    nameof(Phone) when employees.Any(e => e.Phone == Phone && e.Phone != _selecteEmployee.Phone) => "Telefon postoji.",
                    nameof(Email) when string.IsNullOrWhiteSpace(Email) => "Email ne može biti prazan.",
                    nameof(Email) when employees.Any(e => string.Equals(e.Email, Email, StringComparison.OrdinalIgnoreCase) && e.Email != _selecteEmployee.Email) => "Email postoji.",
                    nameof(Address) when string.IsNullOrWhiteSpace(Address) => "Adresa ne može biti prazana.",
                    _ => string.Empty
                };
            }
        }
    }
}
