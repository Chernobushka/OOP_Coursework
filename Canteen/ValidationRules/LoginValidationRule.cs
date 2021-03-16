using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Canteen
{
    public class LoginValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (String.IsNullOrEmpty((string)value)) 
                return new ValidationResult(false, "Введите имя пользователя");
            if (AppData.db.Users.Where(s => s.Login == (string)value).Count() > 0)
            {
                return new ValidationResult(false, "Такой пользователь уже существует");
            }
            return ValidationResult.ValidResult;
        }
    }
}
