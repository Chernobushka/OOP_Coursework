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

    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? DishId { get; set; }
        public virtual Dish Dish { get; set; }
        public virtual Order Order { get; set; }

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

        public BitmapSource Image
        {
            get
            {
                //return Dish.ConvertedImage;
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
        }
    }
}
