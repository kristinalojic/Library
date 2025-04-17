using Library.ViewModels;
using Library.Views.Controls.Admin;
using Library.Views.Controls.Shared;
using Library.Views.Windows.Employee;
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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private int _loggedEmployee;
        public AdminWindow(int id)
        {
            InitializeComponent();
            _loggedEmployee = id;
            ListBox1.SelectedItem = EmployeesMenu;
            EmployeesMenu.IsSelected = true;
            this.Loaded += (s, e) => AdminUserControl_Loaded(s, e, id);
        }

        private async void AdminUserControl_Loaded(object sender, RoutedEventArgs e, int id)
        {
            ThemeViewModel viewModel = await ThemeViewModel.CreateAsync(id);
            DataContext = viewModel;
            Thread.Sleep(10);
            MainContentControl.Content = new EmployeesUserControl(id);
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox selectedListBox)
            {
                if (selectedListBox.SelectedItem == null)
                    return;

                ListBox1.SelectionChanged -= ListBox_SelectionChanged;
                ListBox2.SelectionChanged -= ListBox_SelectionChanged;

                if (selectedListBox == ListBox1)
                {
                    ListBox2.SelectedItem = null;
                }
                else if (selectedListBox == ListBox2)
                {
                    ListBox1.SelectedItem = null;
                }

                ListBox1.SelectionChanged += ListBox_SelectionChanged;
                ListBox2.SelectionChanged += ListBox_SelectionChanged;

                var selectedItem = selectedListBox.SelectedItem as ListBoxItem;
                if (selectedItem != null)
                {
                    var stackPanel = selectedItem.Content as StackPanel;
                    var textBlock = stackPanel?.Children
                        .OfType<TextBlock>()
                        .FirstOrDefault();

                    if (textBlock != null)
                    {
                        string selectedText = textBlock.Text;

                        switch (selectedText)
                        {
                            case "Employees":
                                MainContentControl.Content = new EmployeesUserControl(_loggedEmployee);
                                break;
                            case "Books":
                                MainContentControl.Content = new BooksUserControl();
                                break;
                            case "Settings":
                                MainContentControl.Content = new SettingsUserControl(_loggedEmployee);
                                break;
                            case "Log out":
                                var loginWindow = new Login();
                                loginWindow.Show();

                                var adminWindow = Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
                                if (adminWindow != null)
                                {
                                    adminWindow.Close();
                                }
                                break;
                            case "Zaposleni":
                                MainContentControl.Content = new EmployeesUserControl(_loggedEmployee);
                                break;
                            case "Knjige":
                                MainContentControl.Content = new BooksUserControl();
                                break;
                            case "Podešavanja":
                                MainContentControl.Content = new SettingsUserControl(_loggedEmployee);
                                break;
                            case "Odjava":
                                loginWindow = new Login();
                                loginWindow.Show();

                                adminWindow = Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
                                if (adminWindow != null)
                                {
                                    adminWindow.Close();
                                }
                                break;
                            default:
                                MainContentControl.Content = null;
                                break;
                        }
                    }
                }
            }
        }
    }
}
