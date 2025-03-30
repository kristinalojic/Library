using Employee_And_Company_Management.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Library.Views.Windows.Admin;
using Library.Views.Windows.Employee;
using Library.DAO.MySQL;

namespace Library.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private readonly EmployeeDAO _employeeDAO;
        public string? Username { get; set; }
        public string? Password { get; set; }

        private string? _message;

        public string? Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private bool _isLoggingIn;
        public bool IsLoggingIn
        {
            get => _isLoggingIn;
            set => SetProperty(ref _isLoggingIn, value);
        }

        private Window _currentWindow;

        public ICommand LoginCommand { get; set; }

        public LoginViewModel(Window window)
        {
            _employeeDAO = new EmployeeDAO();
            LoginCommand = new RelayCommand(async (obj) => await ExecuteLogin(obj), CanExecuteLogin);
            _currentWindow = window;
            Task.Run(async () => await _employeeDAO.PreloadDatabase());
        }


        private bool CanExecuteLogin(object? obj)
        {
            return true;
        }

        private async Task ExecuteLogin(object? obj)
        {
            IsLoggingIn = true;
            Message = "";
            await Task.Delay(500);
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                Message = "Neuspješna prijava. Pokušajte ponovo.";
            }
            else 
            { 
                var employee = await _employeeDAO.FindByUsernameAndPasswordAsync(Username, Password);

                if (employee == null || !employee.IsAcive)
                {
                    Message = "Neuspješna prijava. Pokušajte ponovo.";
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (employee.AccountType == 1)
                        {
                            AdminWindow administratorWindow = new AdminWindow();
                            administratorWindow.Show();
                            _currentWindow.Close();
                        }
                        else
                        {
                            EmployeeWindow employeeWindow = new EmployeeWindow();
                            employeeWindow.Show();
                            _currentWindow.Close();
                        }
                    });
                }
            }
            IsLoggingIn = false;
        }
    }
}
