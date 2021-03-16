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
    class CategoryNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (String.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "Введите название категории");
            }

            var categories = AppData.db.Categories.Where(s => s.Name == (string)value);
            if (categories.Count() > 0)
            {
                return new ValidationResult(false, "Блюдо с таким именем уже существует");
            }
            return ValidationResult.ValidResult;
        }
    }
}
