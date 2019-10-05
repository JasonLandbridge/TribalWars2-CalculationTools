using System;
using System.Text.RegularExpressions;
using CalculationTools.Core.Enums;

namespace CalculationTools.Core
{
    public abstract class BaseUnit
    {

        public abstract string Code { get; }
        public string Name => Regex.Replace(this.Code, "([A-Z])", " $1").Trim();

        public abstract UnitType UnitType { get; set; }
        public abstract AttackType AttackType { get; set; }

        #region Unit Cost

        /// <summary>
        /// The cost of Wood for this unit.
        /// </summary>
        public abstract int WoodCost { get; }

        /// <summary>
        /// The cost of Clay for this unit.
        /// </summary>
        public abstract int ClayCost { get; }

        /// <summary>
        /// The cost of Iron for this unit.
        /// </summary>
        public abstract int IronCost { get; }

        /// <summary>
        /// Returns a ResourceSet with the cost of this unit
        /// </summary>
        public ResourceSet ResourceCost => new ResourceSet(WoodCost, ClayCost, IronCost);
        #endregion

        public abstract int ProvisionCost { get; }

        /*Fight values*/
        public abstract int FightingPower { get; set; }
        public abstract int DefenseFromInfantry { get; set; }
        public abstract int DefenseFromCavalry { get; set; }
        public abstract int DefenseFromArchers { get; set; }

        public abstract int LoadCapacity { get; set; }

        public abstract TimeSpan BaseRecruitmentTime { get; set; }
        public abstract TimeSpan TravelTimePerTile { get; set; }

        public string ImagePath => $"/Resources/Img/units/unit_{this.Name.ToLower().Replace(' ', '_')}.png";

    }
}
