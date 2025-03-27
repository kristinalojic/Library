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
        public AdminWindow()
        {
            InitializeComponent();
            ListBox1.SelectedItem = EmployeesMenu;
            EmployeesMenu.IsSelected = true;
            MainContentControl.Content = new EmployeesUserControl();
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
                                MainContentControl.Content = new EmployeesUserControl();
                                break;
                            case "Books":
                                MainContentControl.Content = new BooksUserControl();
                                break;
                            case "Settings":
                                MainContentControl.Content = new SettingsUserControl();
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
