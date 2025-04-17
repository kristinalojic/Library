using Library.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library.Views.Controls.Shared
{
    /// <summary>
    /// Interaction logic for SettingsUserControl.xaml
    /// </summary>
    public partial class SettingsUserControl : UserControl
    {
        private readonly int _id;

        public SettingsUserControl(int id)
        {
            InitializeComponent();
            _id = id;
            this.Loaded += SettingsUserControl_Loaded;
        }

        private async void SettingsUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ThemeViewModel viewModel = await ThemeViewModel.CreateAsync(_id);
            DataContext = viewModel;
        }

    }
}
