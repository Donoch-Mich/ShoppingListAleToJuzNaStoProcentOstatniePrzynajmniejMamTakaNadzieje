using ShoppingList.Models;

namespace ShoppingList.Views;

public partial class ProductView : ContentView
{
    public ProductView()
    {
        InitializeComponent();
    }

    public ProductView(Product product) : this()
    {
        BindingContext = product; // Ustawienie BindingContext
    }
}