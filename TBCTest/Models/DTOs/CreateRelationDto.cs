using System.ComponentModel.DataAnnotations;

namespace TBCTest.Models.DTOs
{
    public class CreateRelationDto
    {
        [Required]
        public int PersonId { get; set; }

        [Required]
        public int RelatedPersonId { get; set; }

        [Required]
        [RegularExpression("^(Colleague|Acquaintance|Relative|Other)$", ErrorMessage = "Invalid relation type.")]
        public string RelationType { get; set; }
    }
}
