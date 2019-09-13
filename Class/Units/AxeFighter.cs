using System;
using System.Collections.Generic;
using System.Text;
using TribalWars2_CalculationTools.Class.Enum;
using TribalWars2_CalculationTools.Models;

namespace TribalWars2_CalculationTools.Class.Units
{
    public class AxeFighter : BaseUnit
    {
        public override string Code { get; } = "AxeFighter";
        public override UnitType UnitType { get; set; } = UnitType.Infantry;
        public override int WoodCost { get; } = 60;
        public override int ClayCost { get; } = 30;
        public override int IronCost { get; } = 40;
        public override int ProvisionCost { get; } = 1;
        public override int FightingPower { get; set; } = 45;
        public override int DefenseFromInfantry { get; set; } = 10;
        public override int DefenseFromCavalry { get; set; } = 5;
        public override int DefenseFromArchers { get; set; } = 10;
        public override int LoadCapacity { get; set; } = 20;
        public override TimeSpan BaseRecruitmentTime { get; set; } = new TimeSpan(0, 2, 30);
        public override TimeSpan TravelTimePerTile { get; set; } = new TimeSpan(0, 14, 0);


    }
}
