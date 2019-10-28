using System.Collections.Generic;
using System.ComponentModel;
using AutoMapper;
using CalculationTools.Common;
using CalculationTools.Data;

namespace CalculationTools.Core
{
    public class DtoToData : Profile
    {

        public DtoToData()
        {

            CreateMap<CharacterDataDTO, ICharacterData>().ReverseMap();
            CreateMap<CharacterData, ICharacterData>().ReverseMap();

            CreateMap<List<Village>, IList<IVillage>>().ReverseMap();
            CreateMap<List<VillageDTO>, IList<IVillage>>().ReverseMap();

            CreateMap<VillageDTO, IVillage>().ReverseMap();
            CreateMap<Village, IVillage>().ReverseMap();


        }

    }


}
