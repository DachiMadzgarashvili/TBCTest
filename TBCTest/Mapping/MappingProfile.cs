using AutoMapper;
using TBCTest.Models;
using TBCTest.Models.DTOs;

namespace TBCTest.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCityDto, City>();
            CreateMap<City, CityDto>().ReverseMap();

            CreateMap<CreatePersonDto, Person>();
            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.CityNameGe, opt => opt.MapFrom(src => src.City.NameGe))
                .ForMember(dest => dest.CityNameEn, opt => opt.MapFrom(src => src.City.NameEn));

            CreateMap<PhoneNumber, PhoneNumberDto>().ReverseMap();

            CreateMap<PersonRelation, RelatedPersonDto>()
                .ForMember(dest => dest.RelatedPersonId, opt => opt.MapFrom(src => src.RelatedPerson.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>
                    $"{src.RelatedPerson.FirstNameGe} {src.RelatedPerson.LastNameGe}"));


        }
    }
}
