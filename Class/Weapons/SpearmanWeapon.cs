using System;
using System.Collections.Generic;
using System.Text;
using TribalWars2_CalculationTools.Class.Enum;

namespace TribalWars2_CalculationTools.Class.Weapons
{
    public class SpearmanWeapon : BaseWeapon
    {
        public override string Code { get; } = "SpearmanWeapon";

        public override List<WeaponModifier> WeaponModifiers { get; } = new List<WeaponModifier>
        {
            new WeaponModifier
            {
                //TODO Unconfirmed
                AtkModifier = 0.10m,
                DefModifer = 0.10m
            },
            new WeaponModifier
            {
                //TODO Unconfirmed
                AtkModifier = 0.15m,
                DefModifer = 0.15m
            },
            new WeaponModifier
            {
                AtkModifier = 0.20m,
                DefModifer = 0.03m
            }
        };

        public override UnitType BelongsToUnitType { get; } = UnitType.Spearman;

    }

}