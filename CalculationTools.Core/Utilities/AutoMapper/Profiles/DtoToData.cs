using AutoMapper;
using CalculationTools.Common;
using CalculationTools.Common.Entities.World;
using CalculationTools.Data;
using System.Collections.Generic;
using System.Linq;

namespace CalculationTools.Core
{
    public class DtoToData : Profile
    {

        public DtoToData()
        {

            CreateMap<CharacterDataDTO, ICharacterData>().ReverseMap();
            CreateMap<ICharacterData, CharacterData>();

            CreateMap<List<Village>, IList<IVillage>>().ReverseMap();
            CreateMap<List<VillageDTO>, IList<IVillage>>().ReverseMap();

            CreateMap<VillageDTO, IVillage>().ReverseMap();
            CreateMap<Village, IVillage>().ReverseMap();


        }

    }


}
