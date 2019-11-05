using CalculationTools.Common;
using Newtonsoft.Json;
using nucs.JsonSettings;
using System.Collections.Generic;
using System.Linq;

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

        public virtual Dictionary<int, Account> Accounts { get; set; } = new Dictionary<int, Account>();

        public List<Account> GetAccounts(bool onlyConfirmed = false)
        {
            List<Account> listResult = new List<Account>();

            foreach (KeyValuePair<int, Account> accountPair in Accounts)
            {
                Account account = accountPair.Value;
                account.AccountID = accountPair.Key;

                if (onlyConfirmed)
                {
                    if (account.IsConfirmed)
                    {
                        listResult.Add(account);
                    }
                    continue;
                }
                listResult.Add(account);
            }

            return listResult;
        }

        public void SetAccount(Account account)
        {
            if (Accounts.ContainsKey(account.AccountID))
            {
                Accounts[account.AccountID] = account;
            }
            else
            {
                AddAccount(account);
            }

            Save();
        }

        public Account AddAccount(Account account = null)
        {
            if (account == null)
            {
                account = new Account { ServerCountryCode = "nl" };
            }

            if (Accounts.Count > 0)
            {
                Accounts.Add(Accounts.Keys.Max() + 1, account);
            }
            else
            {
                Accounts.Add(0, account);
            }

            Save();
            return account;
        }

        #endregion   
        #endregion

        #endregion


        public void SetDefaultValues()
        {
            AddAccount();

        }
    }
}
