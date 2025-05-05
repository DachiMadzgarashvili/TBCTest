using System.ComponentModel.DataAnnotations;

namespace TBCTest.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        [RegularExpression("^[ა-ჰ]+$", ErrorMessage = "FirstNameGe must contain only Georgian letters.")]
        public string FirstNameGe { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "FirstNameEn must contain only Latin letters.")]
        public string FirstNameEn { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        [RegularExpression("^[ა-ჰ]+$", ErrorMessage = "LastNameGe must contain only Georgian letters.")]
        public string LastNameGe { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "LastNameEn must contain only Latin letters.")]
        public string LastNameEn { get; set; }

        [Required]
        [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be 'Male' or 'Female'.")]
        public string Gender { get; set; }

        [Required]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "PersonalNumber must be exactly 11 digits.")]
        public string PersonalNumber { get; set; }

        [Required]
        [CustomValidation(typeof(Person), nameof(ValidateBirthDate))]
        public DateTime BirthDate { get; set; }

        [Required]
        public int CityId { get; set; }
        public City City { get; set; }

        public string? ImagePath { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; } = new();
        public List<PersonRelation> RelatedPeople { get; set; } = new();
        public List<PersonRelation> RelatedToPeople { get; set; } = new();

        public static ValidationResult? ValidateBirthDate(DateTime birthDate, ValidationContext context)
        {
            return birthDate <= DateTime.Today.AddYears(-18)
                ? ValidationResult.Success
                : new ValidationResult("Person must be at least 18 years old.");
        }
    }
}
