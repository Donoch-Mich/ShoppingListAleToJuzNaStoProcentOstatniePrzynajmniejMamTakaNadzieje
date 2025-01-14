using System.Collections.ObjectModel;
using System.Linq;

namespace ShoppingList.Models;

public class Category
{
    public string Name { get; set; }
    public ObservableCollection<Product> Products { get; set; }

    public Category()
    {
        Products = new ObservableCollection<Product>();
    }

    public Category(string name) : this()
    {
        Name = name;
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
        SortProducts();
    }

    public void RemoveProduct(Product product)
    {
        Products.Remove(product);
    }

    public void SortProducts()
    {
        var sortedProducts = Products
            .OrderBy(p => p.IsBought)
            .ToList();

        Products.Clear();
        foreach (var product in sortedProducts)
        {
            Products.Add(product);
        }
    }
}