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
            }
        }
    }
}
