﻿using System.Collections.Generic;

namespace CalculationTools.Core
{
    public class SwordsmanWeapon : BaseWeapon
    {
        public override string Code { get; } = "SwordsmanWeapon";

        public override List<WeaponModifier> WeaponModifiers { get; } = new List<WeaponModifier>
        {
            new WeaponModifier
            {
                AtkModifier = 0.05m,
                DefModifer = 0.10m
            },
            new WeaponModifier
            {
                AtkModifier = 0.10m,
                DefModifer = 0.20m
            },
            new WeaponModifier
            {
                AtkModifier = 0.20m,
                DefModifer = 0.30m
            }
        };

        public override UnitType? BelongsToUnitType { get; } = UnitType.Swordsman;

    }

}