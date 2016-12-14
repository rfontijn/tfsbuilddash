using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using TfsBuildMonitor.Core.ViewModels;

namespace TfsBuildMonitor.WPF.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = new SolidColorBrush(Colors.Green);
            Debugger.Log(0, "", value?.ToString() + "\r\n");
            switch (value?.ToString())
            {
                case "inprogress":
                case "":
                    break;
                case "partiallysucceeded":
                    brush.Color = Colors.Orange;
                    break;
                case "stopped":
                    brush.Color = Colors.HotPink;
                    break;
                case "failed":
                    brush.Color = Colors.Red;
                    break;
                case "succeeded":
                    break;
                default:
                    brush.Color = Colors.DodgerBlue;
                    break;
            }
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
