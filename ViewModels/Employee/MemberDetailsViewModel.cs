using Library.DAO.MySQL;
using Library.DAO;
using Library.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Library.Views.Windows;
using Employee_And_Company_Management.Commands;
using Library.ViewModels.Admin;
using Microsoft.VisualBasic;
using System.ComponentModel;

namespace Library.ViewModels.Employee
{
    class MemberDetailsViewModel : BaseViewModel, IDataErrorInfo
    {
        private Member _selectedMember;

        private readonly ILoan _loanDAO;

        private readonly IBook _bookDAO;

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

        private string? _dueDate;

        public string? DueDate
        {
            get => _dueDate;
            set => SetProperty(ref _dueDate, value);
        }

        private ObservableCollection<Loan> _activeLoans;
        public ObservableCollection<Loan> ActiveLoans
        {
            get => _activeLoans;
            set
            {
                if (SetProperty(ref _activeLoans, value))
                {
                    OnPropertyChanged(nameof(ActiveLoansCount));
                    _activeLoans.CollectionChanged += (s, e) => OnPropertyChanged(nameof(ActiveLoansCount));
                }
            }
        }
        public int ActiveLoansCount => ActiveLoans?.Count ?? 0;

        private ObservableCollection<Loan> _returnedLoans;
        public ObservableCollection<Loan> ReturnedLoans
        {
            get => _returnedLoans;
            set
            {
                if (SetProperty(ref _returnedLoans, value))
                {
                    OnPropertyChanged(nameof(ReturnedLoansCount));
                    _activeLoans.CollectionChanged += (s, e) => OnPropertyChanged(nameof(ReturnedLoansCount));
                }
            }
        }
        public int ReturnedLoansCount => ReturnedLoans?.Count ?? 0;

        private Loan _selectedLoan;
        public Loan SelectedLoan
        {
            get => _selectedLoan;
            set => SetProperty(ref _selectedLoan, value);
        }

        public ICommand CheckInCommand { get; set; }
        public ICommand ExtendCommand { get; set; }
        public ICommand ExtendMembershipCommand { get; set; }
        public ICommand SubmitCommand { get; set; }

        List<Member> members;

        public MemberDetailsViewModel(MembersViewModel model, Member member)
        {
            _selectedMember = member;
            _loanDAO = new LoanDAO();
            _bookDAO = new BookDAO();
            _memberDAO = new MemberDAO();
            _membersViewModel = model;
            DueDate = "Expiration date : " + member.MembershipFee.Expiration;
            CheckInCommand = new RelayCommand(async obj => await CheckIn(obj), CanCheckIn);
            ExtendCommand = new RelayCommand(async obj => await Extend(obj), CanExtend);
            SubmitCommand = new RelayCommand(async obj => await Submit(obj), CanSubmit);
            ExtendMembershipCommand = new RelayCommand(async obj => await ExtendMembership(obj), CanExtendMembership);
            LoadLoans();
            SetData();
        }

        public async Task LoadLoans()
        {
            var loans = await _loanDAO.GetLoansByMemberAsync(_selectedMember);
            ActiveLoans = new ObservableCollection<Loan>(loans.Where(l => !l.IsReturned));
            ReturnedLoans = new ObservableCollection<Loan>(loans.Where(l => l.IsReturned));
            members = await _memberDAO.GetAllMembersAsync();
        }

        private void SetData()
        {
            Name = _selectedMember.Name;
            Surname = _selectedMember.Surname;
            Phone = _selectedMember.Phone;
            Email = _selectedMember.Email;
            Address = _selectedMember.Address;
        }

        private bool CanCheckIn(object? obj) => true;

        private async Task CheckIn(object? obj)
        {
            if (_selectedLoan == null)
            {
                var messageBox = new CustomMessageBox("Selektujte knjigu.");
                messageBox.ShowDialog();
            }
            else
            {
                DeleteConfirmationDialog dialog = new DeleteConfirmationDialog("Da li ste sigurni da želite razduziti knjigu?");
                bool answer = dialog.ShowDialog() ?? false;

                if (answer)
                {
                    _selectedLoan.DueDate = DateOnly.FromDateTime(DateTime.Today);
                    _selectedLoan.IsReturned = true;
                    if (await _loanDAO.UpdateLoanAsync(_selectedLoan))
                    {
                        var messageBox = new CustomMessageBox("Knjiga razduzena.");
                        messageBox.ShowDialog();
                        var _book = _selectedLoan.Book;
                        await _bookDAO.ReturnBook(_book);
                        await LoadLoans();
                    }
                    else
                    {
                        var messageBox = new CustomMessageBox("Nije moguće razduziti knjigu");
                        messageBox.ShowDialog();
                    }
                }
            }
        }

        private bool CanExtend(object? obj) => true;

