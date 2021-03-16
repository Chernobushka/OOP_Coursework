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
    class AdminMenuPageViewModel : ViewModelBase
    {
        DataContext db;
        User currentUser;
        DateTime selectedDate;
        MenuDetail selectedDish;

        RelayCommand changeDateCommand;
        RelayCommand addDishToMenuCommand;
        RelayCommand deleteDishFromMenuCommand;

        Models.Menu selectedMenu
        {
            get { return AppData.SelectedMenu; }
            set
            {
                AppData.SelectedMenu = value;
            }
        }

        public MenuDetail SelectedDish
        { 
            get
            {
                return selectedDish;
            }
            set
            {
                selectedDish = value;
                OnPropertyChanged();
            }
        }
        

        public RelayCommand AddDishToMenuCommand
        {
            get
            {
                return addDishToMenuCommand ??
                    (addDishToMenuCommand = new RelayCommand(obj =>
                    {
                        AdminWindowPageController.SetAddDishToMenuPage();
                    }));
            }
        } 
        public RelayCommand ChangeDateCommand
        { 
            get
            {
                return changeDateCommand ??
                    (changeDateCommand = new RelayCommand(obj =>
                    {
                        if (SelectedDate != null)
                            SetMenu(SelectedDate);
                    }));
            }
        }
        public RelayCommand DeleteDishFromMenuCommand
        {
            get 
            {
                return deleteDishFromMenuCommand ??
                    (deleteDishFromMenuCommand = new RelayCommand(obj =>
                    {
                        if (SelectedDish != null)
                        {
                            selectedMenu.MenuDetails.Remove(SelectedDish);
                            SelectedDish = null;
                            db.SaveChanges();
                            SetMenu(selectedMenu.Date);
                        }
                    }));
            }
        }


        public void SetMenu(DateTime date)
        {
            CurrentMenu.Clear();
            var x = db.Menus.Where(s => DbFunctions.TruncateTime(s.Date) == DbFunctions.TruncateTime(date));
            if (x.Count() == 0)
            {
                Models.Menu menu = new Models.Menu { Date = date };
                db.Menus.Add(menu);
                db.SaveChanges();
                selectedMenu = menu;
            }
            else
            {
                selectedMenu = x.First();
            }
            foreach(var i in selectedMenu.MenuDetails)
            {
                CurrentMenu.Add(i);
            }
            if (CurrentMenuView != null)
                CurrentMenuView.Refresh();
        }

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged();
            }
        }
        //public ObservableCollection<MenuDetail> CurrentMenu 
        //{ 
        //    get 
        //    {
        //        return AppData.CurrentMenu;
        //    } 
        //    set
        //    {
        //        AppData.CurrentMenu = value;
        //        OnPropertyChanged();
        //    }
        //}
        public ObservableCollection<MenuDetail> CurrentMenu { get; set; }
        public ICollectionView CurrentMenuView { get; set; }

        public AdminMenuPageViewModel()
        {
            db = AppData.db;
            currentUser = AppData.CurrentUser;
            CurrentMenu = new ObservableCollection<MenuDetail>();
            
            SetMenu(DateTime.Now);
            CurrentMenuView = CollectionViewSource.GetDefaultView(CurrentMenu);
            CurrentMenuView.GroupDescriptions.Add(new PropertyGroupDescription("Category"));

        }

    }
}
