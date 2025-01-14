using System.Collections.ObjectModel;
using System.Linq;

namespace ShoppingList.Models;

public class Category
{
    public string Name { get; set; } // Nazwa kategorii
    public ObservableCollection<Product> Products { get; set; } // Lista produktów

    public Category()
    {
        Products = new ObservableCollection<Product>(); // Inicjalizacja listy produktów
    }

    public Category(string name) : this()
    {
        Name = name; // Inicjalizacja nazwy kategorii
    }

    // Metoda do dodawania produktu do kategorii
    public void AddProduct(Product product)
    {
        Products.Add(product);
        SortProducts(); // Sortuj produkty po dodaniu
    }

    // Metoda do usuwania produktu z kategorii
    public void RemoveProduct(Product product)
    {
        Products.Remove(product);
    }

    // Metoda do sortowania produktów (kupione na dole)
    public void SortProducts()
    {
        var sortedProducts = Products
            .OrderBy(p => p.IsBought) // Sortuj według IsBought (false -> true)
            .ToList();

        Products.Clear();
        foreach (var product in sortedProducts)
        {
            Products.Add(product);
        }
    }
}