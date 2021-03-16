using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Interactivity;
using Canteen.Models;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Windows.Controls;

namespace Canteen
{
    class LoginWindowViewModel : ViewModelBase
    {
        RelayCommand loginWindowCommand;
        RelayCommand registerWindowCommand;

        Page LoginPage;
        Page RegisterPage;
        Page currentPage;

        public Page CurrentPage
        { 
            get { return currentPage; }
            set
            {
                currentPage = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand LoginWindowCommand
        {
            get
            {
                return loginWindowCommand ??
                    (loginWindowCommand = new RelayCommand(obj =>
                    {
                        CurrentPage = LoginPage;
                    }));
            }
        }

        public RelayCommand RegisterWindowCommand
        {
            get
            {
                return registerWindowCommand ??
                    (registerWindowCommand = new RelayCommand(obj =>
                    {
                        CurrentPage = RegisterPage;
                    }));
            }
        }

        public LoginWindowViewModel()
        {
            LoginPage = new Views.LoginPage();
            RegisterPage = new Views.RegisterPage();

            CurrentPage = LoginPage;
        }
    }
}
