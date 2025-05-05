using System.ComponentModel.DataAnnotations;

namespace TBCTest.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; } // მობილური, ოფისის, სახლის

        [Required, StringLength(50, MinimumLength = 4)]
        public string Number { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
