using CalculationTools.Common.Data;
using nucs.JsonSettings;
using nucs.JsonSettings.Autosave;
using System;
using System.IO;

namespace CalculationTools.Data
{

    public class DataManager : IDataManager
    {
        private readonly IPlayerData _playerData;
        private readonly Settings _settings = new Settings();

        public ISettings Settings => _settings;

        public DataManager(IPlayerData playerData)
        {
            _playerData = playerData;

            string path = $"{AppDomain.CurrentDomain.BaseDirectory}{_settings.FileName}";
            if (!File.Exists(path))
            {
                _settings = JsonSettings.Construct<Settings>().EnableAutosave();
                _settings.SetDefaultValues();
                _settings.Save();
            }

            _settings = JsonSettings.Load<Settings>(_settings.FileName).EnableAutosave();

        }

        public void SetupSettings()
        {

        }
    }
}
