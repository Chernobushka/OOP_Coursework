namespace Canteen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Drawing;
    using System.Windows;
    using System.Windows.Media.Imaging;

    public partial class MenuDetail
    {
        public int Id { get; set; }
        public int? MenuId { get; set; }
        public int? DishId { get; set; }
        public virtual Dish Dish { get; set; }
        public virtual Menu Menu { get; set; }
        public string Name
        {
            get { return Dish.Name; }
        }

        public string Description
        {
            get { return Dish.Description; }
        }

        public int Price
        {
            get { return Dish.Price ?? 0; }
        }

        public string Category
        {
            get { return Dish.Category1.Name; }
        }
        public BitmapSource Image
        {
            get
            {
                //return Dish.ConvertedImage;
                if (Dish.ConvertedImage != null)
                {
                    var bitmap = new Bitmap(Dish.ConvertedImage);
                    IntPtr bmpPt = bitmap.GetHbitmap();
                    BitmapSource bitmapSource =
                     System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                           bmpPt,
                           IntPtr.Zero,
                           Int32Rect.Empty,
                           BitmapSizeOptions.FromEmptyOptions());

                    //freeze bitmapSource and clear memory to avoid memory leaks
                    bitmapSource.Freeze();


                    return bitmapSource;
                }
                return null;
            }
        }
    }
}
