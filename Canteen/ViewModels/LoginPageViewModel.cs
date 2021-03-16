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
using System.Windows;

namespace Canteen
{
    class LoginPageViewModel : ViewModelBase
    {
        private DataContext db = AppData.db;

        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }


        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                    (loginCommand = new RelayCommand(obj =>
                    {
                        PasswordBox pb = (PasswordBox)obj;
                        SHA256 sha = SHA256.Create();
                        string pass = pb.Password;
                        if (Login != "" && pass != "")
                        {
                            var users = db.Users.Where(s => s.Login == Login);
                            if (users.Count() == 0)
                            {
                                MessageBox.Show("Такого пользователя не существует");
                                return;
                            }
                            User user = users.First();
                            if (user.Password == Encoding.Unicode.GetString(sha.ComputeHash(Encoding.Unicode.GetBytes(pass))))
                            {
                                AppData.CurrentUser = user;
                                switch (user.RoleId)
                                {
                                    case 1:
                                        {
                                            WindowsController.ShowAdminWindow();
                                            WindowsController.CloseLoginWindow();
                                            break;
                                        }
                                    case 2:
                                        {
                                           
                                            WindowsController.ShowUserWindow();
                                            WindowsController.CloseLoginWindow();
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Пароль введен неправильно");
                            }
                        }
                       
                    }));
            }
        }

        public LoginPageViewModel()
        {
            
            db = AppData.db;
        }
    }
}
