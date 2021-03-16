using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Canteen
{
    class EmailValidationRule : ValidationRule
    {
        // ([A-Za-z0-9]+@[a-zA-Z]+\.[a-zA-Z]+)
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (String.IsNullOrEmpty((string)value))
                return new ValidationResult(false, "Введите электронную почту");
            Regex regex = new Regex(@"([A-Za-z0-9]+@[a-zA-Z]+\.[a-zA-Z]+)");
            if (!regex.IsMatch((string)value))
                return new ValidationResult(false, "Некорректная электронная почта");
            return ValidationResult.ValidResult;
        }
    }
}
