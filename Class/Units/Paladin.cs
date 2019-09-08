using System;
using System.Collections.Generic;
using System.Text;
using TribalWars2_CalculationTools.Class.Enum;
using TribalWars2_CalculationTools.Models;

namespace TribalWars2_CalculationTools.Class.Units
{
    public class Paladin : BaseUnit
    {
        public override string Name { get; } = "Paladin";
        public override UnitType UnitType { get; set; } = UnitType.Paladin;
        public override int WoodCost { get; } = 0;
        public override int ClayCost { get; } = 0;
        public override int IronCost { get; } = 0;
        public override int ProvisionCost { get; } = 1;
        public override int FightingPower { get; set; } = 150;
        public override int DefenseFromInfantry { get; set; } = 250;
        public override int DefenseFromCavalry { get; set; } = 400;
        public override int DefenseFromArchers { get; set; } = 150;
        public override int LoadCapacity { get; set; } = 100;
        public override TimeSpan BaseRecruitmentTime { get; set; } = new TimeSpan(6, 0, 0);
        public override TimeSpan TravelTimePerTile { get; set; } = new TimeSpan(0, 8, 0);

       public Paladin(CalculatorData parent) : base(parent)
        {

        }
    }
}
