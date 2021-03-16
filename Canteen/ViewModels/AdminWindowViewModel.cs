using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Canteen.Views;

namespace Canteen
{
    class AdminWindowViewModel : ViewModelBase
    {
        RelayCommand adminMenuCommand;
        RelayCommand adminDishesCommand;
        RelayCommand adminCategoriesCommand;
        RelayCommand logoutCommand;

        public RelayCommand AdminMenuCommand
        {
            get
            {
                return adminMenuCommand ??
                    (adminMenuCommand = new RelayCommand(obj =>
                    {
                        AdminWindowPageController.SetMenuPage();
                    }));
            }
        }

        public RelayCommand AdminDishesCommand
        {
            get
            {
                return adminDishesCommand ??
                    (adminDishesCommand = new RelayCommand(obj =>
                    {
                        AdminWindowPageController.SetDishesPage();
                    }));
            }
        }

        public RelayCommand AdminCategoriesCommand
        {
            get
            {
                return adminCategoriesCommand ??
                    (adminCategoriesCommand = new RelayCommand(obj =>
                    {
                        AdminWindowPageController.SetCategoriesPage();
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
                        WindowsController.CloseAdminWindow();
                    }));
            }
        }
    }
}
