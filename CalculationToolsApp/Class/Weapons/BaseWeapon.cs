using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TribalWars2_CalculationTools.Class.Enum;

namespace TribalWars2_CalculationTools.Class.Weapons
{
    public abstract class BaseWeapon
    {
        public abstract string Code { get; }

        public int UnitId => (int)BelongsToUnitType;

        public string Name => Regex.Replace(this.Code.Replace("Weapon", ""), "([A-Z])", " $1").Trim();

        public abstract List<WeaponModifier> WeaponModifiers { get; }

        public abstract UnitType BelongsToUnitType { get; }

        public decimal GetAtkModifier(int level)
        {
            if (level < 1 || level > 3)
            {
                return 0;
            }
            return WeaponModifiers[level - 1].AtkModifier;
        }

        public decimal GetDefModifier(int level)
        {
            if (level < 1 || level > 3)
            {
                return 0;
            }
            return WeaponModifiers[level - 1].DefModifer;
        }
    }
}
