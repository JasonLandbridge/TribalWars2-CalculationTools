namespace CalculationTools.Core
{
    public struct BattleConfig
    {
        public BattleConfig(UnitSet atkUnits, UnitSet defUnits, BattleMeta battleMeta)
        {
            AtkUnits = atkUnits;
            DefUnits = defUnits;
            BattleMeta = battleMeta;
        }

        #region Properties

        public UnitSet AtkUnits { get; set; }
        public BattleMeta BattleMeta { get; set; }
        public UnitSet DefUnits { get; set; }

        /// <summary>
        /// Checks if there are any number of units in this BattleConfig.
        /// </summary>
        public bool HasUnits => AtkUnits.HasUnits && DefUnits.HasUnits;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns the paladin weapon used on the attacking side of the battle.
        /// <para>Returns an empty WeaponSet if there is no paladin present!</para>
        /// </summary>
        /// <returns></returns>
        public WeaponSet GetAtkWeapon()
        {
            if (BattleMeta.AtkWeapon == null || AtkUnits.Paladin <= 0 || BattleMeta.AtkWeapon.BelongsToUnitType == null)
            {
                return new WeaponSet();
            }

            UnitType unitType = (UnitType)BattleMeta.AtkWeapon.BelongsToUnitType;
            return GameData.GetWeapon(unitType, BattleMeta.AtkWeaponLevel);
        }

        /// <summary>
        /// Returns the paladin weapon used on the defending side of the battle.
        /// <para>Returns an empty WeaponSet if there is no paladin present!</para>
        /// </summary>
        /// <returns></returns>
        public WeaponSet GetDefWeapon()
        {
            if (BattleMeta.DefWeapon == null || DefUnits.Paladin <= 0 || BattleMeta.DefWeapon.BelongsToUnitType == null)
            {
                return new WeaponSet();
            }
            UnitType unitType = (UnitType)BattleMeta.DefWeapon.BelongsToUnitType;
            return GameData.GetWeapon(unitType, BattleMeta.DefWeaponLevel);
        }

        public void SetAtkUnits(UnitSet atkUnits)
        {
            AtkUnits = atkUnits;
        }

        public void SetDefUnits(UnitSet defUnits)
        {
            DefUnits = defUnits;
        }

        #endregion Methods
    }
}