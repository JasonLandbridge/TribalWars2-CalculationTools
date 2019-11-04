using nucs.JsonSettings;
using nucs.JsonSettings.Autosave;
using System;
using System.Diagnostics;
using System.IO;
using CalculationTools.Common;

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

        public void SetupSettings()
        {

        }
    }
}
