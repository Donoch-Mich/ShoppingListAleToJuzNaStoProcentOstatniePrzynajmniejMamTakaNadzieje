using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShoppingList.Models;

public class Product : INotifyPropertyChanged
{
    private string _name;
    private string _unit;
    private int _quantity;
    private bool _isBought;
    private string _category;

    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }

    public string Unit
    {
        get => _unit;
        set
        {
            if (_unit != value)
            {
                _unit = value;
                OnPropertyChanged();
            }
        }
    }

    public int Quantity
    {
        get => _quantity;
        set
        {
            if (_quantity != value)
            {
                _quantity = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayText));
            }
        }
    }

    public bool IsBought
    {
        get => _isBought;
        set
        {
            if (_isBought != value)
            {
                _isBought = value;
                OnPropertyChanged();
            }
        }
    }

    public string Category
    {
        get => _category;
        set
        {
            if (_category != value)
            {
                _category = value;
                OnPropertyChanged();
            }
        }
    }

    public string DisplayText => $"{Name} ({Quantity} {Unit})";

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}