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
    class MyOrdersPageViewModel : ViewModelBase
    {
        DataContext db;
        User currentUser;
        RelayCommand reloadOrdersCommand;

        public ObservableCollection<Order> Orders { get; set; } 
        public ICollectionView OrdersView { get; set; }
        public RelayCommand ReloadOrdersCommand
        { 
            get
            {
                return reloadOrdersCommand ??
                    (reloadOrdersCommand = new RelayCommand(obj =>
                    {
                        SetOrders();
                    }));
            }
        }

        void SetOrders()
        {
            Orders.Clear();
            foreach (var x in currentUser.Orders)
            {
                Orders.Add(x);
            }
        }

        public MyOrdersPageViewModel()
        {
            db = AppData.db;
            currentUser = AppData.CurrentUser;
            Orders = new ObservableCollection<Order>();
            SetOrders();
            OrdersView = CollectionViewSource.GetDefaultView(Orders);
            OrdersView.GroupDescriptions.Add(new PropertyGroupDescription("DateWithoutTime"));
        }
    }
}
