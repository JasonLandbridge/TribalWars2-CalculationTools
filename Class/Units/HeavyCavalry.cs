using System;
using System.Collections.Generic;
using System.Text;
using TribalWars2_CalculationTools.Class.Enum;
using TribalWars2_CalculationTools.Models;

namespace TribalWars2_CalculationTools.Class.Units
{
    public class HeavyCavalry : BaseUnit
    {
        public override string Name { get; } = "Heavy Cavalry";
        public override UnitType UnitType { get; set; } = UnitType.Cavalry;
        public override int WoodCost { get; } = 200;
        public override int ClayCost { get; } = 150;
        public override int IronCost { get; } = 600;
        public override int ProvisionCost { get; } = 6;
        public override int FightingPower { get; set; } = 150;
        public override int DefenseFromInfantry { get; set; } = 200;
        public override int DefenseFromCavalry { get; set; } = 160;
        public override int DefenseFromArchers { get; set; } = 180;
        public override int LoadCapacity { get; set; } = 50;
        public override TimeSpan BaseRecruitmentTime { get; set; } = new TimeSpan(0, 10, 0);
        public override TimeSpan TravelTimePerTile { get; set; } = new TimeSpan(0, 9, 0);

        public HeavyCavalry(CalculatorData parent) : base(parent)
        {

        }
    }
}
