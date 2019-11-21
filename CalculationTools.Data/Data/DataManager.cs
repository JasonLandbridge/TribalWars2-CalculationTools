using AutoMapper;
using CalculationTools.Common;
using nucs.JsonSettings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CalculationTools.Data
{

    public class DataManager : IDataManager
    {
        #region Fields

        private readonly IGameDataRepository _gameDataRepository;
        private readonly IMapper _mapper;
        private readonly Settings _settings = new Settings();

        #endregion Fields

        #region Events



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

        public ISettings Settings => _settings;

        #endregion Properties

        #region Methods

        public void SetCharacterData(ICharacterData characterData)
        {
            if (characterData == null) return;

            CharacterData cData = _mapper.Map<CharacterData>(characterData);

            // Add the owning characterId of this village
            foreach (IVillage village in cData.Villages)
            {
                // village.WorldId = ActiveWorldId;
                // village.CharacterId = ActiveCharacterId;
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


        #region Villages

        public List<Village> GetVillages(int characterId = 0)
        {
            return _gameDataRepository.GetVillages(characterId);
        }

        #endregion

        #region Groups

        public void SetGroups(IList<IGroup> groupList)
        {
            _gameDataRepository.UpdateGroups(groupList.ToList());
        }

        #endregion

        public void SetConnectionResult(ConnectResult connectResult)
        {
            DataEvents.InvokeConnectionResult(connectResult);
        }

        #endregion Methods
    }
}
