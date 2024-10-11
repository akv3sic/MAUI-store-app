using System.ComponentModel;

namespace MauiStoreApp.Models
{
    /// <summary>
    /// Represents the details of an item in the shopping cart, including the product and quantity.
    /// </summary>
    public class CartItemDetail : INotifyPropertyChanged
    {
        private Product _product;
        private int _quantity;

        /// <summary>
        /// Gets or sets the product in the cart item.
        /// </summary>
        public Product Product
        {
            get => _product;
            set
            {
                if (_product != value)
                {
                    _product = value;
                    OnPropertyChanged(nameof(Product));
                }
            }
        }

        /// <summary>
        /// Gets or sets the quantity of the product in the cart item.
        /// </summary>
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
