using MauiApp_MvvmToolkit.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp_MvvmToolkit.Entities.Cart
{
    public class CartItemAddedMesage    
    {
        public CartItem CartItem { get; }

        public CartItemAddedMesage(CartItem cartItem)
        {
            CartItem = cartItem;
        }
    }
}
