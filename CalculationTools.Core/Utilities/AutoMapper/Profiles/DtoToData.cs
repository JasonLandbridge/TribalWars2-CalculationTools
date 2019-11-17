using AutoMapper;
using CalculationTools.Common;
using CalculationTools.Common.Extensions;
using CalculationTools.Data;

namespace CalculationTools.Core
{
    public class DtoToData : Profile
    {

        public DtoToData()
        {

            CreateMap<CharacterDataDTO, ICharacterData>()
                .ReverseMap();
            CreateMap<ICharacterData, CharacterData>();

            CreateMap<VillageDTO, IVillage>()
                .ReverseMap();
            CreateMap<Village, IVillage>()
                .ReverseMap();

            CreateMap<VillageDTO, Village>(MemberList.Source);


            CreateMap<WorldDTO, World>(MemberList.None)
                .ReverseMap();

            CreateMap<CharacterDTO, World>(MemberList.None)
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.WorldName))
                .ForMember(dest => dest.WorldId, opt => opt.MapFrom(src => src.WorldId))
                .ReverseMap();

            CreateMap<CharacterDTO, Character>(MemberList.None);

            CreateMap<GroupDTO, Group>(MemberList.Source);



        }

    }


}
