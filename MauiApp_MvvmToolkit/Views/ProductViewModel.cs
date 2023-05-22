using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp_MvvmToolkit.Entities.Cart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp_MvvmToolkit.Views
{
    public partial class ProductItemViewModel : ObservableObject
    {

        private readonly CartStore _cartStore;
        public string Name { get;}
        public decimal Price { get; }
        //public ICommand AddToCartCommand { get; }
        public ProductItemViewModel(string name, decimal price, CartStore cartStore)
        {
            Name = name;
            Price = price;
            _cartStore = cartStore;
            // AddToCartCommand = new RelayCommand(()=> AddToCart());
        }

        [RelayCommand]
        private void AddToCart()
        {
            _cartStore.AddToCart(new CartItem(Name,Price));
        }
    }   

    public class ProductViewModel : ObservableObject
    {
        private readonly ObservableCollection<ProductItemViewModel> _products;

        public ObservableCollection<ProductItemViewModel> Products => _products;

        public ProductViewModel(CartStore cartStore)
        {
            _products = new ObservableCollection<ProductItemViewModel>()
           {
                new ProductItemViewModel("Product 1", 10.00m,cartStore),
                new ProductItemViewModel("Product 2", 20.00m,cartStore),
                new ProductItemViewModel("Product 3", 30.00m,cartStore),
                new ProductItemViewModel("Product 4", 40.00m,cartStore),
                new ProductItemViewModel("Product 5", 50.00m,cartStore),
                new ProductItemViewModel("Product 6", 60.00m,cartStore),
                new ProductItemViewModel("Product 7", 70.00m,cartStore),
                new ProductItemViewModel("Product 8", 80.00m,cartStore),
                new ProductItemViewModel("Product 9", 90.00m,cartStore),
                new ProductItemViewModel("Product 10", 100.00m, cartStore)
           };


        }
    }
}
