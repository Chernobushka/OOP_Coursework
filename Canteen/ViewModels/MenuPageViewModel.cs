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
    class MenuPageViewModel : ViewModelBase
    {
        DataContext db;
        User currentUser;
        RelayCommand getMenuCommand;
        Menu selectedMenu;

        public ICollectionView DetailsView { get; set; }
        public ObservableCollection<MenuDetail> Details { get; set; }

        public Menu SelectedMenu
        {
            get { return selectedMenu; }
            set
            {
                selectedMenu = value;
                OnPropertyChanged();
            }
        }

       

        public RelayCommand GetMenuCommand
        {
            get
            {
                return getMenuCommand ??
                    (getMenuCommand = new RelayCommand(obj =>
                    {
                        int DayOfWeek = Convert.ToInt32((string)obj);
                        SetMenu(AppData.GetDate(DayOfWeek));
                    }));
            }
        }

        void SetMenu(DateTime date)
        {
            //SelectedMenuItems.Clear();
            var x = db.Menus.Where(s => DbFunctions.TruncateTime(s.Date) == DbFunctions.TruncateTime(date));
            Details.Clear();
            if (x.Count() == 0)
            {
                Models.Menu menu = new Models.Menu { Date = date };
                db.Menus.Add(menu);
                db.SaveChanges();
                //Details = menu.MenuDetails;
                SelectedMenu = menu;
            }
            else
            {
                SelectedMenu = x.First();
                //Details = SelectedMenu.MenuDetails;
            }
            foreach (var xq in SelectedMenu.MenuDetails)
            {
                Details.Add(xq);
            }
            if (DetailsView != null)
                DetailsView.Refresh();
            OnPropertyChanged("SelectedMenuItems");
            //SelectedMenuItems = null;
            //SelectedMenuItems = new ObservableCollection<MenuDetail>(SelectedMenu.MenuDetails);
            
        }

        public MenuPageViewModel()
        {
            db = new DataContext();
            currentUser = AppData.CurrentUser;
            Details = new ObservableCollection<MenuDetail>();
            SetMenu(DateTime.Now);
            
            DetailsView = CollectionViewSource.GetDefaultView(Details);
            DetailsView.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
        }

        ~MenuPageViewModel()
        {
            db.Dispose();
        }
    }
}
