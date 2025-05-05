using System.ComponentModel.DataAnnotations;

namespace TBCTest.Models
{
    public class PersonRelation
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("^(Colleague|Acquaintance|Relative|Other)$")]
        public string RelationType { get; set; }


        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int RelatedPersonId { get; set; }
        public Person RelatedPerson { get; set; }
    }
}
