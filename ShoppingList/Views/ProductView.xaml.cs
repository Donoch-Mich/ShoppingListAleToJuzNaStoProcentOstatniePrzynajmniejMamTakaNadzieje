using Microsoft.Maui.Controls;
using ShoppingList.Models;

namespace ShoppingList;

public partial class ProductView : ContentView
{
    // Definiujemy BindableProperty dla Product
    public static readonly BindableProperty ProductProperty =
        BindableProperty.Create(
            nameof(Product),                // Nazwa właściwości
            typeof(Product),                // Typ właściwości
            typeof(ProductView),            // Typ właściciela
            null,                           // Domyślna wartość
            propertyChanged: OnProductChanged); // Metoda wywoływana przy zmianie wartości

    // Właściwość Product
    public Product Product
    {
        get => (Product)GetValue(ProductProperty);
        set => SetValue(ProductProperty, value);
    }

    public ProductView()
    {
        InitializeComponent();
    }

    // Metoda wywoływana przy zmianie wartości Product
    private static void OnProductChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (ProductView)bindable;
        control.BindingContext = newValue; // Ustawiamy BindingContext na nową wartość Product
    }

    // Obsługa zmiany stanu CheckBox (czy produkt został kupiony)
    private void OnProductBoughtChanged(object sender, CheckedChangedEventArgs e)
    {
        if (Product != null)
        {
            Product.IsBought = e.Value; // Zaktualizuj stan produktu

            // Znajdź kategorię, do której należy produkt, i posortuj produkty
            if (Parent?.BindingContext is Category category)
            {
                category.SortProducts(); // Sortuj produkty po zmianie stanu
            }
        }
    }

    // Obsługa zmiany ilości produktu (na bieżąco)
    private void OnQuantityChanged(object sender, TextChangedEventArgs e)
    {
        if (Product != null && sender is Entry entry)
        {
            if (int.TryParse(entry.Text, out int newQuantity))
            {
                Product.Quantity = newQuantity; // Zaktualizuj ilość produktu
            }
            else
            {
                // Jeśli wprowadzono nieprawidłową wartość, przywróć poprzednią ilość
                entry.Text = Product.Quantity.ToString();
            }
        }
    }

    // Obsługa usuwania produktu
    private void OnDeleteProductClicked(object sender, EventArgs e)
    {
        if (Product != null && BindingContext is Product product)
        {
            // Znajdź kategorię, do której należy produkt
            if (Parent?.BindingContext is Category category)
            {
                category.RemoveProduct(product); // Usuń produkt z kategorii
            }
        }
    }
}