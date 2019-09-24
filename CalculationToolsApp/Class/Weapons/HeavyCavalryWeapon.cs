using System;
using System.Collections.Generic;
using System.Text;
using TribalWars2_CalculationTools.Class.Enum;

namespace TribalWars2_CalculationTools.Class.Weapons
{
    public class HeavyCavalryWeapon : BaseWeapon
    {
        public override string Code { get; } = "HeavyCavalryWeapon";

        public override List<WeaponModifier> WeaponModifiers { get; } = new List<WeaponModifier>
        {
            new WeaponModifier
            {
                AtkModifier = 0.10m,
                DefModifer = 0.10m
            },
            new WeaponModifier
            {
                AtkModifier = 0.20m,
                DefModifer = 0.20m
            },
            new WeaponModifier
            {
                AtkModifier = 0.30m,
                DefModifer = 0.30m
            }
        };

        public override UnitType BelongsToUnitType { get; } = UnitType.HeavyCavalry;

    }

}