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
using Library.Models;
using System.Windows;
using Library.Views.Windows;

namespace Library.ViewModels.Admin
{
    public class AdminBooksViewModel : BaseViewModel
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

        private ObservableCollection<Book> _filteredArchivedBooks;
        public ObservableCollection<Book> FilteredArchivedBooks
        {
            get => _filteredArchivedBooks;
            set
            {
                if (SetProperty(ref _filteredArchivedBooks, value))
                {
                    _filteredArchivedBooks.CollectionChanged += (s, e) => OnPropertyChanged(nameof(ArchivedBooksCount));
                }
            }
        }

        public int AvailableBooksCount => FilteredAvailableBooks?.Count ?? 0;

        public int ArchivedBooksCount => FilteredArchivedBooks?.Count ?? 0;

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

        private string _searchText2;
        public string SearchText2
        {
            get => _searchText2;
            set
            {
                if (_searchText2 != value)
                {
                    SetProperty(ref _searchText2, value);
                    FilterBooks();
                }
            }
        }

        public ICommand AddBookCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ActivateCommand { get; set; }
        public ICommand UpdateBookCommand { get; set; }

        private AdminBooksViewModel()
        {
            _booksDAO = new BookDAO();
            AddBookCommand = new RelayCommand(AddBook, CanAddBook);
            DeleteCommand = new RelayCommand(async obj => await DeleteSelectedBook(obj), CanDeleteSelectedBook);
            ActivateCommand = new RelayCommand(async obj => await ActivateSelectedBook(obj), CanActivateSelectedBook);
            UpdateBookCommand = new RelayCommand(UpdateSelectedBook, CanUpdateSelectedBook);
        }

        public static async Task<AdminBooksViewModel> CreateAsync()
        {
            var viewModel = new AdminBooksViewModel();
            await viewModel.LoadBooks(); 
            return viewModel;
        }

        public async Task LoadBooks()
        {
            var books = await _booksDAO.GetAllBooksAsync();
            AvailableBooks = new ObservableCollection<Book>(books.Where(b => b.IsAvailable));
            ArchivedBooks = new ObservableCollection<Book>(books.Where(b => !b.IsAvailable));
            FilterBooks();
        }
        private void AddBook(object? obj)
        {
            AddBookWindow window = new AddBookWindow(this);
            window.Show();
        }

        private bool CanAddBook(object? obj) => true;

        private void UpdateSelectedBook(object? obj)
        {
            var _pomBook = SelectedBook;
            if (_pomBook != null)
            {
                UpdateBookWindow window = new UpdateBookWindow(this, _pomBook);
                window.Show();
            }
            else
            {
                var messageBox = new CustomMessageBox(TryGetResource("SelectBook"));
                messageBox.ShowDialog();
            }
        }

        private bool CanUpdateSelectedBook(object? obj) => true;

        private bool CanDeleteSelectedBook(object parameter)
        {
            return true;
        }

        private async Task DeleteSelectedBook(object parameter)
        {
            var _pomBook = SelectedBook;
            if (_pomBook != null)
            {
                DeleteConfirmationDialog dialog = new DeleteConfirmationDialog(TryGetResource("ConfirmArchiveBook"));
                bool answer = dialog.ShowDialog() ?? false;

                if (answer)
                {
                    _pomBook.IsAvailable = false;
                    AvailableBooks.Remove(_pomBook);
                    ArchivedBooks.Add(_pomBook);

                    await _booksDAO.UpdateBookAsync(_pomBook);
                    FilterBooks();
                }
            }
            else
            {
                var messageBox = new CustomMessageBox(TryGetResource("SelectBook"));
                messageBox.ShowDialog();
            }
        }

        private bool CanActivateSelectedBook(object parameter)
        {
            return true;
        }

        private async Task ActivateSelectedBook(object parameter)
        {
            var _pomBook = SelectedBook;
            if (_pomBook != null)
            {
                DeleteConfirmationDialog dialog = new DeleteConfirmationDialog(TryGetResource("ConfirmReturnToOffer"));
                bool answer = dialog.ShowDialog() ?? false;

                if (answer)
                {
                    _pomBook.IsAvailable = true;
                    AvailableBooks.Add(_pomBook);
                    ArchivedBooks.Remove(_pomBook);

                    await _booksDAO.UpdateBookAsync(_pomBook);
                    FilterBooks();
                }
            }
            else
            {
                var messageBox = new CustomMessageBox(TryGetResource("SelectBook"));
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
            if (string.IsNullOrWhiteSpace(SearchText2))
            {
                FilteredArchivedBooks = new ObservableCollection<Book>(_archivedBooks);
            }
            else
            {
                FilteredArchivedBooks = new ObservableCollection<Book>(_archivedBooks
                    .Where(b => b.Title.StartsWith(SearchText2, StringComparison.OrdinalIgnoreCase) ||
                                b.Author.StartsWith(SearchText2, StringComparison.OrdinalIgnoreCase)));
            }
        }
    }
}
