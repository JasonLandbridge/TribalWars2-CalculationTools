﻿using AutoMapper;
using CalculationTools.Common;
using CalculationTools.Common.Data;
using CalculationTools.Common.Entities.World;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace CalculationTools.Data
{
    public class PlayerData : BasePropertyChanged, IPlayerData
    {
        private readonly IMapper _mapper;
        private readonly ICalculationToolsDataStore _dataStore;

        #region Constructors

        public PlayerData(IMapper mapper, ICalculationToolsDataStore dataStore)
        {
            _mapper = mapper;
            _dataStore = dataStore;
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
                List<CharacterWorld> worldList =
                    Characters.Select(character => new CharacterWorld(character)).ToList();
                return worldList;
            }
        }

        public bool IsLoggedIn { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime LastUpdatedLoginData { get; set; }
        public string Name { get; set; }
        public int PlayerId { get; set; }
        public List<VillageGroup> VillageGroups { get; set; } = new List<VillageGroup>();

        private List<ICharacter> Characters { get; set; }
        private List<IWorld> Worlds { get; set; }

        #endregion Properties

        #region Methods

        public void SetGroups(IList<IGroup> groupList)
        {
            if (groupList == null) return;

            VillageGroups = new List<VillageGroup>();

            foreach (IGroup group in groupList)
            {
                VillageGroups.Add(group as VillageGroup);
            }


        }

        public void SetLoginData(ILoginData loginData)
        {
            if (loginData == null) return;

            Name = loginData.Name;
            PlayerId = loginData.PlayerId;
            // Convert from IList<IWorld> to List<World>

            Worlds = loginData.Worlds?.ToList();
            Characters = loginData.Characters?.ToList();


            LastUpdatedLoginData = DateTime.Now;

            LoginDataIsUpdated.Invoke(this, EventArgs.Empty);

        }

        public void SetCharacterData(ICharacterData characterData)
        {
            if (characterData == null) return;

            // TODO make conversion
            CharacterData cData = _mapper.Map<CharacterData>(characterData);

            _dataStore.UpdateVillages(characterData.Villages);


        }

        #endregion Methods
    }
}
