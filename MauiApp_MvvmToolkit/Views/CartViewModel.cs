using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MauiApp_MvvmToolkit.Entities.Cart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp_MvvmToolkit.Views
{
    public class CartItemViewModel : ObservableObject
    {
        public string Name { get; }
        public decimal Price { get; }
        public CartItemViewModel(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }

    // If you want use the CartItemAddedMesage, you need to implement IRecipient<CartItemAddedMesage>
    // If you want use the CartItemsRequestMessage, you need to implement IRecipient<CartItemsRequestMessage>
    // If you want use "OnActivated" method, you need to use ObservableRecipient
    public partial class CartViewModel : ObservableValidator,  IRecipient<CartItemAddedMesage>, IRecipient<CartItemsRequestMessage>
    {
        private readonly ObservableCollection<CartItemViewModel> _carItems;

        public ObservableCollection<CartItemViewModel> CartItems => _carItems;


        /// <summary>
        /// This property is bound to the "HasAgreedToTermsAndConditions" checkbox in the view.
        /// </summary>
        [ObservableProperty]
        [IsTrue]
        private bool _hasAgreedToTermsAndConditions;


        /// <summary>
        /// This constructor is called when the view model is created.
        /// </summary>
        public CartViewModel()
        {
            _carItems = new ObservableCollection<CartItemViewModel>();

            StrongReferenceMessenger.Default.Register<CartItemAddedMesage>(this);

            // When initializing the view model, we need to send a message to the store to get the current cart items.
            StrongReferenceMessenger.Default.Register<CartItemsRequestMessage>(this);
            StrongReferenceMessenger.Default.Send<CartItemsRequestMessage>();

            //OnActivated();            
        }

        [RelayCommand]
        private async Task Checkout()
        {
            ValidateAllProperties();

            if(HasErrors)
            {
                return;
            }

            Application.Current.MainPage.DisplayAlert("Success", "Your order has been placed", "OK");
       
        }


        ///// <summary>
        ///// This method is called when the view model is activated.
        ///// </summary>
        //protected override void OnActivated()
        //{

        //    StrongReferenceMessenger.Default.Register<CartItemAddedMesage>(this);

        //    // When initializing the view model, we need to send a message to the store to get the current cart items.
        //    StrongReferenceMessenger.Default.Register<CartItemsRequestMessage>(this);
        //    StrongReferenceMessenger.Default.Send<CartItemsRequestMessage>();

        //    base.OnActivated();
            
        //}

        ///// <summary>
        ///// This method is called when the view model is deactivated.
        ///// </summary>
        //protected override void OnDeactivated()
        //{
        //    StrongReferenceMessenger.Default.Unregister<CartItemAddedMesage>(this);
        //    StrongReferenceMessenger.Default.Unregister<CartItemsRequestMessage>(this);          

        //    base.OnDeactivated();
            
        //}

        public void Receive(CartItemAddedMesage message)
        {
            _carItems.Add(new CartItemViewModel(message.CartItem.Name, message.CartItem.Price));
        }

        /// <summary>
        /// This method is called when we receive a message of type CartItemsRequestMessage.
        /// </summary>
        /// <param name="message"></param>
        public void Receive(CartItemsRequestMessage message)
        {
            // When we receive the message, we need to check if the message has already been replied to.
            if(!message.HasReceivedResponse)
            {
                return;
            }
            _carItems.Clear();

            foreach (var cartItem in message.Response)
            {
                _carItems.Add(new CartItemViewModel(cartItem.Name, cartItem.Price));
            }
        }
    }

    public sealed class IsTrueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return bool.TryParse(value.ToString(),out bool b) && b ? ValidationResult.Success:
                new ValidationResult("You must agree to the terms and conditions.");
        }
    }
}
