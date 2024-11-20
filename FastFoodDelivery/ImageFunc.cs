using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FastFoodDelivery
{
     static class ImageFunc
    {

        public static BitmapImage StandartImage()
        {
            return LoadImageFromFile("Images/dish.png");
        }
        public static BitmapImage ConvertByteArrayToImage(byte[] imageData)
        {
            string defaultImagePath = "Images/dish.png";

            if (imageData == null || imageData.Length == 0)
            {
                return LoadImageFromFile(defaultImagePath);
            };

            using (var stream = new MemoryStream(imageData))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze(); // Optional: freeze to use in different threads

                return bitmapImage;
            }
        }
        public static BitmapImage LoadImageFromFile(string imagePath)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(imagePath, UriKind.Relative); // Use relative path
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            bitmapImage.Freeze(); // Optional

            return bitmapImage;
        }
        public static byte[] GenerateThumbnail(int width, int height, byte[] ImageData)
        {
            string defaultImagePath = "Images/dish.png";

            // Если нет изображения, используем изображение по умолчанию
            if (ImageData == null || ImageData.Length == 0)
            {
                return LoadDefaultImageAsByteArray(defaultImagePath);

            }

            using (var stream = new MemoryStream(ImageData))
            {
                using (var originalImage = Image.FromStream(stream))
                {
                    // Создание миниатюры
                    var thumbnail = new Bitmap(width, height);

                    using (var graphics = Graphics.FromImage(thumbnail))
                    {
                        graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                        // Рисуем сжатое изображение
                        graphics.DrawImage(originalImage, 0, 0, width, height);
                    }

                    // Сохранение миниатюры в byte[]
                    using (var thumbnailStream = new MemoryStream())
                    {
                        thumbnail.Save(thumbnailStream, ImageFormat.Png); // Можно задать нужный формат
                        return thumbnailStream.ToArray();
                    }
                }
            }
        }

        private static byte[] LoadDefaultImageAsByteArray(string imagePath)
        {
            if (!File.Exists(imagePath))
                throw new FileNotFoundException("Image file not found.", imagePath);

            return File.ReadAllBytes(imagePath);
        }

    }
}
