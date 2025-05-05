using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace TBCTest.Models.DTOs
{
    public class CreatePersonDto
    {
        [Required, StringLength(50, MinimumLength = 2)]
        [RegularExpression("^[ა-ჰ]+$", ErrorMessage = "FirstNameGe must contain only Georgian letters.")]
        [SwaggerSchema("Georgian first name (2–50 chars)")]
        public string FirstNameGe { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "FirstNameEn must contain only Latin letters.")]
        [SwaggerSchema("English first name (2–50 chars)")]
        public string FirstNameEn { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        [RegularExpression("^[ა-ჰ]+$", ErrorMessage = "LastNameGe must contain only Georgian letters.")]
        [SwaggerSchema("Georgian last name (2–50 chars)")]
        public string LastNameGe { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "LastNameEn must contain only Latin letters.")]
        [SwaggerSchema("English last name (2–50 chars)")]
        public string LastNameEn { get; set; }

        [Required]
        [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be 'Male' or 'Female'.")]
        [SwaggerSchema("Gender: Male or Female")]
        public string Gender { get; set; }

        [Required]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "PersonalNumber must be exactly 11 digits.")]
        [SwaggerSchema("11-digit personal number")]
        public string PersonalNumber { get; set; }

        [Required]
        [SwaggerSchema("Birth date (must be at least 18 years ago)", Format = "date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [SwaggerSchema("City identifier")]
        public int CityId { get; set; }

        [Required, MinLength(1)]
        [SwaggerSchema("Phone numbers for this person")]
        public List<PhoneNumberDto> PhoneNumbers { get; set; }
    }
}
