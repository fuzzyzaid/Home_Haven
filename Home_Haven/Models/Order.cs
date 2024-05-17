using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Home_Haven.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Order Date is required.")]
        public DateTime OrderDate { get; set; }

        // Navigation property for associated user
        public User User { get; set; }

        // Navigation property for associated order details
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
