using Employee_And_Company_Management.Commands;
using Library.DAO.MySQL;
using Library.DAO;
using Library.Models.Entities;
using Library.ViewModels.Admin;
using Library.Views.Windows.Employee;
using Library.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.ViewModels.Employee
{
    public class EmployeeBooksViewModel : BaseViewModel
    {
        private readonly IBook _booksDAO;

        private ObservableCollection<Book> _availableBooks;
        public ObservableCollection<Book> AvailableBooks
        {
            get => _availableBooks;
            set
            {
                if (SetProperty(ref _availableBooks, value))
                {
                    OnPropertyChanged(nameof(AvailableBooksCount));
                    _availableBooks.CollectionChanged += (s, e) => OnPropertyChanged(nameof(AvailableBooksCount));
                }
            }
        }

        private ObservableCollection<Book> _filteredAvailableBooks;
        public ObservableCollection<Book> FilteredAvailableBooks
        {
            get => _filteredAvailableBooks;
            set
            {
                if (SetProperty(ref _filteredAvailableBooks, value))
                {
                    _filteredAvailableBooks.CollectionChanged += (s, e) => OnPropertyChanged(nameof(AvailableBooksCount));
                }
            }
        }

        public int AvailableBooksCount => FilteredAvailableBooks?.Count ?? 0;

        private Book _selectedBook;
        public Book SelectedBook
        {
            get => _selectedBook;
            set => SetProperty(ref _selectedBook, value);
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
                    FilterBooks();
                }
            }
        }

        public ICommand CheckOutBookCommand { get; set; }
        public ICommand ViewLoansCommand { get; set; }

        private EmployeeBooksViewModel()
        {
            _booksDAO = new BookDAO();
            CheckOutBookCommand = new RelayCommand(CheckOutSelectedBook, CanCheckOutBook);
            ViewLoansCommand = new RelayCommand(ViewLoans, CanViewLoans);
        }

        public static async Task<EmployeeBooksViewModel> CreateAsync()
        {
            var viewModel = new EmployeeBooksViewModel();
            await viewModel.LoadBooks();
            return viewModel;
        }

        public async Task LoadBooks()
        {
            var books = await _booksDAO.GetAllBooksAsync();
            AvailableBooks = new ObservableCollection<Book>(books.Where(b => b.IsAvailable));
            FilterBooks();
        }

        private bool CanCheckOutBook(object? obj) => true;

        private void CheckOutSelectedBook(object? obj)
        {
            var _pomBook = SelectedBook;
            if (_pomBook != null && _pomBook.AvailableCopies > 0)
            {
                CheckOutBookWindow window = new CheckOutBookWindow(this, _pomBook);
                window.Show();
            }
            else if (_pomBook != null && _pomBook.AvailableCopies < 1)
            {
                var messageBox = new CustomMessageBox("Knjiga nije dostupna");
                messageBox.ShowDialog();
            }
            else
            {
                var messageBox = new CustomMessageBox("Selektujte knjigu.");
                messageBox.ShowDialog();
            }
        }

        private bool CanViewLoans(object? obj) => true;

        private void ViewLoans(object? obj)
        {
            var _pomBook = SelectedBook;
            if (_pomBook != null)
            {
                LoansWindow window = new LoansWindow(this, _pomBook);
                window.Show();
            }
            else
            {
                var messageBox = new CustomMessageBox("Selektujte knjigu.");
                messageBox.ShowDialog();
            }
        }

        private void FilterBooks()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredAvailableBooks = new ObservableCollection<Book>(_availableBooks);
            }
            else
            {
                FilteredAvailableBooks = new ObservableCollection<Book>(_availableBooks
                    .Where(b => b.Title.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                b.Author.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase)));
            }
        }
    }
}
