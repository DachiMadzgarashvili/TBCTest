using Swashbuckle.AspNetCore.Annotations;

namespace TBCTest.Models.DTOs
{
    public class PersonDto
    {
        [SwaggerSchema("Unique identifier")]
        public int Id { get; set; }

        [SwaggerSchema("Georgian first name")]
        public string FirstNameGe { get; set; }

        [SwaggerSchema("English first name")]
        public string FirstNameEn { get; set; }

        [SwaggerSchema("Georgian last name")]
        public string LastNameGe { get; set; }

        [SwaggerSchema("English last name")]
        public string LastNameEn { get; set; }

        [SwaggerSchema("Gender")]
        public string Gender { get; set; }

        [SwaggerSchema("Personal number")]
        public string PersonalNumber { get; set; }

        [SwaggerSchema("Birth date", Format = "date")]
        public DateTime BirthDate { get; set; }

        [SwaggerSchema("City identifier")]
        public int CityId { get; set; }

        [SwaggerSchema("Georgian city name")]
        public string CityNameGe { get; set; }

        [SwaggerSchema("English city name")]
        public string CityNameEn { get; set; }

        [SwaggerSchema("Phone numbers")]
        public List<PhoneNumberDto> PhoneNumbers { get; set; }

        [SwaggerSchema("Image URL path")]
        public string ImagePath { get; set; }
    }
}