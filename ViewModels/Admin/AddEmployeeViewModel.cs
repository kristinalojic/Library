using Employee_And_Company_Management.Commands;
using Library.DAO.MySQL;
using Library.DAO;
using Library.Models.Entities;
using Library.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;

namespace Library.ViewModels.Admin
{
    class AddEmployeeViewModel : BaseViewModel, IDataErrorInfo
    {
        private Window _currentWindow;

        private readonly IEmployee _employeeDAO;

        private readonly EmployeesViewModel _employeesViewModel;

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

        public AddEmployeeViewModel(Window window, EmployeesViewModel employeesViewModel)
        {
            _currentWindow = window;
            _employeesViewModel = employeesViewModel;
            CancelCommand = new RelayCommand(Cancel, CanCancel);
            SubmitCommand = new RelayCommand(async obj => await Submit(obj), CanSubmit);
            _employeeDAO = new EmployeeDAO();
            LoadEmployees();
        }

        async void LoadEmployees()
        {
            employees = await _employeeDAO.GetAllEmployeesAsync();
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
                    Name = Name,
                    Surname = Surname,
                    Username = Username,
                    Password = Username,
                    Jbmg = JMBG,
                    Phone = Phone,
                    Email = Email,
                    Address = Address,
                    IsAcive = true,
                    AccountType = 2

                };
                if (await _employeeDAO.AddEmployeeAsync(NewEmployee))
                {
                    var messageBox = new CustomMessageBox("Zaposleni uspješno dodan.");
                    messageBox.ShowDialog();
                    await _employeesViewModel.LoadEmployeesAsync();
                    employees.Add(NewEmployee);
                }
                else
                {
                    var messageBox = new CustomMessageBox("Nije moguće dodati zaposlenog");
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
                    nameof(Username) when employees.Any(e => string.Equals(e.Username, Username, StringComparison.OrdinalIgnoreCase)) => "Korisnicko ime je zauzeto.",
                    nameof(JMBG) when string.IsNullOrWhiteSpace(JMBG) => "JMBG ne moze biti prazan.",
                    nameof(JMBG) when !int.TryParse(JMBG, out jmbg) => "JMBG nije validan.",
                    nameof(JMBG) when employees.Any(e => e.Jbmg == JMBG) => "Postoji korisnik sa JMBGom.",
                    nameof(Phone) when string.IsNullOrWhiteSpace(Phone) => "Broj telefona ne moze biti prazan.",
                    nameof(Phone) when !int.TryParse(Phone, out jmbg) => "Broj telefona nije validan.",
                    nameof(Phone) when employees.Any(e => e.Phone == Phone) => "Telefon postoji.",
                    nameof(Email) when string.IsNullOrWhiteSpace(Email) => "Email ne može biti prazan.",
                    nameof(Email) when employees.Any(e => string.Equals(e.Email, Email, StringComparison.OrdinalIgnoreCase)) => "Email postoji.",
                    nameof(Address) when string.IsNullOrWhiteSpace(Address) => "Adresa ne može biti prazana.",
                    _ => string.Empty
                };
            }
        }
    }
}
