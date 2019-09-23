﻿using System;
using System.Collections.Generic;
using System.Text;
using TribalWars2_CalculationTools.Class.Enum;

namespace TribalWars2_CalculationTools.Class.Weapons
{
    public class RamWeapon : BaseWeapon
    {
        public override string Code { get; } = "RamWeapon";

        public override List<WeaponModifier> WeaponModifiers { get; } = new List<WeaponModifier>
        {
            new WeaponModifier
            {
                AtkModifier = 0.25m,
                DefModifer = 0.05m
            },
            new WeaponModifier
            {
                AtkModifier = 0.50m,
                DefModifer = 0.10m
            },
            new WeaponModifier
            {
                AtkModifier = 1.00m,
                DefModifer = 0.20m
            }
        };

        public override UnitType BelongsToUnitType { get; } = UnitType.Ram;

    }

}