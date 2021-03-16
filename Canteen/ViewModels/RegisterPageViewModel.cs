using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Canteen.Models;
using System.Security.Cryptography;

namespace Canteen
{
    class RegisterPageViewModel : ViewModelBase
    {
        User NewUser { get; set; }
        DataContext db = AppData.db;
        RelayCommand registerCommand;

        public string UserName
        {
            get { return NewUser.Name; }
            set
            {
                NewUser.Name = value;
                OnPropertyChanged();
            }
        }
        public string UserEmail
        {
            get { return NewUser.Email; }
            set
            {
                NewUser.Email = value;
                OnPropertyChanged();
            }
        }
        public string UserLogin
        { 
            get { return NewUser.Login; }
            set
            {
                NewUser.Login = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand RegisterCommand
        { 
            get
            {
                return registerCommand ??
                    (registerCommand = new RelayCommand(obj =>
                    {
                        SHA256 sha = SHA256.Create();
                        var passwordBox = (PasswordBox)obj;
                        if (UserName != "" && UserEmail != "" && UserLogin != "" && passwordBox.Password != "")
                        {
                            NewUser.RoleId = 2;
                            NewUser.Password = Encoding.Unicode.GetString(sha.ComputeHash(Encoding.Unicode.GetBytes(passwordBox.Password)));
                            db.Users.Add(NewUser);
                            db.SaveChanges();
                            passwordBox.Password = String.Empty;
                            UserName = String.Empty;
                            UserEmail = String.Empty;
                            UserLogin = String.Empty;
                        }
                    }));
            }
        }

        public RegisterPageViewModel()
        {
            NewUser = new User();
        }
    }
}
