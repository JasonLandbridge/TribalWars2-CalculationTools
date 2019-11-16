using AutoMapper;
using CalculationTools.Common;
using nucs.JsonSettings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CalculationTools.Data
{

    public class DataManager : IDataManager
    {
        #region Fields

        private readonly IGameDataRepository _gameDataRepository;
        private readonly IMapper _mapper;
        private readonly Settings _settings = new Settings();
        public string WorldId;

        #endregion Fields

        #region Events

        public event EventHandler LoginDataIsUpdated;


        #endregion Events


        #region Constructors

        public DataManager(IGameDataRepository gameDataRepository, IMapper mapper)
        {
            _gameDataRepository = gameDataRepository;
            _mapper = mapper;

            string path = $"{AppDomain.CurrentDomain.BaseDirectory}{_settings.FileName}";
            if (!File.Exists(path))
            {
                _settings = JsonSettings.Construct<Settings>();
                _settings.SetDefaultValues();
                _settings.Save();
            }

            try
            {
                _settings = JsonSettings.Load<Settings>(_settings.FileName);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

        }

        #endregion Constructors

        #region Properties
        public int CharacterId { get; set; }

        public ISettings Settings => _settings;

        #endregion Properties

        #region Methods

        public void SetLoginData(ILoginData loginData)
        {
            if (loginData == null) return;

            _gameDataRepository.UpdateLoginData(loginData);

            LoginDataIsUpdated.Invoke(this, EventArgs.Empty);

        }

        public void SetCharacterData(ICharacterData characterData)
        {
            if (characterData == null) return;

            // TODO make conversion
            CharacterData cData = _mapper.Map<CharacterData>(characterData);

            // Add the owning characterId of this village
            foreach (IVillage village in cData.Villages)
            {
                village.WorldId = WorldId;
                village.CharacterId = CharacterId;
            }

            _gameDataRepository.UpdateVillages(cData.Villages);


        }

        public void SetupSettings()
        {

        }

        public void UpdateAccount(Account account)
        {
            _gameDataRepository.UpdateAccount(account);
        }

        public void SetActiveCharacterId(int characterId)
        {
            CharacterId = characterId;
        }

        public void SetActiveWorldId(string worldId)
        {
            WorldId = worldId;
        }

        #region Account
        public Account AddAccount()
        {
            return _gameDataRepository.AddAccount();
        }

        public bool DeleteAccount(int accountId)
        {
            return _gameDataRepository.DeleteAccount(accountId);

        }

        public bool DeleteAccount(Account account)
        {
            return _gameDataRepository.DeleteAccount(account);
        }

        public Account GetAccount(int accountId, bool tw2AccountId = false)
        {
            return _gameDataRepository.GetAccount(accountId, tw2AccountId);
        }

        public List<Account> GetAccounts(bool onlyConfirmed = false)
        {
            return _gameDataRepository.GetAccounts(onlyConfirmed);
        }

        public List<Server> GetServers()
        {
            return _gameDataRepository.GetServers();
        }
        #endregion

        #endregion Methods
    }
}
