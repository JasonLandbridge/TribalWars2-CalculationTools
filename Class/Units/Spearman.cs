using System;
using System.Collections.Generic;
using System.Text;
using TribalWars2_CalculationTools.Class.Enum;
using TribalWars2_CalculationTools.Models;

namespace TribalWars2_CalculationTools.Class.Units
{
    public class Spearman : BaseUnit
    {
        public override string Name { get; } = "Spearman";
        public override UnitType UnitType { get; set; } = UnitType.Infantry;
        public override int WoodCost { get; } = 50;
        public override int ClayCost { get; } = 30;
        public override int IronCost { get; } = 20;
        public override int ProvisionCost { get; } = 1;
        public override int NumberOnDefense { get; set; } = 0;
        public override int FightingPower { get; set; } = 10;
        public override int DefenseFromInfantry { get; set; } = 25;
        public override int DefenseFromCavalry { get; set; } = 45;
        public override int DefenseFromArchers { get; set; } = 10;
        public override int LoadCapacity { get; set; } = 25;
        public override TimeSpan BaseRecruitmentTime { get; set; } = new TimeSpan(0, 1, 30);
        public override TimeSpan TravelTimePerTile { get; set; } = new TimeSpan(0, 14, 0);


        public Spearman(CalculatorData parent) : base(parent)
        {

        }
    }
}
