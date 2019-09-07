using System;
using System.Collections.Generic;
using System.Text;
using TribalWars2_CalculationTools.Class.Enum;

namespace TribalWars2_CalculationTools.Class.Units
{
    public abstract class BaseUnit
    {
        public abstract string Name { get; }
        public abstract UnitType UnitType { get; set; }

        /*Unit Cost*/
        public abstract int WoodCost { get; }
        public abstract int ClayCost { get; }
        public abstract int IronCost { get; }
        public abstract int ProvisionCost { get; }

        /*Fight values*/
        public abstract int FightingPower { get; set; }
        public abstract int DefenseFromInfantry { get; set; }
        public abstract int DefenseFromCavalry { get; set; }
        public abstract int DefenseFromArchers { get; set; }

        public abstract int LoadCapacity { get; set; }

        public abstract TimeSpan BaseRecruitmentTime { get; set; }
        public abstract TimeSpan TravelTimePerTile { get; set; }

        public abstract int NumberOnAttack { get; set; }
        public abstract int NumberOnDefense { get; set; }

        public string ImagePath => $"/Resources/Img/unit_{this.Name.ToLower().Replace(' ', '_')}.png";
    }
}
