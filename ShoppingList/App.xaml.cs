namespace ShoppingList;

public partial class App : Application
{
    public App()
    {
        InitializeComponent(); // Ta linia ładuje plik App.xaml
        MainPage = new MainPage(); // Ustaw główną stronę aplikacji
    }
}