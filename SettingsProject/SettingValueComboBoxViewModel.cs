﻿using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable enable

namespace SettingsProject
{
    internal sealed class SettingValueComboBoxViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private readonly SettingValue _settingValue;

        public SettingValueComboBoxViewModel(SettingValue settingValue)
        {
            _settingValue = settingValue;

            _settingValue.PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    case nameof(SettingValue.EnumValues):
                        PropertyChanged?.Invoke(this, e);
                        break;
                    case nameof(SettingValue.EvaluatedValue):
                        OnPropertyChanged(nameof(SelectedValue));
                        break;
                }
            };
        }

        public ImmutableArray<SupportedValue> EnumValues => _settingValue.EnumValues;

        public SupportedValue SelectedValue
        {
            get => _settingValue.EnumValues.FirstOrDefault(v => string.Equals(v.Value, _settingValue.EvaluatedValue as string, StringComparison.OrdinalIgnoreCase));
            set
            {
                foreach (var supportedValue in _settingValue.EnumValues)
                {
                    if (string.Equals(supportedValue.Value, value.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        _settingValue.EvaluatedValue = supportedValue.Value;
                        OnPropertyChanged();
                        return;
                    }
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}