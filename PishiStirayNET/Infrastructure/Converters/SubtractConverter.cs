﻿using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace PishiStirayNET.Infrastructure.Converters
{
    public class SubtractConverter : MarkupExtension, IValueConverter
    {
        public double Value { get; set; }

        public object Convert(object baseValue, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double val = System.Convert.ToDouble(baseValue);
            // Change here if you want other operations
            return val - Value;
        }

        public object ConvertBack(object baseValue, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
