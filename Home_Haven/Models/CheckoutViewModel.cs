using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Home_Haven.Models
{
    public class CheckoutViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Please enter your name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your address.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your phone number.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits long.")]
        public string PhoneNumber { get; set; }

        
        [Required(ErrorMessage = "Please enter your card number.")]
        [CreditCard(ErrorMessage = "Please enter a valid card number.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Please enter your card's expiry date.")]
        [RegularExpression(@"\d{2}/\d{2}", ErrorMessage = "Please enter a valid expiry date (MM/YY).")]
        public string ExpiryDate { get; set; }

        [Required(ErrorMessage = "Please enter your CVV.")]
        [RegularExpression(@"\d{3,4}", ErrorMessage = "CVV must be 3 or 4 digits.")]
        public string CVV { get; set; }

        // Implement the IValidatableObject interface
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Check the custom validation for ExpiryDate
            if (!IsValidExpiryDate())
            {
                yield return new ValidationResult("Expiry date is invalid. Month should be between 1 and 12, and date should not be in the past.", new[] { nameof(ExpiryDate) });
            }
        }

        // Custom validation method for ExpiryDate
        public bool IsValidExpiryDate()
        {
            // Split the expiry date into month and year
            var parts = ExpiryDate.Split('/');
            if (parts.Length != 2)
            {
                return false;
            }

            // Parse the month and year
            if (int.TryParse(parts[0], out int month) && int.TryParse(parts[1], out int year))
            {
                // Get the current date
                DateTime currentDate = DateTime.Now;

                // Ensure the month is between 1 and 12
                if (month < 1 || month > 12)
                {
                    return false;
                }

                // Add 2000 to the year to get a four-digit year
                year += 2000;

                // Create a date for the provided expiry date
                DateTime expiryDate = new DateTime(year, month, 1);

                // Check that the expiry date is not before the current date
                return expiryDate >= new DateTime(currentDate.Year, currentDate.Month, 1);
            }

            return false;
        }
    }
}
