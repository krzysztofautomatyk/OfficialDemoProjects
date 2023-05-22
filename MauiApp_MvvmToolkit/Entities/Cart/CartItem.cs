using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp_MvvmToolkit.Entities.Cart
{
    public class CartItem
    {

        public string Name { get; }
        public decimal Price { get; }
        public CartItem(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
