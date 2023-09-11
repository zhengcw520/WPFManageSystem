using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MS.Client.Common
{

    public static class PasswordHelper
    {
        #region 密码附加属性
        public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.RegisterAttached("Password",
        typeof(string), typeof(PasswordHelper),
        new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach",
            typeof(bool), typeof(PasswordHelper), new PropertyMetadata(false, Attach));

        private static readonly DependencyProperty IsUpdatingProperty =
           DependencyProperty.RegisterAttached("IsUpdating", typeof(bool),
           typeof(PasswordHelper));


        public static void SetAttach(DependencyObject dp, bool value)
        {
            dp.SetValue(AttachProperty, value);
        }

        public static bool GetAttach(DependencyObject dp)
        {
            return (bool)dp.GetValue(AttachProperty);
        }

        public static string GetPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject dp, string value)
        {
            dp.SetValue(PasswordProperty, value);
        }

        private static bool GetIsUpdating(DependencyObject dp)
        {
            return (bool)dp.GetValue(IsUpdatingProperty);
        }

        private static void SetIsUpdating(DependencyObject dp, bool value)
        {
            dp.SetValue(IsUpdatingProperty, value);
        }

        private static void OnPasswordPropertyChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            passwordBox.PasswordChanged -= PasswordChanged;

            if (!(bool)GetIsUpdating(passwordBox))
            {
                passwordBox.Password = (string)e.NewValue;
            }
            passwordBox.PasswordChanged += PasswordChanged;
        }

        private static void Attach(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;

            if (passwordBox == null)
                return;

            if ((bool)e.OldValue)
            {
                passwordBox.PasswordChanged -= PasswordChanged;
            }

            if ((bool)e.NewValue)
            {
                passwordBox.PasswordChanged += PasswordChanged;
            }
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            SetIsUpdating(passwordBox, true);
            SetPassword(passwordBox, passwordBox.Password);
            SetIsUpdating(passwordBox, false);
        }
        #endregion

        #region 密码占位符(自带密码控件没有占位符功能)
        public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.RegisterAttached("Placeholder", typeof(string), typeof(PasswordHelper), new FrameworkPropertyMetadata(null, OnPlaceholderChanged));

        public static string GetPlaceholder(DependencyObject obj)
        {
            return (string)obj.GetValue(PlaceholderProperty);
        }

        public static void SetPlaceholder(DependencyObject obj, string value)
        {
            obj.SetValue(PlaceholderProperty, value);
        }

        private static void OnPlaceholderChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is PasswordBox passwordBox))
            {
                return;
            }

            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()))
            {
                passwordBox.GotFocus += RemovePlaceholder;
                passwordBox.LostFocus += ShowPlaceholder;
                passwordBox.PasswordChanged += HidePlaceholder;
                ShowPlaceholder(passwordBox, null);
            }
            else
            {
                passwordBox.ClearValue(PlaceholderProperty);
                passwordBox.GotFocus -= RemovePlaceholder;
                passwordBox.LostFocus -= ShowPlaceholder;
                passwordBox.PasswordChanged -= HidePlaceholder;
            }
        }

        private static void ShowPlaceholder(object sender, RoutedEventArgs e)
        {
            if (!(sender is PasswordBox passwordBox))
            {
                return;
            }

            if (string.IsNullOrEmpty(passwordBox.Password))
            {
                passwordBox.Tag = passwordBox.Password;
                passwordBox.Password = GetPlaceholder(passwordBox);
                passwordBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private static void HidePlaceholder(object sender, RoutedEventArgs e)
        {
            if (!(sender is PasswordBox passwordBox))
            {
                return;
            }

            if (passwordBox.Tag != null && string.IsNullOrEmpty(passwordBox.Password))
            {
                passwordBox.Password = passwordBox.Tag.ToString();
                passwordBox.Tag = null;
                passwordBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private static void RemovePlaceholder(object sender, RoutedEventArgs e)
        {
            if (!(sender is PasswordBox passwordBox))
            {
                return;
            }

            if (passwordBox.Tag != null)
            {
                passwordBox.Password = passwordBox.Tag.ToString();
                passwordBox.Tag = null;
            }
        }
        #endregion

        #region 密码框清除附加属性
        public static readonly DependencyProperty ClearButtonProperty =
        DependencyProperty.RegisterAttached("ClearButton", typeof(bool), typeof(PasswordHelper), new FrameworkPropertyMetadata(false, OnClearButtonChanged));

        public static bool GetClearButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(ClearButtonProperty);
        }

        public static void SetClearButton(DependencyObject obj, bool value)
        {
            obj.SetValue(ClearButtonProperty, value);
        }

        private static void OnClearButtonChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid layoutRoot))
            {
                return;
            }

            var passwordBox = FindChild<PasswordBox>(layoutRoot);
            if (passwordBox == null)
            {
                throw new ArgumentException("The PasswordBox element cannot be found in the specified control hierarchy.");
            }

            if ((e.NewValue != null && (bool)e.NewValue) || e.OldValue == null)
            {
                var button = new Button
                {
                    Style = Application.Current.FindResource("ClearButtonStyle") as Style,
                    Visibility = string.IsNullOrEmpty(passwordBox.Password) ? Visibility.Collapsed : Visibility.Visible
                };

                button.Click += (o, ea) =>
                {
                    passwordBox.Clear();
                };

                layoutRoot.Children.Add(button);

                passwordBox.PasswordChanged += (o, ea) =>
                {
                    button.Visibility = string.IsNullOrEmpty(passwordBox.Password) ? Visibility.Collapsed : Visibility.Visible;
                };
            }
            else
            {
                var button = FindChild<Button>(layoutRoot, "ClearButton");
                if (button != null)
                {
                    layoutRoot.Children.Remove(button);
                }

                passwordBox.PasswordChanged -= (o, ea) =>
                {
                    button!.Visibility = string.IsNullOrEmpty(passwordBox.Password) ? Visibility.Collapsed : Visibility.Visible;
                };
            }
        }

        private static TChild FindChild<TChild>(DependencyObject obj, string name = null)
            where TChild : FrameworkElement
        {
            int count = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child.GetType() == typeof(TChild) && (name == null || child.GetValue(FrameworkElement.NameProperty).ToString() == name))
                {
                    return (TChild)child;
                }

                child = FindChild<TChild>(child, name);
                if (child != null)
                {
                    return (TChild)child;
                }
            }

            return null;
        }
        #endregion
    }
}
