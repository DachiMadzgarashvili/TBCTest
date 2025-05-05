namespace TBCTest.Models.DTOs
{
    public class PersonRelationReportDto
    {
        public int PersonId { get; set; }
        public string FullName { get; set; }
        public Dictionary<string, int> RelationCounts { get; set; }
    }
}
