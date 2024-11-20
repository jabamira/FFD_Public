using System.ComponentModel.DataAnnotations;
using System.Windows.Media.Imaging;
namespace FastFoodDelivery


{
    public class User
    {
        public int Id { get; set; }
        public required  string Login { get; set; }
        public required string Password { get; set; }
        public DateTime? TimeRegister { get; set; }
        public bool Admin  { get; set; }

    }
    public class ItemMenu
    {
        [Key]
        public int MenuItemId { get; set; }
        public  int RestaurantId { get; set; }
        public required string Name { get; set; }
        public required string ShortDescript { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required string ItemType { get; set; }
        public required int StockCount { get; set; }
        public required bool ForAdult { get; set; }
        public required bool SpecialOffer { get; set; }
        public required byte[] ImageData { get; set; }
        public required byte[] Thumbnails { get; set; }
        public BitmapImage ImageSource
        {
            get
            {
                if (ImageData != null)
                {
                    return ImageFunc.ConvertByteArrayToImage(ImageData);
                }
                return ImageFunc.StandartImage();
            }
        }
        public BitmapImage ImageSourceThumbNails
        {
            get
            {
                if (Thumbnails != null) 
                {
                    return ImageFunc.ConvertByteArrayToImage(Thumbnails);
                }
                return ImageFunc.StandartImage();


            }
        }

       
        



    }


}
