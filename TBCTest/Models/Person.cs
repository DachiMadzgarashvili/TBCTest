using System.ComponentModel.DataAnnotations;

namespace TBCTest.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        [RegularExpression("^[ა-ჰ]+$|^[a-zA-Z]+$", ErrorMessage = "Only Georgian or Latin letters allowed.")]
        public string FirstName { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        [RegularExpression("^[ა-ჰ]+$|^[a-zA-Z]+$", ErrorMessage = "Only Georgian or Latin letters allowed.")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^(კაცი|ქალი)$")]
        public string Gender { get; set; }

        [Required, StringLength(11, MinimumLength = 11)]
        [RegularExpression("^[0-9]{11}$")]
        public string PersonalNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Person), nameof(ValidateBirthDate))]
        public DateTime BirthDate { get; set; }

        [Required]
        public int CityId { get; set; }
        public City City { get; set; }

        public string? ImagePath { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public ICollection<PersonRelation> RelatedPeople { get; set; }
        public ICollection<PersonRelation> RelatedToPeople { get; set; } // reverse nav

        public static ValidationResult? ValidateBirthDate(DateTime birthDate, ValidationContext context)
        {
            return birthDate <= DateTime.Today.AddYears(-18)
                ? ValidationResult.Success
                : new ValidationResult("Person must be at least 18 years old.");
        }
    }
}
