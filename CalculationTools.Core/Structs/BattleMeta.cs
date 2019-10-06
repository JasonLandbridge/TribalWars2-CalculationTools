namespace CalculationTools.Core
{
    /// <summary>
    /// Used to collect all input values related to the battle simulator
    /// </summary>
    public struct BattleMeta
    {
        public bool GrandmasterBonus { get; set; }

        #region Weapons

        /// <summary>
        /// The weapon level used on the attacking side of the battle.
        /// </summary>
        public int AtkWeaponLevel { get; set; }

        /// <summary>
        /// The weapon type used on the attacking side of the battle.
        /// </summary>
        public BaseWeapon AtkWeapon { get; set; }

        /// <summary>
        /// The weapon level  used on the defending side of the battle.
        /// </summary>

        public int DefWeaponLevel { get; set; }

        /// <summary>
        /// The weapon type  used on the defending side of the battle.
        /// </summary>
        public BaseWeapon DefWeapon { get; set; }

        #endregion Weapons

        #region Buildings

        public FaithLevel AtkChurch { get; set; }
        public FaithLevel DefChurch { get; set; }
        public int WallLevel { get; set; }

        #endregion Buildings

        #region Meta

        public int Luck { get; set; }
        public int Morale { get; set; }
        public bool NightBonus { get; set; }
        public int WeaponMastery { get; set; }

        #endregion Meta

        public decimal AtkFaithBonus => AtkChurch.Modifier;
        public decimal DefFaithBonus => DefChurch.Modifier;

        public decimal MoralePercentage => Morale / 100m;
        public decimal LuckPercentage => 1m + (Luck / 100m);
        public decimal NightBonusPercentage => NightBonus ? 2m : 1m;
        public decimal GrandMasterBonusPercentage => GrandmasterBonus ? 0.1m : 0m;
        public decimal WeaponMasteryPercentage => WeaponMastery * 0.02m;
    }
}