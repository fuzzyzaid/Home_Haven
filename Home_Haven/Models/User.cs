using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Home_Haven.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please enter your name.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        [StringLength(250, ErrorMessage = "Address cannot be longer than 250 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter a valid phone number.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits long.")]
        public string PhoneNumber { get; set; }

        // Navigation property for associated orders
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
