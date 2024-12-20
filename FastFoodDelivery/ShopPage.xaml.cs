﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace FastFoodDelivery
{
    public partial class ShopPage: Page
    {
        
        public ObservableCollection<ItemMenuShort> ItemsMenu;
        private static readonly List<string> ItemTypes = new List<string> { "All Products", "Main Course", "Side Dish", "Dessert", "Drink", "Snack", "Combo", "Sous" };



        Rectangle ?Line_Search;
        Rectangle ?Background_Search_box;

        public bool Finding = false;

        private UserAuth User;
        private CancellationTokenSource ?loading_token;
        private string Search_box_previous_text;
       


        public ShopPage(UserAuth user)

        {
            InitializeComponent();
            User = user;
            ItemsMenu = new ObservableCollection<ItemMenuShort>();
            ProductsItemsControl.ItemsSource = ItemsMenu;

            ComboBox_ItemType.ItemsSource = ItemTypes;

            Search_box_previous_text = Search_box.Text.Trim();

            {
                //MenuService.AddNewMenuItem("Чизбургер", 681, "Сочный бургер с сыром", "Чи́збургер (англ. cheeseburger, от cheese — сыр) — это гамбургер с сыром. Традиционно ломтик сыра кладется поверх мясной котлеты. Сыр обычно добавляют в готовящийся гамбургер незадолго до подачи на стол, что позволяет сыру расплавиться..", 12.0m, "Main Course", 1020, false, true, "C:\\Users\\artem\\Downloads\\burger.png");
                //MenuService.AddNewMenuItem("Гамбургер", 682, "Классический бургер с говядиной", "Гамбургер — это сэндвич, состоящий из котлеты из говядины, помещенной между двумя половинками булочки. Часто подается с различными добавками, такими как салат, помидоры и соусы.", 10.0m, "Main Course", 800, false, true, "C:\\Users\\artem\\Downloads\\images.png");
                //MenuService.AddNewMenuItem("Куриный бургер", 683, "Бургер с куриным филе", "Куриный бургер — это сэндвич, в котором используется котлета из куриного филе. Часто подается с овощами и соусами, такими как майонез или горчица.", 11.0m, "Main Course", 750, false, true, "C:\\Users\\artem\\Downloads\\chiken.png");
                //MenuService.AddNewMenuItem("Coca-Cola", 682, "Освежающий газированный напиток", "Coca-Cola — это классический газированный напиток с уникальным вкусом. Идеально подходит, чтобы утолить жажду в любое время!", 5.0m, "Drink", 140, false, true, "C:\\Users\\artem\\Downloads\\coca_cola.png");
                //MenuService.AddNewMenuItem("Sprite", 683, "Лимонно-лаймовый газированный напиток", "Sprite — это освежающий напиток с лимонным и лаймовым вкусом. Отличный выбор для летнего дня!", 5.0m, "Drink", 140, false, true, "C:\\Users\\artem\\Downloads\\sprite.png");
                //MenuService.AddNewMenuItem("Fanta", 684, "Фруктовый газированный напиток", "Fanta — это яркий и фруктовый газированный напиток, который заставит вас улыбнуться с каждого глотка!", 5.0m, "Drink", 150, false, true, "C:\\Users\\artem\\Downloads\\fanta.png");
                //MenuService.AddNewMenuItem("Лимонный сок", 685, "Освежающий натуральный лимонный сок", "Настоящий лимонный сок, полный витаминов и идеален для поднятия настроения в жаркий день!", 6.0m, "Drink", 30, false, true, "C:\\Users\\artem\\Downloads\\lemon_juice.png");

            }

            Load_menu_items_local();

        }
        
        //LOADING_ITEMS
        public async void Load_menu_items_local() 
        {
            Finding = false;
            donutSpinner.Visibility = Visibility.Visible;
            donutSpinner_search.Visibility = Visibility.Collapsed;
            ItemsMenu.Clear();
            loading_token?.Cancel();
            loading_token = new CancellationTokenSource();
            var token = loading_token.Token;
            string search = Search_box.Text.Trim();
            
            string ?type = ComboBox_ItemType.SelectedItem.ToString();
            try
            {
                var products = await Task.Run(() =>
                {
                    if (type is string) 
                    {
                        return MenuService.SearchAndFilterItems(search, type);
                    }
                    return null;
                }, token);

                if (token.IsCancellationRequested)
                {
                    Debug.WriteLine("Загрузка отменена");
                    return;
                }
                ItemsMenu.Clear();

                if (products != null) 
                {
                    foreach (var product in products)
                    {
                        ItemsMenu.Add(product); 
                    }
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
            finally 
            {
                donutSpinner.Visibility = Visibility.Collapsed;
            }
          
        }
       

        //Serch box LOGIC 
        private void Search_box_GotFocus(object sender, RoutedEventArgs e)
        {

            if (Search_box.Text == "Search Yummy Here!")
            {
                Search_box.Text = "";
                
            }
        }
        private void Search_box_LostFocus(object sender, RoutedEventArgs e)
        {

            if (Search_box.Text.Length == 0)
            {
                Search_box.Text = "Search Yummy Here!";
                firstsearch = true;

            }
        }
        private bool firstsearch = true;
        private void Search_box_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            Debug.WriteLine(firstsearch);
            Debug.WriteLine(Search_box_previous_text);
            if (Search_box != null && Line_Search != null)
            {
                if (Search_box.Text!="" && Search_box.Text!= "Search Yummy Here!" || !firstsearch && Search_box.Text == "") 
                {
                    Load_menu_items_local();
                    firstsearch = false;
                }

                TextBoxResizer.ResizeTextBox(Search_box, Line_Search);               
                Search_box_previous_text = Search_box.Text;
            }
            else
            {
                Debug.WriteLine("null Search_box or Line_Search");
            }

        }

        //CLICK
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ApplicationContext.UseMySql = !ApplicationContext.UseMySql;
        }
        private void Item_btn_Click(object sender, RoutedEventArgs e)
        {


            if (sender != null && sender is Button button)
            {

                var clickedItem = button.CommandParameter as ItemMenuShort;

                if (clickedItem != null)
                {
                    var nav = this.NavigationService;
                    PageFunc.OpenPage(new ItemPage(clickedItem, User),User, nav);
                
                }
            }

        }

        private async void User_btn_Click(object sender, RoutedEventArgs e)
        {

            var nav = this.NavigationService;
            PageFunc.OpenPage(new UserPage(User), User, nav);

        }


        //CHANGERS

        private void ComboBox_ItemType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_ItemType.SelectedItem is string type)
            { 
                Load_menu_items_local();
            }
           
        

        }
        private void ShopPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {



        }


        //LOADERS
        private void Search_box_Loaded(object sender, RoutedEventArgs e)
        {
            var template = Search_box.Template as ControlTemplate;

            Background_Search_box = template?.FindName("Background_Search_box", Search_box) as Rectangle;
            Line_Search = template?.FindName("Line_Search", Search_box) as Rectangle;

            if (sender is FrameworkElement AnimObj)
            {
                AnimObj.GotFocus += (sender, e) => Animations.StartAnimation(Animations.CreateScaleAnimationStandartIn(), sender,false);
                AnimObj.LostFocus += (sender, e) => Animations.StartAnimation(Animations.CreateScaleAnimationStandartOut(), sender, false);
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
