using Employee_And_Company_Management.Commands;
using Library.DAO;
using Library.DAO.MySQL;
using Library.Models.Entities;
using Library.ViewModels.Admin;
using Library.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace Library.ViewModels.Employee
{
    public class CheckOutViewModel : BaseViewModel, IDataErrorInfo
    {
        private Window _currentWindow;

        private readonly IMember _memberDAO;

        private readonly ILoan _loanDAO;

        private readonly IBook _bookDAO;

        private readonly EmployeeBooksViewModel _model;

        private Book _selectedBook;

        private string _book;

        public string Book
        {
            get => _book;
            set => SetProperty(ref _book, value);
        }

        private ObservableCollection<Member> _autoSuggestMembers;

        public ObservableCollection<Member> AutoSuggestMembers
        {
            get => _autoSuggestMembers;
            set => SetProperty(ref _autoSuggestMembers, value);
        }

        private ObservableCollection<Member> _autoSuggestFilteredMembers;

        public ObservableCollection<Member> AutoSuggestFilteredMembers
        {
            get => _autoSuggestFilteredMembers;
            set => SetProperty(ref _autoSuggestFilteredMembers, value);
        }

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                FilterMembers();
                FindMember();
            }
        }

        private Member _selectedMember;

        public Member SelectedMember
        {
            get => _selectedMember;
            set
            {
                SetProperty(ref _selectedMember, value);
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        public ICommand CancelCommand { get; set; }
        public ICommand SubmitCommand { get; set; }

        public CheckOutViewModel(Window window, Book book, EmployeeBooksViewModel model)
        {
            _currentWindow = window;
            _memberDAO = new MemberDAO();
            _loanDAO = new LoanDAO();
            _bookDAO = new BookDAO();
            _selectedBook = book;
            _model = model;
            Book = book.Title + ", " + book.Author;
            CancelCommand = new RelayCommand(Cancel, CanCancel);
            SubmitCommand = new RelayCommand(async obj => await Submit(obj), CanSubmit);
            LoadMembers();
            SelectedDate = DateTime.Today;
        }

        private async void LoadMembers()
        {
            var members = await _memberDAO.GetAllMembersAsync();
            AutoSuggestMembers = new ObservableCollection<Member>(members.Where(m => m.MembershipFee.Expiration >= DateOnly.FromDateTime(DateTime.Today)));
        }

        private void Cancel(object? obj)
        {
            _currentWindow.Close();
        }

        private bool CanCancel(object? obj) => true;
        private bool CanSubmit(object? obj) => true;

        private bool _isValidationEnabled = false;

        private async Task Submit(object? obj)
        {
            _isValidationEnabled = true;
            OnPropertyChanged(nameof(SelectedMember));
            OnPropertyChanged(nameof(SelectedDate));

            if (string.IsNullOrEmpty(this[nameof(SelectedDate)]) &&
                string.IsNullOrEmpty(this[nameof(SelectedMember)])
                )
            {
                var NewLoan = new Loan
                {
                    Book = _selectedBook.Id,
                    Member = SelectedMember.MembershipCardNumber,
                    IssueDate = DateOnly.FromDateTime(DateTime.Today),
                    DueDate = DateOnly.FromDateTime(SelectedDate),
                    HasBeenExtended = false,
                    IsReturned = false

                };
                if (await _loanDAO.AddLoanAsync(NewLoan))
                {
                    var messageBox = new CustomMessageBox("Zaduzenje dodano.");
                    messageBox.ShowDialog();
                    _selectedBook.AvailableCopies--;
                    await _bookDAO.UpdateBookAsync(_selectedBook);
                    await _model.LoadBooks();
                    _currentWindow.Close();
                }
                else
                {
                    var messageBox = new CustomMessageBox("Nije moguće dodati zaduzenje");
                    messageBox.ShowDialog();
                }
            }
        }

        private void FilterMembers()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                AutoSuggestFilteredMembers = new ObservableCollection<Member>(
                    AutoSuggestMembers.Where(m =>
                        ($"{m.Name}{m.Surname}").StartsWith(SearchText.ToString(), StringComparison.OrdinalIgnoreCase) ||
                        m.MembershipCardNumber.ToString().StartsWith(SearchText.ToString(), StringComparison.OrdinalIgnoreCase)
                    ));
            }
            else
            {
                AutoSuggestFilteredMembers = new ObservableCollection<Member>(AutoSuggestMembers);
            }
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                if (!_isValidationEnabled) return string.Empty;
                int jmbg = 0;
                switch (columnName)
                {
                    case nameof(SelectedMember):
                        return SelectedMember == null ? "Nevalidan unos za član." : string.Empty;
                    case nameof(SelectedDate):
                        return SelectedDate == default || SelectedDate <= DateTime.Today ? "Nevalidan datum." : string.Empty;
                    default:
                        return string.Empty;
                }
            }
        }

        private void FindMember()
        {
            var match = Regex.Match(SearchText, @"\((\d+)\)");

            if (match.Success)
            {
                int membershipCardNumber;
                if (int.TryParse(match.Groups[1].Value, out membershipCardNumber))
                {
                    SelectedMember = AutoSuggestMembers.FirstOrDefault(m => m.MembershipCardNumber == membershipCardNumber);
                }
            }
            else SelectedMember = null;
        }

    }
}

