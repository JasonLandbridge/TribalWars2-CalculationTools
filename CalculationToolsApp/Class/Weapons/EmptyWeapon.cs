using System;
using System.Collections.Generic;
using System.Text;
using TribalWars2_CalculationTools.Class.Enum;

namespace TribalWars2_CalculationTools.Class.Weapons
{
    public class EmptyWeapon : BaseWeapon
    {
        public override string Code { get; } = " - ";

        public override List<WeaponModifier> WeaponModifiers { get; } = new List<WeaponModifier>
        {
            new WeaponModifier
            {
                AtkModifier = 0.00m,
                DefModifer = 0.0m
            },
            new WeaponModifier
            {
                AtkModifier = 0.0m,
                DefModifer = 0.0m
            },
            new WeaponModifier
            {
                AtkModifier = 0.0m,
                DefModifer = 0.0m
            }
        };

        public override UnitType BelongsToUnitType { get; } = UnitType.None;

    }

}