        private async Task Extend(object? obj)
        {
            if (_selectedLoan == null)
            {
                var messageBox = new CustomMessageBox("Selektujte knjigu.");
                messageBox.ShowDialog();
            }
            else
            {
                if (_selectedLoan.DueDate >= DateOnly.FromDateTime(DateTime.Today).AddDays(3) || _selectedLoan.HasBeenExtended)
                {
                    var messageBox = new CustomMessageBox("Nije moguće produziti rok.");
                    messageBox.ShowDialog();
                }
                else
                {
                    DeleteConfirmationDialog dialog = new DeleteConfirmationDialog("Da li ste sigurni da želite produziti rok?");
                    bool answer = dialog.ShowDialog() ?? false;

                    if (answer)
                    {
                        if (_selectedLoan.DueDate <= DateOnly.FromDateTime(DateTime.Today).AddDays(3) && !_selectedLoan.HasBeenExtended)
                        {
                            _selectedLoan.DueDate = _selectedLoan.DueDate.AddDays(7);
                            _selectedLoan.HasBeenExtended = true;
                            if (await _loanDAO.UpdateLoanAsync(_selectedLoan))
                            {
                                var messageBox = new CustomMessageBox("Rok produzen.");
                                messageBox.ShowDialog();
                                await LoadLoans();
                            }
                        }
                        else
                        {
                            var messageBox = new CustomMessageBox("Nije moguće produziti rok.");
                            messageBox.ShowDialog();
                        }
                    }
                }
            }
        }

        private bool CanExtendMembership(object? obj) => true;

        private async Task ExtendMembership(object? obj)
        {
            if (_selectedMember == null)
            {
                var messageBox = new CustomMessageBox("Greska.");
                messageBox.ShowDialog();
            }
            else
            {
                if (_selectedMember.MembershipFee.Expiration >= DateOnly.FromDateTime(DateTime.Today).AddDays(30))
                {
                    var messageBox = new CustomMessageBox("Nije moguće produziti clanarinu.");
                    messageBox.ShowDialog();
                }
                else
                {
                    DeleteConfirmationDialog dialog = new DeleteConfirmationDialog("Da li ste sigurni da želite produziti clanarinu?");
                    bool answer = dialog.ShowDialog() ?? false;

                    if (answer)
                    {
                        if (_selectedMember.MembershipFee.Expiration <= DateOnly.FromDateTime(DateTime.Today).AddDays(30))
                        {
                            _selectedMember.MembershipFee.Issuence = DateOnly.FromDateTime(DateTime.Today);
                            _selectedMember.MembershipFee.Expiration = DateOnly.FromDateTime(DateTime.Today).AddDays(365);

                            if (await _memberDAO.UpdateMemberWithFeeAsync(_selectedMember))
                            {
                                var messageBox = new CustomMessageBox("Clanarina produzena.");
                                messageBox.ShowDialog();
                                await _membersViewModel.LoadMembers();
                                DueDate = "Expiration date: " + _selectedMember.MembershipFee.Expiration;
                            }
                        }
                        else
                        {
                            var messageBox = new CustomMessageBox("Nije moguće produziti clanarinu.");
                            messageBox.ShowDialog();
                        }
                    }
                }
            }
        }

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
                    MembershipCardNumber = _selectedMember.MembershipCardNumber,
                    Name = Name,
                    Surname = Surname,
                    Phone = Phone,
                    Email = Email,
                    Address = Address
                };
                if (await _memberDAO.UpdateMemberAsync(NewMember))
                {
                    var messageBox = new CustomMessageBox("Clan je azuriran.");
                    messageBox.ShowDialog();
                    await _membersViewModel.LoadMembers();
                }
                else
                {
                    var messageBox = new CustomMessageBox("Nije moguće azurirati clana");
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
                int phone = 0;
                return columnName switch
                {
                    nameof(Name) when string.IsNullOrWhiteSpace(Name) => "Ime ne moze biti prazno.",
                    nameof(Surname) when string.IsNullOrWhiteSpace(Surname) => "Prezime ne moze biti prazno.",
                    nameof(Phone) when string.IsNullOrWhiteSpace(Phone) => "Broj telefona ne moze biti prazan.",
                    nameof(Phone) when !int.TryParse(Phone, out phone) => "Broj telefona nije validan.",
                    nameof(Phone) when members.Any(m => m.Phone == Phone && m.Phone != _selectedMember.Phone) => "Telefon postoji.",
                    nameof(Email) when string.IsNullOrWhiteSpace(Email) => "Email ne može biti prazan.",
                    nameof(Email) when members.Any(m => string.Equals(m.Email, Email, StringComparison.OrdinalIgnoreCase) && m.Email != _selectedMember.Email) => "Email postoji.",
                    nameof(Address) when string.IsNullOrWhiteSpace(Address) => "Adresa ne može biti prazana.",
                    _ => string.Empty
                };
            }
        }
    }
}
