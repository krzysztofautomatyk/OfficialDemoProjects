using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp_MvvmToolkit.Entities.Cart
{
    public class CartStore
    {
        private readonly List<CartItem> _cartItems;

        public CartStore() { 
            _cartItems = new List<CartItem>();

            StrongReferenceMessenger.Default.Register<CartStore,CartItemsRequestMessage>(this, OnCartItemsRequestMessage);
        }

        private void OnCartItemsRequestMessage(CartStore cartStore, CartItemsRequestMessage message)
        {
            message.Reply(cartStore._cartItems);
        }

        internal void AddToCart(CartItem cartItem)
        {
            _cartItems.Add(cartItem);

            StrongReferenceMessenger.Default.Send(new CartItemAddedMesage(cartItem));
        }
    }
}
