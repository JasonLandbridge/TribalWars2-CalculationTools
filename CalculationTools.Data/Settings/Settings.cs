using CalculationTools.Common;
using Newtonsoft.Json;
using nucs.JsonSettings;
using System.Collections.Generic;

namespace CalculationTools.Data
{
    /// <summary>
    /// The settings class holding all settings stored in a JSON file
    /// <para>See: https://github.com/Nucs/JsonSettings/ </para>
    /// </summary>
    public class Settings : JsonSettings, ISettings
    {
        #region Constructors

        public Settings()
        {

        }
        #endregion Constructors

        #region Properties

        public sealed override string FileName { get; set; } = "Settings.json";

        #region Settings

        #region BattleSimulator
        /// <summary>
        /// Holds the filter settings for the BattleResult in the BattleSimulator
        /// </summary>
        public virtual Dictionary<string, bool> BattleResultFilters { get; set; } = new Dictionary<string, bool>
        {
            [nameof(IsAttackStrengthShown)] = false,
            [nameof(IsDefenseStrengthShown)] = false,
            [nameof(IsResourcesLostShown)] = false,
        };

        [JsonIgnore]
        public bool IsAttackStrengthShown
        {
            get => BattleResultFilters[nameof(IsAttackStrengthShown)];
            set
            {
                BattleResultFilters[nameof(IsAttackStrengthShown)] = value;
                Save();
            }
        }

        [JsonIgnore]
        public bool IsDefenseStrengthShown
        {
            get => BattleResultFilters[nameof(IsDefenseStrengthShown)];
            set
            {
                BattleResultFilters[nameof(IsDefenseStrengthShown)] = value;
                Save();
            }
        }

        [JsonIgnore]
        public bool IsResourcesLostShown
        {
            get => BattleResultFilters[nameof(IsResourcesLostShown)];
            set
            {
                BattleResultFilters[nameof(IsResourcesLostShown)] = value;
                Save();
            }
        }


        #endregion

        #region Accounts
        public virtual List<Account> Accounts { get; set; }



        #endregion   
        #endregion

        #endregion


        public void SetDefaultValues()
        {
            Accounts = new List<Account> { new Account { ServerCountryCode = "nl" } };

            Save();
        }
    }
}
