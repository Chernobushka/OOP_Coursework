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

    public partial class Dish
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dish()
        {
            MenuDetails = new HashSet<MenuDetail>();
            OrderDetails = new HashSet<OrderDetail>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? Price { get; set; }
        public int? Category { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Image ConvertedImage
        {
            get
            {
                //if (Image != null)
                //{
                //    byte[] imageBytes = Convert.FromBase64String(Image);
                //    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                //    ms.Write(imageBytes, 0, imageBytes.Length);
                //    System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                //    return image;
                //}
                //return null;
                return AppData.ConvertImage(Image);
            }
        }
        public BitmapSource BitmapImage
        {
            get
            {
                //return Dish.ConvertedImage;
                //if (ConvertedImage != null)
                //{
                //    var bitmap = new Bitmap(ConvertedImage);
                //    IntPtr bmpPt = bitmap.GetHbitmap();
                //    BitmapSource bitmapSource =
                //     System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                //           bmpPt,
                //           IntPtr.Zero,
                //           Int32Rect.Empty,
                //           BitmapSizeOptions.FromEmptyOptions());

                //    //freeze bitmapSource and clear memory to avoid memory leaks
                //    bitmapSource.Freeze();


                //    return bitmapSource;
                //}
                //return null;
                return AppData.GetBitmapImage(ConvertedImage);
            }
        }
        public string CategoryName
        {
            get
            {
                return Category1.Name;
            }
        }
        public virtual Category Category1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuDetail> MenuDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
