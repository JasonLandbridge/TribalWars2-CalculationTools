using AutoMapper;
using CalculationTools.Common;
using CalculationTools.Common.Extensions;
using CalculationTools.Data;
using System.Collections.Generic;

namespace CalculationTools.Core
{
    public class DtoToData : Profile
    {

        public DtoToData()
        {

            CreateMap<CharacterDataDTO, ICharacterData>()
                .ReverseMap();
            CreateMap<ICharacterData, CharacterData>();

            CreateMap<List<Village>, IList<IVillage>>()
                .ReverseMap();
            CreateMap<List<VillageDTO>, IList<IVillage>>()
                .ReverseMap();

            CreateMap<VillageDTO, IVillage>()
                .ReverseMap();
            CreateMap<Village, IVillage>()
                .ReverseMap();
            CreateMap<Village, VillageDTO>()
                .ReverseMap();


            CreateMap<WorldDTO, World>(MemberList.None)
                .ReverseMap();

            CreateMap<CharacterDTO, World>(MemberList.None)
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.WorldName))
                .ForMember(dest => dest.WorldCode, opt => opt.MapFrom(src => src.WorldId))
                .ReverseMap();

            CreateMap<CharacterDTO, Character>(MemberList.Destination)
                .Ignore(x => x.Id)
                .Ignore(x => x.Worlds)
                .Ignore(x => x.Groups);




            CreateMap<GroupDTO, Group>(MemberList.Source);



        }

    }


}
