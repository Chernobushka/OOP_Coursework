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
using System.Windows.Controls;
using Canteen.Models;

namespace Canteen
{
    class AddDishToMenuPageViewModel : ViewModelBase
    {
        DataContext db;
        User currentUser;
        RelayCommand addDishesToMenuCommand;
        RelayCommand searchCommand;
        
        string searchRequest;

        public string SearchRequest
        {
            get { return searchRequest; }
            set
            {
                searchRequest = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddDishesToMenuCommand
        {
            get
            {
                return addDishesToMenuCommand ??
                    (addDishesToMenuCommand = new RelayCommand(obj =>
                    {
                        var selectedDishes = ((System.Collections.IList)obj).Cast<Dish>();
                        foreach (var x in selectedDishes)
                        {
                            if (AppData.SelectedMenu.MenuDetails.Where(s => s.Dish == x).Count() == 0)
                            {

                                MenuDetail menuDetail = new MenuDetail { Dish = x };

                                AppData.SelectedMenu.MenuDetails.Add(menuDetail);
                                db.SaveChanges();
                            }
                        }
                        ((AdminMenuPageViewModel)AdminWindowPageController.adminMenuPage.DataContext).SetMenu(AppData.SelectedMenu.Date);
                        AdminWindowPageController.SetMenuPage();
                    }));
            }
        }

        public RelayCommand SearchCommand
        {
            get
            {
                return searchCommand ??
                    (searchCommand = new RelayCommand(obj =>
                    {
                        if (!String.IsNullOrEmpty(SearchRequest))
                        {
                            DishesView.Filter = s =>
                            {
                                Dish dish = (Dish)s;
                                return dish.Name.ToLower().Contains(SearchRequest.ToLower());
                            };
                            DishesView.Refresh();
                        }
                        else
                        {
                            DishesView.Filter = null;
                            DishesView.Refresh();
                        }
                    }));
            }
        }


        public ObservableCollection<Dish> Dishes { get; set; }
        public ICollectionView DishesView { get; set; }

        public AddDishToMenuPageViewModel()
        {
            db = AppData.db;
            currentUser = AppData.CurrentUser;
            Dishes = new ObservableCollection<Dish>();
            foreach (var x in db.Dishes)
            {
                Dishes.Add(x);
            }
            DishesView = CollectionViewSource.GetDefaultView(Dishes);
            DishesView.GroupDescriptions.Add(new PropertyGroupDescription("CategoryName"));
        }
    }
}
