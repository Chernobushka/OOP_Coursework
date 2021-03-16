using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Canteen.Models;

namespace Canteen
{
    class NewOrderPageViewModel : ViewModelBase
    {
        DataContext db;
        User currentUser;
        Menu selectedMenu;
        RelayCommand addDishCommand;
        RelayCommand deleteDishCommand;
        RelayCommand saveOrderCommand;

        MenuDetail selectedMenuItem;
        MenuDetail selectedDish;
        int orderPrice;

        public int OrderPrice
        {
            get { return orderPrice; }
            set
            {
                orderPrice = value;
                OnPropertyChanged();
            }
        }

        public MenuDetail SelectedMenuItem
        {
            get { return selectedMenuItem; }
            set
            {
                selectedMenuItem = value;
                OnPropertyChanged();
            }
        }
        public MenuDetail SelectedDish
        {
            get { return selectedDish; }
            set
            {
                selectedDish = value;
                OnPropertyChanged();
            }
        }


        public ICollectionView NewOrderView { get; set; }
        public ICollectionView CurrentMenuView { get; set; }
        public ObservableCollection<MenuDetail> CurrentOrder { get; set; }
        public ObservableCollection<MenuDetail> CurrentMenu { get; set; }

        public RelayCommand SaveOrderCommand
        {
            get
            {
                return saveOrderCommand ?? (
                    saveOrderCommand = new RelayCommand(obj =>
                    {
                        if (CurrentOrder.Count != 0)
                        {
                            Order order = new Order() { Date = DateTime.Now };
                            foreach (var x in CurrentOrder)
                            {
                                order.OrderDetails.Add(new OrderDetail { Dish = x.Dish});
                            }
                            currentUser.Orders.Add(order);
                            db.SaveChanges();
                            CurrentOrder.Clear();
                        }
                    }));
            }
        }

        public RelayCommand AddDishCommand
        {
            get
            {
                return addDishCommand ?? (
                    addDishCommand = new RelayCommand(obj =>
                    {
                        if (SelectedMenuItem != null)
                        {
                            CurrentOrder.Add(new MenuDetail() { Dish = SelectedMenuItem.Dish });
                        }
                    }));
            }
        }

        public RelayCommand DeleteDishCommand
        {
            get
            {
                return deleteDishCommand ?? (
                    deleteDishCommand = new RelayCommand(obj =>
                    {
                        if (SelectedDish != null)
                        {
                            CurrentOrder.Remove(SelectedDish);
                            SelectedDish = null;
                        }
                    }));
            }
        }

        void SetMenu()
        {
            CurrentMenu.Clear();
            foreach(var x in selectedMenu.MenuDetails)
            {
                CurrentMenu.Add(x);
            }
        }


        public NewOrderPageViewModel()
        {
            db = AppData.db;
            currentUser = AppData.CurrentUser;
            CurrentOrder = new ObservableCollection<MenuDetail>();
            CurrentMenu = new ObservableCollection<MenuDetail>();
            if (db.Menus.Where(s => DbFunctions.TruncateTime(s.Date) == DbFunctions.TruncateTime(DateTime.Now)).Count() == 0)
            {
                Menu menu = new Menu { Date = DateTime.Now };
                db.Menus.Add(menu);
                db.SaveChanges();
                selectedMenu = menu;
            }
            else
            {
                selectedMenu = db.Menus.Where(s => DbFunctions.TruncateTime(s.Date) == DbFunctions.TruncateTime(DateTime.Now)).First();
            }
            SetMenu();

            NewOrderView = CollectionViewSource.GetDefaultView(CurrentOrder);
            NewOrderView.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
            CurrentMenuView = CollectionViewSource.GetDefaultView(CurrentMenu);
            CurrentMenuView.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
            CurrentOrder.CollectionChanged += CurrentOrder_CollectionChanged;
        }

        private void CurrentOrder_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            int price = 0;
            foreach(var x in CurrentOrder)
            {
                price += x.Dish.Price ?? 0;
            }
            OrderPrice = price;
        }
    }
}
