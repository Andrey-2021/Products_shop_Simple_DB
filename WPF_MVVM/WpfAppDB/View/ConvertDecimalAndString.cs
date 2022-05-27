using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace WpfAppDB.View
{

    /// <summary>
    /// Конвертер Decimal <-> string
    /// </summary>
  //  [ValueConversion(typeof(decimal), typeof(string))]
    public class CostConverter : IValueConverter
    {
        /// <summary>
        /// Конвертируем число в строку
        /// </summary>
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            // Возвращаем строку
            return ((decimal)value).ToString();
        }


        /// <summary>
        /// Конвертируем строку в число
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {

            var str = value as string;

            if (string.IsNullOrEmpty(str)) return 0;

            string newStr = "";
            for (int i = 0; i < str.Length; i++) //перебираем символы в строке
            {
                if (Char.IsDigit(str[i])) newStr += str[i]; //если это цифра, тогда оставляем её
            }

            //если число больше decimal.MaxValue
            if (double.TryParse(newStr, out double num) && num > (double)decimal.MaxValue)
                newStr=newStr.Remove(newStr.Length-1);  //удаляем последний символ

            decimal result;
            if (decimal.TryParse(newStr, System.Globalization.NumberStyles.Any,
                         culture, out result))
            {
                return result;
            };

            return 0; 
        }
    }
    
}
