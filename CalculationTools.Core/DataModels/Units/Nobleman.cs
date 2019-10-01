using System;

namespace CalculationTools.Core
{
    public class Nobleman : BaseUnit
    {
        public override string Code { get; } = "Nobleman";
        public override UnitType UnitType { get; set; } = UnitType.Nobleman;
        public override int WoodCost { get; } = 40000;
        public override int ClayCost { get; } = 50000;
        public override int IronCost { get; } = 50000;
        public override int ProvisionCost { get; } = 100;
        public override int FightingPower { get; set; } = 30;
        public override int DefenseFromInfantry { get; set; } = 100;
        public override int DefenseFromCavalry { get; set; } = 50;
        public override int DefenseFromArchers { get; set; } = 100;
        public override int LoadCapacity { get; set; } = 0;
        public override TimeSpan BaseRecruitmentTime { get; set; } = new TimeSpan(3, 0, 0);
        public override TimeSpan TravelTimePerTile { get; set; } = new TimeSpan(0, 35, 0);

    }
}
