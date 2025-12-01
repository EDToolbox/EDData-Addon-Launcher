using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Elite_Dangerous_Addon_Launcher_V2.Localization
{
    /// <summary>
    /// Markup extension for easy localization in XAML.
    /// Usage: {loc:Localize Key=MainWindow_AddApp}
    /// </summary>
    [MarkupExtensionReturnType(typeof(string))]
    public class LocalizeExtension : MarkupExtension
    {
        public string Key { get; set; } = string.Empty;

        public LocalizeExtension() { }

        public LocalizeExtension(string key)
        {
            Key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(Key))
                return "[Missing Key]";

            var value = Properties.Resources.ResourceManager.GetString(Key, CultureInfo.CurrentUICulture);
            return value ?? $"[{Key}]";
        }
    }

    /// <summary>
    /// Converter for binding to localized resources.
    /// </summary>
    public class LocalizationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string key)
            {
                var result = Properties.Resources.ResourceManager.GetString(key, CultureInfo.CurrentUICulture);
                return result ?? $"[{key}]";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Static helper class for programmatic localization access.
    /// </summary>
    public static class Strings
    {
        /// <summary>
        /// Gets a localized string by key.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <returns>The localized string or the key in brackets if not found.</returns>
        public static string Get(string key)
        {
            var value = Properties.Resources.ResourceManager.GetString(key, CultureInfo.CurrentUICulture);
            return value ?? $"[{key}]";
        }

        /// <summary>
        /// Gets a localized string with format arguments.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>The formatted localized string.</returns>
        public static string Format(string key, params object[] args)
        {
            var format = Get(key);
            try
            {
                return string.Format(format, args);
            }
            catch
            {
                return format;
            }
        }

        /// <summary>
        /// Changes the application's UI culture.
        /// </summary>
        /// <param name="cultureName">Culture name (e.g., "de", "en", "fr").</param>
        public static void SetCulture(string cultureName)
        {
            var culture = new CultureInfo(cultureName);
            CultureInfo.CurrentUICulture = culture;
            CultureInfo.CurrentCulture = culture;

            // For WPF, we also need to set the thread culture
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
        }

        /// <summary>
        /// Gets the current UI culture name.
        /// </summary>
        public static string CurrentCulture => CultureInfo.CurrentUICulture.Name;
    }
}
