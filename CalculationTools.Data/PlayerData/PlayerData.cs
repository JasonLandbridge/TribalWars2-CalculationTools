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

        #region Events

        public event EventHandler LoginDataIsUpdated;

        #endregion Events

        #region Properties

        public List<CharacterWorld> CharacterWorlds
        {
            get
            {
                //TODO retrieve from database
                return new List<CharacterWorld>();
            }
        }

        public bool IsLoggedIn { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime LastUpdatedLoginData { get; set; }
        public string Name { get; set; }
        public int PlayerId { get; set; }
        public int CharacterId { get; set; }

        #endregion Properties

        #region Methods

        public void SetLoginData(ILoginData loginData)
        {
            if (loginData == null) return;

            Name = loginData.Name;
            PlayerId = loginData.PlayerId;

            _gameDataRepository.UpdateWorlds(loginData.Worlds?.ToList());
            _gameDataRepository.UpdateWorlds(loginData.Characters?.ToList());

            LastUpdatedLoginData = DateTime.Now;

            LoginDataIsUpdated.Invoke(this, EventArgs.Empty);

        }

        public void SetActiveCharacterId(int characterId)
        {
            CharacterId = characterId;
        }

        public void SetCharacterData(ICharacterData characterData)
        {
            if (characterData == null) return;

            // TODO make conversion
            CharacterData cData = _mapper.Map<CharacterData>(characterData);

            // Add the owning characterId of this village
            foreach (IVillage village in cData.Villages)
            {
                village.CharacterId = CharacterId;
            }

            _gameDataRepository.UpdateVillages(cData.Villages);


        }
        public void SetGroups(IList<IGroup> groupList)
        {
            _gameDataRepository.UpdateGroups(groupList.ToList());
        }
        #endregion Methods
    }
}
