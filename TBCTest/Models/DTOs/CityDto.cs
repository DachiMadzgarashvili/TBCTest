
using Swashbuckle.AspNetCore.Annotations;

namespace TBCTest.Models.DTOs
{
    public class CityDto
    {
        [SwaggerSchema("Unique identifier")]
        public int Id { get; set; }

        [SwaggerSchema("Georgian name")]
        public string NameGe { get; set; }

        [SwaggerSchema("English name")]
        public string NameEn { get; set; }
    }
}