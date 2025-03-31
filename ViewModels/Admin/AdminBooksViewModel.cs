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
using Library.Views.Windows.Admin;
using Employee_And_Company_Management.Commands;

namespace Library.ViewModels.Admin
{
    class AdminBooksViewModel : BaseViewModel
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

        private ObservableCollection<Book> _archivedBooks;
        public ObservableCollection<Book> ArchivedBooks
        {
            get => _archivedBooks;
            set
            {
                if (SetProperty(ref _archivedBooks, value))
                {
                    OnPropertyChanged(nameof(ArchivedBooksCount));
                    _archivedBooks.CollectionChanged += (s, e) => OnPropertyChanged(nameof(ArchivedBooksCount));
                }
            }
        }

        public int AvailableBooksCount => AvailableBooks?.Count ?? 0;

        public int ArchivedBooksCount => ArchivedBooks?.Count ?? 0;

        public ICommand AddBookCommand { get; set; }

        public AdminBooksViewModel()
        {
            _booksDAO = new BookDAO();
            LoadBooks();
            AddBookCommand = new RelayCommand(AddBook, CanAddBook);
        }

        private async void LoadBooks()
        {
            var books = await _booksDAO.GetAllBooksAsync();
            AvailableBooks = new ObservableCollection<Book>(books.Where(b => b.IsAvailable));
            ArchivedBooks = new ObservableCollection<Book>(books.Where(b => !b.IsAvailable));
        }

        private void AddBook(object? obj)
        {
            AddBookWindow window = new AddBookWindow();
            window.Show();
        }

        private bool CanAddBook(object? obj) => true;
    }
}
