using MauiStoreApp.Services;

namespace MauiStoreApp
{
    public partial class MainPage : ContentPage
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly CartService _cartService;
        private readonly UserService _userService;
        private Color _originalButtonColor;

        public MainPage()
        {
            InitializeComponent();

            _productService = MauiProgram.CreateMauiApp().Services.GetService<ProductService>();
            _categoryService = MauiProgram.CreateMauiApp().Services.GetService<CategoryService>();
            _cartService = MauiProgram.CreateMauiApp().Services.GetService<CartService>();
            _userService = MauiProgram.CreateMauiApp().Services.GetService<UserService>();

            // Store the original button color
            _originalButtonColor = GetProductsBtn.BackgroundColor;
        }

        private void OnGetProductsBtnClicked(object sender, EventArgs e)
        {
            LoadAndLogProducts();
        }

        private void OnGetCategoriesBtnClicked(object sender, EventArgs e)
        {
            LoadAndLogCategories();
        }

        private void OnGetCartBtnClicked(object sender, EventArgs e)
        {
            LoadAndLogCart(5);
        }

        private void OnGetUserBtnClicked(object sender, EventArgs e)
        {
            LoadAndLogUser(5);
        }


        private async void LoadAndLogProducts()
        {
            try
            {
                var products = await _productService.GetProductsAsync();

                if (products != null)
                {
                    Console.WriteLine("Products loaded successfully!");
                    GetProductsBtn.Text = "Products 200 OK";
                    GetProductsBtn.BackgroundColor = Colors.Green;

                    foreach (var product in products)
                    {
                        Console.WriteLine($"ID: {product.Id}, Rating: {product.Rating.Rate}");
                    }

                    // delay and then reset the button
                    await Task.Delay(1500);
                    GetProductsBtn.Text = "Get Products";
                    GetProductsBtn.BackgroundColor = _originalButtonColor;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        
        private async void LoadAndLogCategories()
        {
            try
            {
                var categories = await _categoryService.GetCategoriesAsync();

                if (categories != null)
                {
                    Console.WriteLine("Categories loaded successfully!");
                    GetCategoriesBtn.Text = "Categories 200 OK";
                    GetCategoriesBtn.BackgroundColor = Colors.Green;

                    foreach (var category in categories)
                    {
                        Console.WriteLine($"Category: {category.Name}");
                    }

                    // delay and then reset the button
                    await Task.Delay(1500);
                    GetCategoriesBtn.Text = "Get Categories";
                    GetCategoriesBtn.BackgroundColor = _originalButtonColor;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async void LoadAndLogCart(int cartId)
        {
            try
            {
                var cart = await _cartService.GetCartAsync(cartId);

                if (cart != null)
                {
                    Console.WriteLine("Cart loaded successfully!");
                    GetCartBtn.Text = "Cart 200 OK";
                    GetCartBtn.BackgroundColor = Colors.Green;

                    Console.WriteLine($"Cart ID: {cart.Id}, User ID: {cart.UserId}, Date: {cart.Date}");
                    foreach (var product in cart.Products)
                    {
                        Console.WriteLine($"Product ID: {product.ProductId}, Quantity: {product.Quantity}");
                    }

                    // delay and then reset the button
                    await Task.Delay(1500);
                    GetCartBtn.Text = "Get Cart";
                    GetCartBtn.BackgroundColor = _originalButtonColor;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async void LoadAndLogUser(int userId)
        {
            try
            {
                var user = await _userService.GetUserAsync(userId);

                if (user != null)
                {
                    Console.WriteLine("User loaded successfully!");
                    GetUserBtn.Text = "User 200 OK";
                    GetUserBtn.BackgroundColor = Colors.Green;

                    Console.WriteLine($"User ID: {user.Id}, Name: {user.Name}, Email: {user.Email}");

                    // delay and then reset the button
                    await Task.Delay(1500);
                    GetUserBtn.Text = "Get User";
                    GetUserBtn.BackgroundColor = _originalButtonColor;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }   
        }

    }
}
