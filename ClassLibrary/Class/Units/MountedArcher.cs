using System;
using ClassLibrary.Enums;

namespace ClassLibrary.Class.Units
{
    public class MountedArcher : BaseUnit
    {
        public override string Code { get; } = "MountedArcher";
        public override UnitType UnitType { get; set; } = UnitType.MountedArcher;
        public override int WoodCost { get; } = 250;
        public override int ClayCost { get; } = 200;
        public override int IronCost { get; } = 100;
        public override int ProvisionCost { get; } = 5;
        public override int FightingPower { get; set; } = 150;
        public override int DefenseFromInfantry { get; set; } = 40;
        public override int DefenseFromCavalry { get; set; } = 30;
        public override int DefenseFromArchers { get; set; } = 50;
        public override int LoadCapacity { get; set; } = 50;
        public override TimeSpan BaseRecruitmentTime { get; set; } = new TimeSpan(0, 7, 30);
        public override TimeSpan TravelTimePerTile { get; set; } = new TimeSpan(0, 8, 0);


    }
}
