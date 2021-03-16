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
    class AdminChangeCategoryPageViewModel : ViewModelBase
    {
        DataContext db;
        User currentUser;
        bool createNewCategory;
        Category selectedCategory;
        RelayCommand saveCategory;

        public string Name
        {
            get { return SelectedCategory.Name; }
            set
            {
                SelectedCategory.Name = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveCategoryCommand
        {
            get
            {
                return saveCategory ??
                    (saveCategory = new RelayCommand(obj =>
                    {
                        if (!String.IsNullOrEmpty(Name))
                            if (createNewCategory)
                            {

                                db.Categories.Add(SelectedCategory);
                                db.SaveChanges();
                                AdminWindowPageController.SetCategoriesPage();
                            }
                            else
                            {
                                db.SaveChanges();
                                AdminWindowPageController.SetCategoriesPage();
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

        public AdminChangeCategoryPageViewModel(Category category)
        {
            db = AppData.db;
            currentUser = AppData.CurrentUser;
            if (category == null)
            {
                createNewCategory = true;
                SelectedCategory = new Category();
            }
            else
            {
                SelectedCategory = category;
            }
        }
    }
}
