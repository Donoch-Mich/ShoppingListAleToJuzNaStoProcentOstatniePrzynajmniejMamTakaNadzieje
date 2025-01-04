// ShoppingCardView.xaml.cs
using Microsoft.Maui.Controls;
using ShoppingList.Models;

namespace ShoppingList.Controls;

public partial class ShoppingCardView : ContentView
{
    public ShoppingCardView()
    {
        InitializeComponent();
    }

    // Bindable properties
    public static readonly BindableProperty BorderColorProperty =
        BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(ShoppingCardView), Colors.Gray);

    public static readonly BindableProperty CategoryNameProperty =
        BindableProperty.Create(nameof(CategoryName), typeof(string), typeof(ShoppingCardView), string.Empty);

    public static readonly BindableProperty ProductsProperty =
        BindableProperty.Create(nameof(Products), typeof(List<Product>), typeof(ShoppingCardView), new List<Product>());

    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    public string CategoryName
    {
        get => (string)GetValue(CategoryNameProperty);
        set => SetValue(CategoryNameProperty, value);
    }

    public List<Product> Products
    {
        get => (List<Product>)GetValue(ProductsProperty);
        set => SetValue(ProductsProperty, value);
    }
}