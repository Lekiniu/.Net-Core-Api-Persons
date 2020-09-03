using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Persons.Data.Entities;
using System.Linq;
using Persons.Data.Models;

namespace Persons.Core.Profiles
{
    public static class Mapping
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                //cfg.ForAllMaps((typeMap, mappingExpression) => mappingExpression.MaxDepth(1));
                cfg.AddProfile<MappingProfiles>();

            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
             CreateMap<PersonTypes, PersonTypesModel>()
               .ForMember(dto => dto.TypeName, opt => opt.MapFrom(x => x.TypeName))
               .MaxDepth(1)
               .ReverseMap();

            CreateMap<Data.Entities.Persons, PersonsModel>()
                //.ForMember(dto => dto.RelatedPerson, opt => opt.MapFrom(x => x.RelatedPersons.Select(y => y.RelatedPerson)))
                // .ForMember(dto => dto.Type, opt => opt.MapFrom(x => x.RelatedPersons.Select(y => y.PersonType.TypeName)))
            .MaxDepth(1)
            .ReverseMap()
            .ForPath(x => x.AddressId, x => x.Ignore())
            .ForPath(x => x.Address, x => x.Ignore())
            .ForPath(x => x.PersonId, x => x.Ignore());

            CreateMap<Addresses, AddressesModel>()
                //.ForMember(dto => dto.PersonId, opt => opt.MapFrom(x => x.Person.PersonId))
                .MaxDepth(1)
                .ReverseMap()
                .ForPath(x => x.AddressId, x => x.Ignore())
                .ForPath(x => x.PersonId, x => x.Ignore())
                .ForMember(dto => dto.Person, x => x.Ignore());


            CreateMap<Files, FilesModel>()
              .MaxDepth(1)
              .ReverseMap()
              .ForPath(x => x.FileId, x => x.Ignore())
              .ForPath(x => x.PersonId, x => x.Ignore())
              .ForMember(dto => dto.Person, x => x.Ignore());


            CreateMap<RelatedPersons, RelatedPersonsModel>()
            .MaxDepth(1)
            .ReverseMap()
            //.ForPath(x => x.RelateId, x => x.Ignore())
            .ForPath(x => x.PersonId, x => x.Ignore())
            .ForPath(x => x.RelatedPersonId, x => x.Ignore())
            .ForMember(dto => dto.RelatedPerson, x => x.Ignore())
            .ForMember(dto => dto.Person, x => x.Ignore());

           
        }

    }
}
