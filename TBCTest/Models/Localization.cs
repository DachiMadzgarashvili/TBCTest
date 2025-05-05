using System.ComponentModel.DataAnnotations;

namespace TBCTest.Models
{
    public class Localization
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Language { get; set; } // e.g. "en-US", "ka-GE"

        [Required]
        [MaxLength(100)]
        public string Key { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Value { get; set; }
    }
}
