using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library.ViewModels
{
     public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return false;

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected static void ChangeLanguage(string languageCode)
        {
            try
            {
                var culture = new CultureInfo(languageCode.ToLower());
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

                var dict = new ResourceDictionary();
                switch (languageCode)
                {
                    case "EN":
                        dict.Source = new Uri("Resources/StringResources.en.xaml", UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri("Resources/StringResources.sr.xaml", UriKind.Relative);
                        break;
                }

                var oldDict = (from d in Application.Current.Resources.MergedDictionaries
                               where d.Source != null && d.Source.OriginalString.StartsWith("Resources/StringResources.")
                               select d).FirstOrDefault();

                if (oldDict != null)
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);

                Application.Current.Resources.MergedDictionaries.Add(dict);
            }
            catch (Exception ex)
            {
            }
        }

        protected string TryGetResource(string key)
        {
            if (Application.Current.Resources.Contains(key))
                return Application.Current.Resources[key]?.ToString() ?? string.Empty;
            return string.Empty;
        }
    }
}
