using Employee_And_Company_Management.Commands;
using Library.DAO.MySQL;
using Library.Models.Entities;
using Library.Views.Windows.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Library.ViewModels
{
    class BookViewModel : BaseViewModel
    {
        private Window _currentWindow;

        private readonly GenreDAO _genreDAO;

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
            set => SetProperty(ref _author, value);
        }

        public string _genre;

        public string? Genre
        {
            get => _genre;
            set => SetProperty(ref _genre, value);
        }

        private int _yearOfPublication;

        public int YearOfPublication
        {
            get => _yearOfPublication;
            set => SetProperty(ref _yearOfPublication, value);
        }

        private sbyte _copies;

        public sbyte Copies
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

        public BookViewModel(Window window)
        {
            _currentWindow = window;
            CancelCommand = new RelayCommand(Cancel, CanCancel);
            SubmitCommand = new RelayCommand(Submit, CanSubmit);
            _genreDAO = new GenreDAO();
            LoadGenres();  
        }

        private async void LoadGenres()
        {
            var genresFromDb = await _genreDAO.GetAllGenresAsync();
            Genres = new ObservableCollection<Genre>(genresFromDb);
        }

        private void Cancel(object? obj)
        {
            _currentWindow.Close();
        }

        private bool CanCancel(object? obj) => true;

        private void Submit(object? obj)
        {
            AddBookWindow window = new AddBookWindow();
            window.Show();
        }

        private bool CanSubmit(object? obj) => true;

    }
}
