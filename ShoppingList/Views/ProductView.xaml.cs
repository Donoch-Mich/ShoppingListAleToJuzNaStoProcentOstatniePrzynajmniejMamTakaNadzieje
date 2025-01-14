using Microsoft.Maui.Controls;
using ShoppingList.Models;

namespace ShoppingList;

public partial class ProductView : ContentView
{
    public static readonly BindableProperty ProductProperty =
        BindableProperty.Create(
            nameof(Product),
            typeof(Product),
            typeof(ProductView),
            null,
            propertyChanged: OnProductChanged);

    public Product Product
    {
        get => (Product)GetValue(ProductProperty);
        set => SetValue(ProductProperty, value);
    }

    public ProductView()
    {
        InitializeComponent();
    }

    private static void OnProductChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (ProductView)bindable;
        control.BindingContext = newValue;
    }

    private void OnProductBoughtChanged(object sender, CheckedChangedEventArgs e)
    {
        if (Product != null)
        {
            Product.IsBought = e.Value;

            if (Parent?.BindingContext is Category category)
            {
                category.SortProducts();
            }
        }
    }

    private void OnQuantityChanged(object sender, TextChangedEventArgs e)
    {
        if (Product != null && sender is Entry entry)
        {
            if (int.TryParse(entry.Text, out int newQuantity))
            {
                Product.Quantity = newQuantity;
            }
            else
            {
                entry.Text = Product.Quantity.ToString();
            }
        }
    }

    private void OnDeleteProductClicked(object sender, EventArgs e)
    {
        if (Product != null && BindingContext is Product product)
        {
            if (Parent?.BindingContext is Category category)
            {
                category.RemoveProduct(product);
            }
        }
    }
}