using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Canteen
{
    public static class AppData
    {
        public static Models.DataContext db;
        public static Models.User CurrentUser;
        public static Models.Menu SelectedMenu { get; set; }

        public static DateTime GetDate(int dayOfWeek)
        {
            //0 - Monday, 6 - Sunday

            DateTime Monday = StartOfWeek(DateTime.Now, DayOfWeek.Monday);

            return Monday.AddDays(dayOfWeek);
        }

        public static DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static Image ConvertImage(string Image)
        {
            if (Image != null)
            {
                byte[] imageBytes = Convert.FromBase64String(Image);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                return image;
            }
            return null;
        }

        public static BitmapSource GetBitmapImage(Image ConvertedImage)
        {
            if (ConvertedImage != null)
            {
                var bitmap = new Bitmap(ConvertedImage);
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

        public static string ImageToBase64(string path)
        {
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
        }
    }
}
