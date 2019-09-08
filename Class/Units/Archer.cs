using System;
using System.Collections.Generic;
using System.Text;
using TribalWars2_CalculationTools.Class.Enum;
using TribalWars2_CalculationTools.Models;

namespace TribalWars2_CalculationTools.Class.Units
{
    public class Archer : BaseUnit
    {
        public override string Name { get; } = "Archer";
        public override UnitType UnitType { get; set; } = UnitType.Archer;
        public override int WoodCost { get; } = 80;
        public override int ClayCost { get; } = 30;
        public override int IronCost { get; } = 60;
        public override int ProvisionCost { get; } = 1;
        public override int FightingPower { get; set; } = 25;
        public override int DefenseFromInfantry { get; set; } = 10;
        public override int DefenseFromCavalry { get; set; } = 30;
        public override int DefenseFromArchers { get; set; } = 60;
        public override int LoadCapacity { get; set; } = 10;
        public override TimeSpan BaseRecruitmentTime { get; set; } = new TimeSpan(0, 3, 0);

        public override TimeSpan TravelTimePerTile { get; set; } = new TimeSpan(0, 14, 0);

        public Archer(CalculatorData parent) : base(parent)
        {

        }
    }
}
