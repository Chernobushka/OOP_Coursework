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
using System.Windows.Controls;
using Canteen.Views;

namespace Canteen
{ 
    class MainWindowViewModel : ViewModelBase
    {
        RelayCommand newOrderCommand;
        RelayCommand myOrdersCommand;
        RelayCommand menuCommand;
        RelayCommand logoutCommand;
        string userName;
        public string UserName
        { 
            get { return AppData.CurrentUser.Name; }
            set
            {
                AppData.CurrentUser.Name = value;
                OnPropertyChanged();
            }
        }


        Page currentPage;
        Page newOrderPage;
        Page myOrdersPage;
        Page menuPage;

        public RelayCommand NewOrderCommand
        {
            get
            {
                return newOrderCommand ??
                    (newOrderCommand = new RelayCommand(obj =>
                    {
                        CurrentPage = newOrderPage;
                    }));
            }
        }

        public RelayCommand MyOrdersCommand
        {
            get
            {
                return myOrdersCommand ??
                    (myOrdersCommand = new RelayCommand(obj =>
                    {
                        CurrentPage = myOrdersPage;
                    }));
            }
        }

        public RelayCommand MenuCommand
        {
            get
            {
                return menuCommand ??
                    (menuCommand = new RelayCommand(obj =>
                    {
                        CurrentPage = menuPage;
                    }));
            }
        }

        public RelayCommand LogoutCommand
        { 
            get
            {
                return logoutCommand ??
                    (logoutCommand = new RelayCommand(obj =>
                    {
                        WindowsController.CloseUserWindow();
                        WindowsController.ShowLoginWindow();
                    }));
            }
        }

        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            newOrderPage = new NewOrderPage();
            myOrdersPage = new MyOrdersPage();
            menuPage = new MenuPage();

            CurrentPage = newOrderPage;
        }
    }
}
