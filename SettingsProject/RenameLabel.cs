﻿using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft;

#nullable enable

namespace SettingsProject
{
    [TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_TextBlock", Type = typeof(TextBlock))]
    public sealed class RenameLabel : Control
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(RenameLabel),
            new PropertyMetadata("", (d, e) => { }));

        public static readonly DependencyProperty IsRenamingProperty = DependencyProperty.Register(
            nameof(IsRenaming),
            typeof(bool),
            typeof(RenameLabel),
            new PropertyMetadata(false, (d, e) => ((RenameLabel)d).OnIsRenamingChanged((bool)e.NewValue)));

        private TextBox? _textBox;
        private TextBlock? _textBlock;
        private string? _previousValue;

        static RenameLabel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RenameLabel), new FrameworkPropertyMetadata(typeof(RenameLabel)));
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool IsRenaming
        {
            get => (bool)GetValue(IsRenamingProperty);
            set => SetValue(IsRenamingProperty, value);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _textBox = (TextBox?)GetTemplateChild("PART_TextBox");
            _textBlock = (TextBlock?)GetTemplateChild("PART_TextBlock");

            Assumes.NotNull(_textBlock);
            Assumes.NotNull(_textBox);

            _textBox.LostFocus += delegate { IsRenaming = false; };
            _textBox.KeyUp += (sender, args) =>
            {
                if (args.Key == Key.Escape)
                {
                    Assumes.NotNull(_previousValue);

                    // roll back to previous value
                    Text = _previousValue;
                    _textBox.Text = _previousValue;

                    IsRenaming = false;
                    args.Handled = true;
                }
                else if (args.Key == Key.Enter)
                {
                    IsRenaming = false;
                    args.Handled = true;
                }
            };
        }

        private void OnIsRenamingChanged(bool newValue)
        {
            Assumes.NotNull(_textBlock);
            Assumes.NotNull(_textBox);

            if (newValue)
            {
                _previousValue = Text;
                _textBlock.Visibility = Visibility.Collapsed;
                _textBox.Visibility = Visibility.Visible;
                _textBox.Focus();
                _textBox.SelectAll();
            }
            else
            {
                _previousValue = null;
                _textBlock.Visibility = Visibility.Visible;
                _textBox.Visibility = Visibility.Collapsed;
            }
        }
    }
}
