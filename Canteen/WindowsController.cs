using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Canteen.Views;

namespace Canteen
{
    public static class WindowsController
    {
        public static LoginWindow LoginWindow;
        public static MainWindow UserWindow = null;
        public static AdminWindow AdminWindow = null;

        public static void ShowUserWindow()
        {
            if (UserWindow == null)
                UserWindow = new MainWindow();
            UserWindow.Show();
        }

        public static void ShowLoginWindow()
        {
            if (LoginWindow == null)
                LoginWindow = new LoginWindow();
            LoginWindow.Show();
        }

        public static void ShowAdminWindow()
        {
            if (AdminWindow == null)
                AdminWindow = new AdminWindow();
            AdminWindow.Show();

        }

        public static void CloseLoginWindow()
        {
            LoginWindow.Close();
            LoginWindow = null;
        }

        public static void CloseUserWindow()
        {
            
            UserWindow.Close();
            UserWindow = null;
        }

        public static void CloseAdminWindow()
        {
            
            AdminWindow.Close();
            AdminWindow = null;
        }
    }
}
