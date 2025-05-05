namespace TBCTest.Models.DTOs
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string FirstNameGe { get; set; }
        public string FirstNameEn { get; set; }
        public string LastNameGe { get; set; }
        public string LastNameEn { get; set; }
        public string Gender { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string? ImagePath { get; set; }
        public string CityNameGe { get; set; }
        public string CityNameEn { get; set; }
        public List<PhoneNumberDto> PhoneNumbers { get; set; }
        public List<RelatedPersonDto> RelatedPeople { get; set; }
    }
}