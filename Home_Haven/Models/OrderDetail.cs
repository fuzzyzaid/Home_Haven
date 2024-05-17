using System.ComponentModel.DataAnnotations;

namespace Home_Haven.Models
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public Order Order { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Total Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total Price must be a positive value.")]
        public decimal TotalPrice { get; set; }
    }
}
