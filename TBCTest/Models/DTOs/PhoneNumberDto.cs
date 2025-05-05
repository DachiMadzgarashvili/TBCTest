using System.ComponentModel.DataAnnotations;

namespace TBCTest.Models.DTOs
{
    public class PhoneNumberDto
    {
        [Required]
        [RegularExpression("^(Mobile|Office|Home)$", ErrorMessage = "Type must be 'Mobile', 'Office', or 'Home'.")]
        public string Type { get; set; }

        [Required, StringLength(50, MinimumLength = 4)]
        public string Number { get; set; }
    }
}
