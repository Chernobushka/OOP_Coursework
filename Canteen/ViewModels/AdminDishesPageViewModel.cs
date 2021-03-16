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
    class AdminDishesPageViewModel : ViewModelBase
    {
        DataContext db;
        User currentUser;
        RelayCommand newDishCommand;
        RelayCommand changeDishCommand;
        RelayCommand deleteDishCommand;
        RelayCommand searchCommand;
        Dish selectedDish;
        string searchRequest;

        public Dish SelectedDish
        {
            get { return selectedDish; }
            set
            {
                selectedDish = value;
                OnPropertyChanged();
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

        public string SearchRequest
        {
            get { return searchRequest; }
            set
            {
                searchRequest = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Dish> Dishes { get; set; }
        public ICollectionView DishesView { get; set; }
        void SetDishes()
        {
            Dishes.Clear();
            foreach (var x in db.Dishes)
            {
                Dishes.Add(x);
            }
        }

        public RelayCommand NewDishCommand
        {
            get
            {
                return newDishCommand ??
                    (newDishCommand = new RelayCommand(obj =>
                    {
                        AdminWindowPageController.SetChangeDishPage(null);
                    }));
            }
        }

        public RelayCommand ChangeDishCommand
        {
            get
            {
                return changeDishCommand ??
                    (changeDishCommand = new RelayCommand(obj =>
                    {
                        AdminWindowPageController.SetChangeDishPage(SelectedDish);
                    }));
            }
        }

        public RelayCommand DeleteDishCommand
        {
            get
            {
                return deleteDishCommand ??
                    (deleteDishCommand = new RelayCommand(obj =>
                    {
                        if (SelectedDish != null)
                        {
                            db.Dishes.Remove(SelectedDish);
                            db.SaveChanges();
                            SelectedDish = null;
                            SetDishes();
                        }
                    }));
            }
        }

        public AdminDishesPageViewModel()
        {
            db = AppData.db;
            currentUser = AppData.CurrentUser;
            Dishes = new ObservableCollection<Dish>();
            SetDishes();
            DishesView = CollectionViewSource.GetDefaultView(Dishes);
            DishesView.GroupDescriptions.Add(new PropertyGroupDescription("CategoryName"));
        }

    }
}
