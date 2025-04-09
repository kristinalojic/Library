using Library.Models.Entities;
using Library.ViewModels.Employee;
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

namespace Library.Views.Windows.Employee
{
    /// <summary>
    /// Interaction logic for CheckOutBookWindow.xaml
    /// </summary>
    public partial class CheckOutBookWindow : Window
    {
        public CheckOutBookWindow(EmployeeBooksViewModel model, Book book)
        {
            InitializeComponent();
            DataContext = new CheckOutViewModel(this, book, model);
        }
    }
}
