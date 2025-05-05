using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TBCTest.Models.DTOs
{
    public class CreatePersonDto
    {
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
        public DateTime BirthDate { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        [MinLength(1)]
        public List<PhoneNumberDto> PhoneNumbers { get; set; }
    }
}
