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
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Canteen
{
    class AdminChangeDishPageViewModel : ViewModelBase
    {
        DataContext db;
        User currentUser;
        RelayCommand saveCommand;
        RelayCommand selectImageCommand;
        Dish selectedDish;
        bool createNewDish;
        

        public string Name
        {
            get { return SelectedDish.Name; }
            set
            {
                SelectedDish.Name = value;
                OnPropertyChanged();
            }
        }
        public int Price
        {
            get { return SelectedDish.Price ?? 0; }
            set
            {
                SelectedDish.Price = value;
                OnPropertyChanged();
            }
        }
        public Category Category
        {
            get { return SelectedDish.Category1; }
            set
            {
                SelectedDish.Category1 = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get { return SelectedDish.Description; }
            set
            {
                SelectedDish.Description = value;
                OnPropertyChanged();
            }
        }
        public string Image
        {
            get { return SelectedDish.Image; }
            set
            {
                SelectedDish.Image = value;
                OnPropertyChanged();
            }
        }
        public BitmapSource BitmapImage
        {
            get { return AppData.GetBitmapImage(AppData.ConvertImage(Image)); }
        }
        public Dish SelectedDish
        {
            get { return selectedDish; }
            set
            {
                selectedDish = value;
                OnPropertyChanged();
            }
        }
        
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(obj =>
                    {
                        if (createNewDish)
                        {
                            db.Dishes.Add(selectedDish);
                            db.SaveChanges();
                            AdminWindowPageController.SetDishesPage();
                        }
                        else
                        {
                            db.SaveChanges();
                            AdminWindowPageController.SetDishesPage();
                        }
                    }));
            }
        }
        public RelayCommand SelectImageCommand
        {
            get 
            {
                return selectImageCommand ??
                    (selectImageCommand = new RelayCommand(obj =>
                    {
                        OpenFileDialog fileDialog = new OpenFileDialog();
                        fileDialog.Filter = "Image|*.jpg|All files (*.*)|*.*";
                        fileDialog.ShowDialog();
                        Image = AppData.ImageToBase64(fileDialog.FileName);
                    }));
            }
        }

        public ObservableCollection<Category> Categories { get; set; }
               
        public AdminChangeDishPageViewModel(Dish dish)
        {
            db = AppData.db;
            currentUser = AppData.CurrentUser;
            Categories = new ObservableCollection<Category>();
            if (dish == null)
            {
                createNewDish = true;
                selectedDish = new Dish();
            }
            else
            {
                selectedDish = dish;
            }
            foreach(var x in db.Categories)
            {
                Categories.Add(x);
            }
        }

    }
}
