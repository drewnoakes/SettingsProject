using System;
using System.Collections.Immutable;
using System.Windows;
using Microsoft;

#nullable enable

namespace SettingsProject
{
    public partial class App
    {
        private readonly ImmutableArray<Uri> _themes = ImmutableArray.Create(
            new Uri("/Themes/BlueTheme.xaml", UriKind.Relative),
            new Uri("/Themes/DarkTheme.xaml", UriKind.Relative));

        private readonly ResourceDictionary _parentDictionary;
        private int _themeIndex = 1;

        public App()
        {
            InitializeComponent();

            _parentDictionary = new ResourceDictionary();
            Resources.MergedDictionaries.Add(_parentDictionary);
            
            ChangeTheme(_themeIndex);
        }

        public void NextTheme()
        {
            _themeIndex++;
            if (_themeIndex == _themes.Length)
                _themeIndex = 0;
            ChangeTheme(_themeIndex);
        }

        private void ChangeTheme(int index)
        {
            _parentDictionary.MergedDictionaries.Clear();
            _parentDictionary.MergedDictionaries.Add(new ResourceDictionary { Source = _themes[index] });
        }
    }
}
