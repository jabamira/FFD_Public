using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Diagnostics;


namespace FastFoodDelivery
{
    public static class MenuService
    {


        public static ObservableCollection<ItemMenuShort> ToShorCollect(this IQueryable<ItemMenu> items)
        {
            var result =  items.Select(item => new ItemMenuShort
            {
                MenuItemId = item.MenuItemId,
                Name = item.Name,
                ShortDescript = item.ShortDescript,
                ItemType = item.ItemType,
                Price = item.Price,
                Thumbnails = item.Thumbnails,
                RestaurantId = item.RestaurantId,
                Description = item.Description,
                StockCount = item.StockCount,
                ForAdult = item.ForAdult,
                SpecialOffer = item.SpecialOffer

            }).ToList();

            return new ObservableCollection<ItemMenuShort>(result);
        }

        public static ObservableCollection<ItemMenuShort> GetProducts() 
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return  db.ItemsMenu.ToShorCollect(); 
            }
        }


        public static void AddNewMenuItem(string name, int restaurantId, string shortDescript, string description, decimal price, string itemType, int stockCount, bool forAdult, bool specialOffer, string imagePath = "Images/dish.png")
        {
            byte[] data = File.ReadAllBytes(imagePath);
            var newItem = new ItemMenu
            {
                Name = name,
                RestaurantId = restaurantId,
                ShortDescript = shortDescript,  
                Description = description,
                Price = price,
                ItemType = itemType,
                StockCount = stockCount,        
                ForAdult = forAdult,             
                SpecialOffer = specialOffer,
                ImageData = data,
                Thumbnails = ImageFunc.GenerateThumbnail(175,175, data)


            };

            using (ApplicationContext db = new ApplicationContext())
            { 
                db.ItemsMenu.Add(newItem);
                db.SaveChanges();
            }
              

            Console.WriteLine("New menu item added successfully.");
        }

       

        

        
        public static ObservableCollection<ItemMenuShort> SearchAndFilterItems(string searchTerm, string type)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var query = db.ItemsMenu.AsQueryable();
                Debug.WriteLine("11");

                //поиск
                if (!string.IsNullOrWhiteSpace(searchTerm) && searchTerm != "Search Yummy Here!")
                {
                    if (ApplicationContext.UseMySql)
                    {
                        query = query.Where(item => item.Name.ToLower().Contains(searchTerm.ToLower()));
                       
                    }
                    else
                    {
                        var initialSelection = db.ItemsMenu
                        .Select(item => new { item.MenuItemId, item.Name })
                        .ToList();



                        var matchingIds = initialSelection
                        .Where(item => item.Name.ToLower().Contains(searchTerm.ToLower()))
                        .Select(item => item.MenuItemId)
                        .ToList();



                        query = query.Where(item => matchingIds.Contains(item.MenuItemId));
                    }
                }

                // фильтрация типа
                if (!string.IsNullOrWhiteSpace(type) && type != "All Products")
                {
                    query = query.Where(item => item.ItemType == type);
                }

                var result = query.ToShorCollect();

                return new ObservableCollection<ItemMenuShort>(result);

                
            }

        }
        public static byte[] LoadImage(int id) 
        {
            using (ApplicationContext db = new ApplicationContext())
            {
               
                var imageData = db.ItemsMenu
                    .Where(item => item.MenuItemId == id)
                    .Select(item => item.ImageData)
                    .FirstOrDefault();
                if (imageData != null)
                {
                    return imageData;
                }
                else 
                {
                    return null;
                }
               
            }
            
        }
    }

}
