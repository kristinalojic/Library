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
    /// Interaction logic for EmployeesUserControl.xaml
    /// </summary>
    public partial class EmployeesUserControl : UserControl
    {
        public EmployeesUserControl(int id)
        {
            InitializeComponent();
            this.Loaded += (s, e) => EmployeesUserControl_Loaded(s, e, id);
        }

        private async void EmployeesUserControl_Loaded(object sender, RoutedEventArgs e, int id)
        {
            EmployeesViewModel viewModel = await EmployeesViewModel.CreateAsync(id);
            DataContext = viewModel; 
        }
    }
}
