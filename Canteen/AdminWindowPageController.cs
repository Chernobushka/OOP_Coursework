using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using Canteen.Views;

namespace Canteen
{
    public static class AdminWindowPageController
    {
        public static NavigationService Navigator { get; set; }
        private static Page adminCategoriesPage = new AdminCategoriesPage();
        private static Page adminDishesPage = new AdminDishesPage();
        public static Page adminMenuPage = new AdminMenuPage();
        private static Page adminChangeDishPage = new AdminChangeDishPage();
        private static Page adminChangeCategoryPage = new AdminChangeCategoryPage();
        private static Page addDishToMenuPage = new AddDishToMenuPage();

        public static void SetCategoriesPage()
        {
            adminCategoriesPage.DataContext = new AdminCategoriesPageViewModel();
            Navigator.Navigate(adminCategoriesPage);
        }

        public static void SetMenuPage()
        {
            adminMenuPage.DataContext = new AdminMenuPageViewModel();
            Navigator.Navigate(adminMenuPage);
        }

        public static void SetDishesPage()
        {
            adminDishesPage.DataContext = null;
            adminDishesPage.DataContext = new AdminDishesPageViewModel();
            Navigator.Navigate(adminDishesPage);
        }

        public static void SetChangeDishPage(Models.Dish dish)
        {
            adminChangeDishPage.DataContext = null;
            adminChangeDishPage.DataContext = new AdminChangeDishPageViewModel(dish);
            Navigator.Navigate(adminChangeDishPage);
        }

        public static void SetChangeCategoryPage(Models.Category category)
        {
            adminChangeCategoryPage.DataContext = new AdminChangeCategoryPageViewModel(category);
            Navigator.Navigate(adminChangeCategoryPage);
        }

        public static void SetAddDishToMenuPage()
        {
            addDishToMenuPage.DataContext = null;
            addDishToMenuPage.DataContext = new AddDishToMenuPageViewModel();
            Navigator.Navigate(addDishToMenuPage);
        }
    }
}
