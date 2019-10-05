using System.Collections.Generic;

namespace CalculationTools.Core
{
    public class LightCavalryWeapon : BaseWeapon
    {
        public override string Code { get; } = "LightCavalryWeapon";

        public override List<WeaponModifier> WeaponModifiers { get; } = new List<WeaponModifier>
        {
            new WeaponModifier
            {
                AtkModifier = 0.10m,
                DefModifer = 0.05m
            },
            new WeaponModifier
            {
                AtkModifier = 0.20m,
                DefModifer = 0.10m
            },
            new WeaponModifier
            {
                AtkModifier = 0.30m,
                DefModifer = 0.20m
            }
        };

        public override UnitType? BelongsToUnitType { get; } = UnitType.LightCavalry;

    }

}