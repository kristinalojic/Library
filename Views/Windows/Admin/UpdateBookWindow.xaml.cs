using Library.Models.Entities;
using Library.ViewModels.Admin;
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
    /// Interaction logic for UpdateBookWindow.xaml
    /// </summary>
    public partial class UpdateBookWindow : Window
    {
        public UpdateBookWindow(AdminBooksViewModel adminBooksViewModel, Book book)
        {
            InitializeComponent();
            DataContext = new UpdateBookViewModel(this, adminBooksViewModel, book);
        }
    }
}
