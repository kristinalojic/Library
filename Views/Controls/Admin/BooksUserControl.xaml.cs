﻿using Library.ViewModels.Admin;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library.Views.Controls.Admin
{
    /// <summary>
    /// Interaction logic for BooksUserControl.xaml
    /// </summary>
    public partial class BooksUserControl : UserControl
    {
        public BooksUserControl()
        {
            InitializeComponent();
            this.Loaded += BooksUserControl_Loaded; 
        }

        private async void BooksUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            AdminBooksViewModel viewModel = await AdminBooksViewModel.CreateAsync();
            DataContext = viewModel; 
        }
    }
}
