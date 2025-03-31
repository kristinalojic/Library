using Employee_And_Company_Management.Commands;
using Library.DAO.MySQL;
using Library.Models.Entities;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace Library.ViewModels
{
    class ThemeViewModel : BaseViewModel
    {
        private readonly SettingsDAO _settingsDAO;
        private readonly int _loggedEmployee;

        private string _selectedTheme;
        private string _selectedColor;

        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (SetProperty(ref _selectedTheme, value))
                {
                    ApplyTheme();
                }
            }
        }

        public string SelectedColor
        {
            get => _selectedColor;
            set
            {
                if (SetProperty(ref _selectedColor, value))
                {
                    ApplyColor();
                }
            }
        }

        public ICommand ChangeThemeCommand { get; set; }

        public ICommand ChangeColorCommand { get; set; }

        public ThemeViewModel(int id)
        {
            _loggedEmployee = id;
            _settingsDAO = new SettingsDAO();
            LoadDefaultTheme();
            ChangeThemeCommand = new RelayCommand(param => ChangeTheme(param as string), _ => CanChangeTheme());
            ChangeColorCommand = new RelayCommand(param => ChangeColor(param as string), _ => CanChangeColor());
        }

        private async void LoadDefaultTheme()
        {
            var setting = await _settingsDAO.GetSettingByIdAsync(_loggedEmployee);
            if (setting != null)
            {
                SelectedTheme = setting.Theme;
                SelectedColor = setting.Color;
            }
        }

        private void ApplyTheme()
        {
            var paletteHelper = new PaletteHelper();
            Theme theme = paletteHelper.GetTheme();

            if (SelectedTheme == "Dark")
            {
                theme.SetDarkTheme();
            }
            else
            {
                theme.SetLightTheme();
            }

            paletteHelper.SetTheme(theme);
        }

        private void ApplyColor()
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            if (SelectedColor == "Purple")
            {
                theme.SetPrimaryColor(Color.FromRgb(103, 58, 183));
                theme.SetSecondaryColor(Color.FromRgb(103, 58, 183));
            }
            else if (SelectedColor == "Green")
            {
                theme.SetPrimaryColor(Color.FromRgb(54, 115, 76));
                theme.SetSecondaryColor(Color.FromRgb(54, 115, 76));
            }
            else if (SelectedColor == "Yellow")
            {
                theme.SetPrimaryColor(Color.FromRgb(247, 236, 74));
                theme.SetSecondaryColor(Color.FromRgb(247, 236, 74));
            }

            paletteHelper.SetTheme(theme);
        }

        private void ChangeTheme(string theme)
        {
             SelectedTheme = theme;
            _settingsDAO.SaveSettingsByIdAsync(_loggedEmployee, SelectedTheme, SelectedColor, "English");
        }

        private bool CanChangeTheme() => true;

        private void ChangeColor(string color)
        {
            SelectedColor = color;
            _settingsDAO.SaveSettingsByIdAsync(_loggedEmployee, SelectedTheme, SelectedColor, "English");
        }

        private bool CanChangeColor() => true;
    }
}
