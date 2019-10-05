namespace CalculationTools.Core
{
    /// <summary>
    /// Used to collect all input values related to the battle simulator
    /// </summary>
    public struct BattleMeta
    {

        public bool GrandmasterBonus { get; set; }

        #region Weapons
        public int AtkWeaponLevel { get; set; }
        public BaseWeapon AtkWeapon { get; set; }
        public int DefWeaponLevel { get; set; }
        public BaseWeapon DefWeapon { get; set; }

        #endregion


        #region Buildings
        public FaithLevel AtkChurch { get; set; }
        public FaithLevel DefChurch { get; set; }
        public int WallLevel { get; set; }

        #endregion

        #region Meta
        public int Luck { get; set; }
        public int Morale { get; set; }
        public bool NightBonus { get; set; }
        public int WeaponMastery { get; set; }

        #endregion

    }
}
