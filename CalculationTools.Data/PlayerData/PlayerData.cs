using AutoMapper;
using CalculationTools.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculationTools.Data
{
    public class PlayerData : BasePropertyChanged, IPlayerData
    {
        private readonly IMapper _mapper;
        private readonly IGameDataRepository _gameDataRepository;

        #region Constructors

        public PlayerData(IMapper mapper, IGameDataRepository gameDataRepository)
        {
            _mapper = mapper;
            _gameDataRepository = gameDataRepository;
        }

        #endregion Constructors


        #region Properties

        public bool IsLoggedIn { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime LastUpdatedLoginData { get; set; }
        public string Name { get; set; }
        public int PlayerId { get; set; }

        #endregion Properties

        #region Methods






        public void SetGroups(IList<IGroup> groupList)
        {
            _gameDataRepository.UpdateGroups(groupList.ToList());
        }
        #endregion Methods
    }
}
