using Employee_And_Company_Management.Commands;
using Library.DAO;
using Library.DAO.MySQL;
using Library.Models.Entities;
using Library.ViewModels.Admin;
using Library.Views.Controls.Admin;
using Library.Views.Windows;
using Library.Views.Windows.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Library.ViewModels
{
    class BookViewModel : BaseViewModel, IDataErrorInfo
    {
        private Window _currentWindow;

        private readonly IGenre _genreDAO;

        private readonly IBook _bookDAO;

        private readonly AdminBooksViewModel _adminBooksViewModel;

        private string _title;
        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _author;

        public string? Author
        {
            get => _author;
            set
            {
                SetProperty(ref _author, value);
                OnPropertyChanged(nameof(Title));
            }
        }

        public string _genre;

        public string? Genre
        {
            get => _genre;
            set => SetProperty(ref _genre, value);
        }

        private string? _yearOfPublication;

        public string? YearOfPublication
        {
            get => _yearOfPublication;
            set => SetProperty(ref _yearOfPublication, value);
        }

        private string? _copies;

        public string? Copies
        {
            get => _copies;
            set => SetProperty(ref _copies, value);
        }

        private ObservableCollection<Genre> _genres;
        public ObservableCollection<Genre> Genres
        {
            get => _genres;
            set => SetProperty(ref _genres, value);
        }

        public ICommand CancelCommand { get; set; }
        public ICommand SubmitCommand { get; set; }

        List<Book> books;

        public BookViewModel(Window window, AdminBooksViewModel adminBooksViewModel)
        {
            _currentWindow = window;
            _adminBooksViewModel = adminBooksViewModel;
            CancelCommand = new RelayCommand(Cancel, CanCancel);
            SubmitCommand = new RelayCommand(async obj => await Submit(obj), CanSubmit);
            _genreDAO = new GenreDAO();
            _bookDAO = new BookDAO();
            LoadGenres();
            LoadBooks();
        }

        async void LoadBooks()
        {
            books = await _bookDAO.GetAllBooksAsync();
        }

        private async void LoadGenres()
        {
            var genresFromDb = await _genreDAO.GetAllGenresAsync();
            Genres = new ObservableCollection<Genre>(genresFromDb);

            if (Genres.Any())
            {
                Genre = Genres.First().Name;
            }
        }

        private void Cancel(object? obj)
        {
            _currentWindow.Close();
        }

        private bool CanCancel(object? obj) => true;

        private async Task Submit(object? obj)
        {
            _isValidationEnabled = true;
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Author));
            OnPropertyChanged(nameof(Genre));
            OnPropertyChanged(nameof(YearOfPublication));
            OnPropertyChanged(nameof(Copies));

            if (string.IsNullOrEmpty(this[nameof(Title)]) &&
                string.IsNullOrEmpty(this[nameof(Author)]) &&
                string.IsNullOrEmpty(this[nameof(Genre)]) &&
                string.IsNullOrEmpty(this[nameof(YearOfPublication)]) &&
                string.IsNullOrEmpty(this[nameof(Copies)]))
            {
                var NewBook = new Book
                {
                    Title = Title,
                    Author = Author,
                    YearOfPublication = int.Parse(YearOfPublication),
                    Copies = (sbyte)int.Parse(Copies),
                    AvailableCopies = (sbyte)int.Parse(Copies),
                    GenreId = await _genreDAO.GetIdByNameAsync(Genre)
                };
                if (await _bookDAO.AddBookAsync(NewBook))
                {
                    var messageBox = new CustomMessageBox("Knjiga uspjesno dodana.");
                    messageBox.ShowDialog();
                    await _adminBooksViewModel.LoadBooks();
                    books.Add(NewBook);
                }
                else
                {
                    var messageBox = new CustomMessageBox("Nije moguće dodati knjigu");
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
                int year = 0, copies = 0;
                return columnName switch
                {
                    nameof(Title) when string.IsNullOrWhiteSpace(Title) => "Naslov ne može biti prazan.",
                    nameof(Title) when books.Any(b => string.Equals(b.Title, Title, StringComparison.OrdinalIgnoreCase) && string.Equals(b.Author, Author, StringComparison.OrdinalIgnoreCase)) => "Knjiga vec postoji.",
                    nameof(Author) when string.IsNullOrWhiteSpace(Author) => "Autor ne može biti prazan.",
                    nameof(YearOfPublication) when string.IsNullOrWhiteSpace(YearOfPublication) => "Godina izdanja ne može biti prazna.",
                    nameof(YearOfPublication) when !int.TryParse(YearOfPublication, out year) => "Godina izdanja mora biti broj.",
                    nameof(YearOfPublication) when year <= 0 || year > DateTime.Now.Year => "Godina izdanja mora biti validan broj.",
                    nameof(Copies) when string.IsNullOrWhiteSpace(Copies) => "Broj primjeraka ne može biti prazna.",
                    nameof(Copies) when !int.TryParse(Copies.ToString(), out copies) => "Broj kopija mora biti broj.",
                    nameof(Copies) when copies <= 0 => "Broj kopija mora biti validan broj.",
                    _ => string.Empty
                };
            }
        }

    }
}
