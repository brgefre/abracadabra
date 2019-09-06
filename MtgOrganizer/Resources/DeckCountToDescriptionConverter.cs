using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MtgOrganizer.Resources
{
    public class DeckCountToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int i = int.Parse(value.ToString());
            return i + (i == 1 ? " deck" : " decks");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}