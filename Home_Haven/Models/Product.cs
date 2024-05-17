using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Home_Haven.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please enter a product name.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide a description.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a price.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter the quantity.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please provide an image URL.")]
        [StringLength(500, ErrorMessage = "Image URL cannot exceed 500 characters.")]
        public string ImageURL { get; set; }

        // Navigation property for associated order details
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
