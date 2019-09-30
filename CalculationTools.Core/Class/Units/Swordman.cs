using System;

namespace CalculationTools.Core
{
    public class Swordsman : BaseUnit
    {
        public override string Code { get; } = "Swordsman";
        public override UnitType UnitType { get; set; } = UnitType.Swordsman;
        public override int WoodCost { get; } = 30;
        public override int ClayCost { get; } = 30;
        public override int IronCost { get; } = 70;
        public override int ProvisionCost { get; } = 1;
        public override int FightingPower { get; set; } = 25;
        public override int DefenseFromInfantry { get; set; } = 55;
        public override int DefenseFromCavalry { get; set; } = 5;
        public override int DefenseFromArchers { get; set; } = 30;
        public override int LoadCapacity { get; set; } = 15;
        public override TimeSpan BaseRecruitmentTime { get; set; } = new TimeSpan(0, 2, 0);
        public override TimeSpan TravelTimePerTile { get; set; } = new TimeSpan(0, 18, 0);

    }
}
