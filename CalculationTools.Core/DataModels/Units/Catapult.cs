using System;

namespace CalculationTools.Core
{
    public class Catapult : BaseUnit
    {
        public override string Code { get; } = "Catapult";
        public override UnitType UnitType { get; set; } = UnitType.Catapult;
        public override AttackType AttackType { get; set; } = AttackType.Special;

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
