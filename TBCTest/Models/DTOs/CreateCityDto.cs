using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace TBCTest.Models.DTOs
{
    public class CreateCityDto
    {
        [Required, StringLength(100, MinimumLength = 2)]
        [RegularExpression("^[ა-ჰ ]+$", ErrorMessage = "NameGe must contain only Georgian letters.")]
        [SwaggerSchema("Georgian city name")]
        public string NameGe { get; set; }

        [Required, StringLength(100, MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "NameEn must contain only Latin letters.")]
        [SwaggerSchema("English city name")]
        public string NameEn { get; set; }
    }
}
