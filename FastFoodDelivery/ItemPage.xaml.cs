
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace FastFoodDelivery
{
    public partial class ItemPage : Page
    {
        private UserAuth User;
        public ItemPage(ItemMenuShort Preitem,  UserAuth user) 
        {
            InitializeComponent();
            User = user;
            ItemImage.Source = Preitem.ImageSourceThumbNails;
            InitializationItemMenu(Preitem);

            this.Loaded += (s, e) => MainGrid.Focus();
            Load_Image(Preitem.MenuItemId);
        }
        public ItemPage() { }
        //GetDataItem
        private void InitializationItemMenu(ItemMenuShort Preitem) 
        {
            Name_box.Text = Preitem.Name;
            Price_box.Text = Preitem.Price.ToString() +"$";
            ItemType_box.Text = Preitem.ItemType.ToString();
            StockCount_box.Text = "In Stock: " + Preitem.StockCount.ToString();
            if (Preitem.ForAdult)
            {
                ForAdult_box.Text = "Only For Adults 18+";
            }
            else 
            {
                ForAdult_box.Text = "";
            }
            ItemImage.Source = Preitem.ImageSourceThumbNails;
        }
        public async void Load_Image(int id)
        {
            try
            {
                Name_box.Margin = new Thickness(67, 0, 0, 0);
                LoadingDonat.Visibility = System.Windows.Visibility.Visible;
                byte[] Image = await Task.Run(() =>
                {
                        return MenuService.LoadImage(id);     
                });
                LoadingDonat.Visibility = System.Windows.Visibility.Collapsed;
                Name_box.Margin = new Thickness(67, 120,0, 0);
                if (Image != null)
                {
                    ItemImage.Source = ImageFunc.ConvertByteArrayToImage(Image);
                    Debug.WriteLine("Image full is loaded");
                }
               
            }
            catch (TaskCanceledException)
            {

                Debug.WriteLine("Ошибка загрузки");
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"Произошла ошибка при загрузке: {ex.Message}");
            }
           
        }

        //KEYS
        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            PageFunc.KeysBack(sender, e, this.NavigationService);
        }


        //CLICK
        private void Back_btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
         
            PageFunc.GoBack(this.NavigationService);
          
            
        }

        private void User_btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           
            PageFunc.OpenPage(new UserPage(User),User,this.NavigationService);
          
            
        }

        //LOADERS
        private void Back_btn_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement AnimObj)
            {
                AnimObj.MouseEnter += (sender, e) => Animations.StartAnimation(Animations.CreateScaleAnimationStandartIn(), sender, false);
                AnimObj.MouseLeave += (sender, e) => Animations.StartAnimation(Animations.CreateScaleAnimationStandartOut(), sender, false);
            }
        }

        private void User_btn_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement AnimObj)
            {
                AnimObj.MouseEnter += (sender, e) => Animations.StartAnimation(Animations.CreateScaleAnimationStandartIn(), sender, false);
                AnimObj.MouseLeave += (sender, e) => Animations.StartAnimation(Animations.CreateScaleAnimationStandartOut(), sender, false);
            }
        }
    }
}
