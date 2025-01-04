using ShoppingList.Models;
using ShoppingList.Services;
using System.Collections.ObjectModel;

namespace ShoppingList;

public partial class MainPage : ContentPage
{
    private readonly FileService _fileService = new();
    public ObservableCollection<Category> Categories { get; set; }

    public MainPage()
    {
        InitializeComponent();

        // Wczytaj dane z pliku XML
        Categories = new ObservableCollection<Category>(_fileService.LoadData());
        BindingContext = this; // Ustawienie BindingContext
    }

    private async void OnAddProductClicked(object sender, EventArgs e)
    {
        string productName = await DisplayPromptAsync("Dodaj produkt", "Podaj nazwę produktu:");
        string unit = await DisplayPromptAsync("Dodaj produkt", "Podaj jednostkę (np. szt., kg, l):");
        string quantity = await DisplayPromptAsync("Dodaj produkt", "Podaj ilość:", keyboard: Keyboard.Numeric);

        if (!string.IsNullOrWhiteSpace(productName) && !string.IsNullOrWhiteSpace(unit) && int.TryParse(quantity, out int qty))
        {
            var product = new Product
            {
                Name = productName,
                Unit = unit,
                Quantity = qty,
                IsBought = false
            };

            string categoryName = await DisplayActionSheet("Wybierz kategorię", "Anuluj", null, Categories.Select(c => c.Name).ToArray());
            var selectedCategory = Categories.FirstOrDefault(c => c.Name == categoryName);

            if (selectedCategory != null)
            {
                product.Category = selectedCategory.Name; // Przypisz kategorię do produktu
                selectedCategory.AddProduct(product);
                _fileService.SaveData(Categories.ToList()); // Zapisz dane do pliku XML
            }
        }
        else
        {
            await DisplayAlert("Błąd", "Wprowadź poprawne dane produktu.", "OK");
        }
    }

    protected override void OnDisappearing()
    {
        // Zapisz dane do pliku XML przy zamykaniu strony
        _fileService.SaveData(Categories.ToList());
        base.OnDisappearing();
    }
}