using System;

namespace CalculationTools.Core
{
    public class Trebuchet : BaseUnit
    {
        public override string Code { get; } = "Trebuchet";
        public override UnitType UnitType { get; set; } = UnitType.Trebuchet;
        public override AttackType AttackType { get; set; } = AttackType.Special;

        public override int WoodCost { get; } = 4000;
        public override int ClayCost { get; } = 2000;
        public override int IronCost { get; } = 1000;
        public override int ProvisionCost { get; } = 10;
        public override int FightingPower { get; set; } = 30;
        public override int DefenseFromInfantry { get; set; } = 200;
        public override int DefenseFromCavalry { get; set; } = 250;
        public override int DefenseFromArchers { get; set; } = 200;
        public override int LoadCapacity { get; set; } = 0;
        public override TimeSpan BaseRecruitmentTime { get; set; } = new TimeSpan(0, 20, 0);
        public override TimeSpan TravelTimePerTile { get; set; } = new TimeSpan(0, 50, 0);


    }
}