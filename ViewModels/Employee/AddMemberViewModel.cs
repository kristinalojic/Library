using Employee_And_Company_Management.Commands;
using Library.DAO.MySQL;
using Library.DAO;
using Library.ViewModels.Admin;
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
using System.IO;

namespace Library.ViewModels.Employee
{
    class AddMemberViewModel : BaseViewModel, IDataErrorInfo
    {
        private Window _currentWindow;

        private readonly IMember _memberDAO;

        private readonly MembersViewModel _membersViewModel;

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

        List<Member> members;

        public AddMemberViewModel(Window window, MembersViewModel membersViewModel)
        {
            _currentWindow = window;
            _membersViewModel = membersViewModel;
            CancelCommand = new RelayCommand(Cancel, CanCancel);
            SubmitCommand = new RelayCommand(async obj => await Submit(obj), CanSubmit);
            _memberDAO = new MemberDAO();
            LoadMembers();
        }

        async void LoadMembers()
        {
            members = await _memberDAO.GetAllMembersAsync();
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
            OnPropertyChanged(nameof(Phone));
            OnPropertyChanged(nameof(Address));
            OnPropertyChanged(nameof(Email));

            if (string.IsNullOrEmpty(this[nameof(Name)]) &&
                string.IsNullOrEmpty(this[nameof(Surname)]) &&
                string.IsNullOrEmpty(this[nameof(Phone)]) &&
                string.IsNullOrEmpty(this[nameof(Email)]) &&
                string.IsNullOrEmpty(this[nameof(Address)])
                )
            {
                var NewMember = new Member
                {
                    Name = Name,
                    Surname = Surname,
                    Phone = Phone,
                    Email = Email,
                    Address = Address


                };
                if (await _memberDAO.AddMemberAsync(NewMember))
                {
                    var messageBox = new CustomMessageBox(TryGetResource("MemberAddedSuccessfully"));
                    messageBox.ShowDialog();
                    await _membersViewModel.LoadMembers();
                    members.Add(NewMember);
                }
                else
                {
                    var messageBox = new CustomMessageBox(TryGetResource("UnableToAddMember"));
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
                    nameof(Name) when string.IsNullOrWhiteSpace(Name) => TryGetResource("EmptyField"),
                    nameof(Surname) when string.IsNullOrWhiteSpace(Surname) => TryGetResource("EmptyField"),
                    nameof(Phone) when string.IsNullOrWhiteSpace(Phone) => TryGetResource("EmptyField"),
                    nameof(Phone) when !int.TryParse(Phone, out jmbg) => TryGetResource("InvalidInput"),
                    nameof(Phone) when members.Any(e => e.Phone == Phone) => TryGetResource("AlreadyTaken"),
                    nameof(Email) when string.IsNullOrWhiteSpace(Email) => TryGetResource("EmptyField"),
                    nameof(Email) when members.Any(e => string.Equals(e.Email, Email, StringComparison.OrdinalIgnoreCase)) => TryGetResource("AlreadyTaken"),
                    nameof(Address) when string.IsNullOrWhiteSpace(Address) => TryGetResource("EmptyField"),
                    _ => string.Empty
                };
            }
        }
    }
}
