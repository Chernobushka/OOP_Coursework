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
    class DishPriceValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int price;
            if (String.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "Введите цену");
            }
            try
            {
                price = Convert.ToInt32((string)value);
            }
            catch
            {
                return new ValidationResult(false, "Некорректный ввод цены");
            }
            if (price < 0)
            {
                return new ValidationResult(false, "Цена не может быть отрицательной");
            }
            return ValidationResult.ValidResult;
        }
    }
}
