using System;
using System.Collections.Generic;
using System.Text;

namespace TribalWars2_CalculationTools.Class.Enum
{
    public struct WeaponSet
    {
        public UnitType BelongsToUnitType { get; set; }

        public decimal AtkModifier { get; set; }

        public decimal DefModifier { get; set; }


        public WeaponSet(UnitType belongsToUnitType, decimal atkModifier, decimal defModifier)
        {
            BelongsToUnitType = belongsToUnitType;
            AtkModifier = atkModifier;
            DefModifier = defModifier;

        }
    }
}
