namespace ShoppingList.Models;

public class Product
{
    public string Name { get; set; } // Nazwa produktu
    public string Unit { get; set; } // Jednostka (szt., kg, l)
    public int Quantity { get; set; } // Ilość
    public bool IsBought { get; set; } // Czy kupiony
    public string Category { get; set; } // Kategoria produktu

    // Właściwość tylko do odczytu
    public string DisplayText => $"{Name} ({Quantity} {Unit})";
}