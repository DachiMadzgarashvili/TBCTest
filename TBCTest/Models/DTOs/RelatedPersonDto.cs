using Swashbuckle.AspNetCore.Annotations;

namespace TBCTest.Models.DTOs
{
    /// <summary>
    /// A minimal DTO for a person linked via a relation.
    /// </summary>
    public class RelatedPersonDto
    {
        [SwaggerSchema("Identifier of the related person")]
        public int Id { get; set; }

        [SwaggerSchema("Full name in Georgian")]
        public string FullNameGe { get; set; }

        [SwaggerSchema("Full name in English")]
        public string FullNameEn { get; set; }

        [SwaggerSchema("Type of relation")]
        public string RelationType { get; set; }
    }
}
