using System;
using System.Collections.Generic;
using System.Text;
using TribalWars2_CalculationTools.Class.Enum;
using TribalWars2_CalculationTools.Models;

namespace TribalWars2_CalculationTools.Class.Units
{
    public class Catapult : BaseUnit
    {
        public override string Name { get; } = "Catapult";
        public override UnitType UnitType { get; set; } = UnitType.Catapult;
        public override int WoodCost { get; } = 320;
        public override int ClayCost { get; } = 400;
        public override int IronCost { get; } = 100;
        public override int ProvisionCost { get; } = 8;
        public override int FightingPower { get; set; } = 100;
        public override int DefenseFromInfantry { get; set; } = 100;
        public override int DefenseFromCavalry { get; set; } = 50;
        public override int DefenseFromArchers { get; set; } = 100;
        public override int LoadCapacity { get; set; } = 0;
        public override TimeSpan BaseRecruitmentTime { get; set; } = new TimeSpan(0, 7, 30);
        public override TimeSpan TravelTimePerTile { get; set; } = new TimeSpan(0, 24, 0);


    }
}
