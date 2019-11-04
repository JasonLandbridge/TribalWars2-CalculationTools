using CalculationTools.Common;
using CalculationTools.Common.Entities.World;
using System;
using System.Collections.Generic;
using AutoMapper;

namespace CalculationTools.Data
{
    public class GameDataRepository : IGameDataRepository
    {
        private readonly IMapper _mapper;
        private readonly CalculationToolsDBContext _context;

        public GameDataRepository(IMapper mapper)
        {
            _mapper = mapper;
            _context = new CalculationToolsDBContext();
        }

        public void UpdateVillages(List<IVillage> villages)
        {
            foreach (IVillage village in villages)
            {
                _context.Villages.Add(_mapper.Map<Village>(village));
            }
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }


    }
}
