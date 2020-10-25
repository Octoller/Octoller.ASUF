using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Octoller.ASUF.Desktop.Support {

    public class EnumToArrayConvert : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            Enum.GetValues(value as Type);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            null;
    }
}
