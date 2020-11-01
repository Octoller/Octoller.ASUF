/*
 * **************************************************************************************************************************
 *     _    ____  _   _ _____ 
 *    / \  / ___|| | | |  ___|
 *   / _ \ \___ \| | | | |_   
 *  / ___ \ ___) | |_| |  _|  
 * /_/   \_\____/ \___/|_|  
 * 
 * Octoller.ASUF
 * Desctop.WPF
 * 24.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using Octoller.ASUF.Kernel.ServiceObjects;
using System.Globalization;
using System.Windows.Data;
using System.Linq;
using System;

namespace Octoller.ASUF.DesktopApp.Support {

    public class EnumToArrayConvert : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            Enum.GetValues(value as Type);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            null;
    }

    public class EnumToItemConvert : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (int)((ReasonCreatingFolder)value).GetHashCode();

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            (ReasonCreatingFolder)Enum.GetValues(targetType).GetValue((int)value);
    }

    public class ExtensionArrayToStringConvert : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            if (value is null) {
                return string.Empty;
            }

            return (string)String.Join("; ", (string[])value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            ((string)value).Replace(" ", "")
            .Split(new[] { ";", }, StringSplitOptions.RemoveEmptyEntries)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToArray();
    }

    public class CheckNumberFolder : IValueConverter {

public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {

            if (value != null && value is string input) {
                if (int.TryParse(input, out int i)) {
                    if (i > 0) {
                        return i;
                    }
                }
            }

            return 0;
        }
    }
}
