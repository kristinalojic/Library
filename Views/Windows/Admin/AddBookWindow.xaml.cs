﻿using Library.ViewModels;
using Library.ViewModels.Admin;
using Library.Views.Controls.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Library.Views.Windows.Admin
{
    /// <summary>
    /// Interaction logic for AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        public AddBookWindow(AdminBooksViewModel booksViewModel)
        {
            InitializeComponent();
            DataContext = new BookViewModel(this, booksViewModel);
        }
    }
}
