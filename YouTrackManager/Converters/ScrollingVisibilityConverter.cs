using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace YouTrackManager.Converters
{
    public class ScrollingVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(x => x == DependencyProperty.UnsetValue)) return Visibility.Collapsed;

            var computedVerticalScrollBarVisibility = (Visibility)values[0];
            var verticalOffset = (double)values[1];
            var extentHeight = (double)values[2];
            var viewportHeight = (double)values[3];

            if (parameter.ToString().Equals("Bottom"))
            {
                bool isAtBottom = verticalOffset >= extentHeight - viewportHeight;
                if (isAtBottom)
                {
                    return Visibility.Collapsed;
                }
            }
            else
            {
                bool isAtTop = verticalOffset == 0;
                if (isAtTop)
                {
                    return Visibility.Collapsed;
                }
            }

            return computedVerticalScrollBarVisibility;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
