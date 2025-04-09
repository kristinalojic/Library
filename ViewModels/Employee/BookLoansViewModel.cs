using Library.DAO;
using Library.DAO.MySQL;
using Library.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels.Employee
{
    class BookLoansViewModel : BaseViewModel
    {
        private Book _selectedBook;

        private readonly ILoan _loanDAO;

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
        public BookLoansViewModel(Book book) 
        {
            _selectedBook = book;
            _loanDAO = new LoanDAO();
            LoadLoans();
        }

        public async Task LoadLoans()
        {
            var loans = await _loanDAO.GetLoansByBookIdAsync(_selectedBook.Id);
            ActiveLoans = new ObservableCollection<Loan>(loans.Where(l => !l.IsReturned));
            ReturnedLoans = new ObservableCollection<Loan>(loans.Where(l => l.IsReturned));
        }
    }
}
