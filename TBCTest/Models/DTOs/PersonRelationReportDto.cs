using Swashbuckle.AspNetCore.Annotations;

namespace TBCTest.Models.DTOs
{
    /// <summary>
    /// Report item showing how many related people a person has, grouped by relation type.
    /// </summary>
    public class PersonRelationReportDto
    {
        [SwaggerSchema("Person identifier")]
        public int PersonId { get; set; }

        [SwaggerSchema("Full name of the person")]
        public string FullName { get; set; }

        [SwaggerSchema("Dictionary of relation type → count")]
        public Dictionary<string, int> RelationCounts { get; set; }
    }
}
