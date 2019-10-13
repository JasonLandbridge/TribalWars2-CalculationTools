using CalculationTools.Common;
using nucs.JsonSettings;
using nucs.JsonSettings.Autosave;
using System;
using System.IO;

namespace CalculationTools.Data
{

    public class DataManager : IDataManager
    {
        public ISettings Settings => _settings;

        private Settings _settings = new Settings();
        public DataManager()
        {
            SetupSettings();
        }

        public void SetupSettings()
        {
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}{_settings.FileName}";

            if (File.Exists(path))
            {
                _settings = JsonSettings.Load<Settings>(_settings.FileName).EnableAutosave();
            }
            else
            {
                _settings = JsonSettings.Construct<Settings>(_settings.FileName).EnableAutosave();
                _settings.SetDefaultValues();
            }
        }
    }
}
