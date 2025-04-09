using Library.ViewModels.Admin;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library.Views.Controls.Employees
{
    /// <summary>
    /// Interaction logic for UsersUserControl.xaml
    /// </summary>
    public partial class UsersUserControl : UserControl
    {
        public UsersUserControl()
        {
            InitializeComponent();
            this.Loaded += MembersUserControl_Loaded;
        }

        private async void MembersUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MembersViewModel viewModel = await MembersViewModel.CreateAsync();
            DataContext = viewModel;
        }
    }
}
