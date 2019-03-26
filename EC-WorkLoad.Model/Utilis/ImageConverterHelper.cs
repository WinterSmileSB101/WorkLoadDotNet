using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace EC_WorkLoad.Model.Utilis
{
    public class ImageConverterHelper
    {
        /// <summary>
        /// bitmap convert to bitmapImage
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapImage BitmapConvertToBitMapImage(Bitmap bitmap, ImageFormat format) {
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, format);

                stream.Position = 0;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
                // Force the bitmap to load right now so we can dispose the stream.
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
        }

        /// <summary>
        /// bitmap convert to Icon
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Icon BitmapConvertToIcon(Bitmap bitmap,ImageFormat format) {
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, format);
                return new Icon(stream);
            }
        }
    }
}
