using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FastFoodDelivery
{
        public class ItemMenuShort
        {
        public required int MenuItemId { get; set; }
        public required string Name { get; set; }
        public required string ItemType { get; set; }
        public required string ShortDescript { get; set; }
        public required  decimal Price { get; set; }
        public required byte[]  Thumbnails { get; set; }
        public BitmapImage ImageSourceThumbNails
        {
            get
            {
                return ImageFunc.ConvertByteArrayToImage(Thumbnails);
            }
        }

    }

}
