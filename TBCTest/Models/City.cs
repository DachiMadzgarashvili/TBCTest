using System.ComponentModel.DataAnnotations;

namespace TBCTest.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required, StringLength(100, MinimumLength = 2)]
        [RegularExpression("^[ა-ჰ ]+$", ErrorMessage = "NameGe must contain only Georgian letters.")]
        public string NameGe { get; set; }

        [Required, StringLength(100, MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "NameEn must contain only Latin letters.")]
        public string NameEn { get; set; }

        public ICollection<Person> People { get; set; }
    }
}
