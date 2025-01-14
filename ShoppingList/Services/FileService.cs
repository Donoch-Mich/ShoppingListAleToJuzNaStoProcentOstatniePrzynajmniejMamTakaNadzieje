using System.Xml.Linq;
using ShoppingList.Models;

namespace ShoppingList.Services;

public class FileService
{
    private readonly string _filePath;

    public FileService()
    {
        _filePath = Path.Combine(FileSystem.AppDataDirectory, "shoppinglist.xml");
    }

    public List<Category> LoadData()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Category>();
        }

        var xml = File.ReadAllText(_filePath);
        var xDoc = XDocument.Parse(xml);

        var categories = new List<Category>();
        foreach (var categoryElement in xDoc.Root.Elements("Category"))
        {
            var category = new Category
            {
                Name = categoryElement.Attribute("Name")?.Value
            };

            foreach (var productElement in categoryElement.Elements("Product"))
            {
                var product = new Product
                {
                    Name = productElement.Attribute("Name")?.Value,
                    Unit = productElement.Attribute("Unit")?.Value,
                    Quantity = int.Parse(productElement.Attribute("Quantity")?.Value ?? "0"),
                    IsBought = bool.Parse(productElement.Attribute("IsBought")?.Value ?? "false"),
                    Category = category.Name
                };
                category.Products.Add(product);
            }

            categories.Add(category);
        }

        return categories;
    }

    public void SaveData(List<Category> categories)
    {
        var xDoc = new XDocument(new XElement("ShoppingList"));

        foreach (var category in categories)
        {
            var categoryElement = new XElement("Category", new XAttribute("Name", category.Name));

            foreach (var product in category.Products)
            {
                var productElement = new XElement("Product",
                    new XAttribute("Name", product.Name),
                    new XAttribute("Unit", product.Unit),
                    new XAttribute("Quantity", product.Quantity),
                    new XAttribute("IsBought", product.IsBought),
                    new XAttribute("Category", product.Category)
                );
                categoryElement.Add(productElement);
            }

            xDoc.Root.Add(categoryElement);
        }

        xDoc.Save(_filePath);
    }
}