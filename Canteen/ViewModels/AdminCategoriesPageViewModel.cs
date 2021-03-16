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
    class AdminCategoriesPageViewModel : ViewModelBase
    {
        DataContext db;
        User currentUser;
        RelayCommand newCategoryCommand;
        RelayCommand changeCategoryCommand;
        RelayCommand deleteCategoryCommand;
        Category selectedCategory;

        public RelayCommand NewCategoryCommand
        {
            get
            {
                return newCategoryCommand ??
                    (newCategoryCommand = new RelayCommand(obj =>
                    {
                        AdminWindowPageController.SetChangeCategoryPage(null);
                    }));
            }
        }

        public RelayCommand ChangeCategoryCommand
        {
            get
            {
                return changeCategoryCommand ??
                    (changeCategoryCommand = new RelayCommand(obj =>
                    {
                        AdminWindowPageController.SetChangeCategoryPage(SelectedCategory);
                    }));
            }
        }

        public RelayCommand DeleteCategoryCommand
        { 
            get
            {
                return deleteCategoryCommand ??
                    (deleteCategoryCommand = new RelayCommand(obj =>
                    {
                        if (SelectedCategory != null)
                        {
                            db.Categories.Remove(SelectedCategory);
                            db.SaveChanges();
                            SetCategories();
                            SelectedCategory = null;
                        }
                    }));
            }
        }

        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                OnPropertyChanged();
            }
        }

        void SetCategories()
        {
            Categories.Clear();
            foreach (var x in db.Categories)
            {
                Categories.Add(x);
            }
        }

        public ObservableCollection<Category> Categories { get; set; }

        public AdminCategoriesPageViewModel()
        {
            db = AppData.db;
            currentUser = AppData.CurrentUser;
            Categories = new ObservableCollection<Category>();
            SetCategories();
        }
    }
}
