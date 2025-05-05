using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace TBCTest.Models.DTOs
{
    /// <summary>
    /// Request to link two persons together.
    /// </summary>
    public class CreateRelationDto
    {
        [Required]
        [SwaggerSchema("Identifier of the main person")]
        public int PersonId { get; set; }

        [Required]
        [SwaggerSchema("Identifier of the related person")]
        public int RelatedPersonId { get; set; }

        [Required]
        [RegularExpression("^(Colleague|Acquaintance|Relative|Other)$",
            ErrorMessage = "RelationType must be one of: Colleague, Acquaintance, Relative, Other.")]
        [SwaggerSchema("Type of relation (Colleague, Acquaintance, Relative, Other)")]
        public string RelationType { get; set; }
    }
}
