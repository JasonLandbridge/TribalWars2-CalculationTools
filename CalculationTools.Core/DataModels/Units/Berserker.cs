using System;

namespace CalculationTools.Core
{
    public class Berserker : BaseUnit
    {
        public override string Code { get; } = "Berserker";
        public override UnitType UnitType { get; set; } = UnitType.Berserker;
        public override AttackType AttackType { get; set; } = AttackType.Infantry;
        public override int WoodCost { get; } = 1200;
        public override int ClayCost { get; } = 1200;
        public override int IronCost { get; } = 2400;
        public override int ProvisionCost { get; } = 6;
        public override int FightingPower { get; set; } = 300;
        public override int DefenseFromInfantry { get; set; } = 100;
        public override int DefenseFromCavalry { get; set; } = 100;
        public override int DefenseFromArchers { get; set; } = 50;
        public override int LoadCapacity { get; set; } = 10;
        public override TimeSpan BaseRecruitmentTime { get; set; } = new TimeSpan(0, 20, 0);
        public override TimeSpan TravelTimePerTile { get; set; } = new TimeSpan(0, 14, 0);


    }
}