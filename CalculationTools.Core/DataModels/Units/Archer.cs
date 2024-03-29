﻿using System;

namespace CalculationTools.Core
{
    public class Archer : BaseUnit
    {
        public override string Code { get; } = "Archer";
        public override UnitType UnitType { get; set; } = UnitType.Archer;
        public override AttackType AttackType { get; set; } = AttackType.Archer;

        public override int WoodCost { get; } = 80;
        public override int ClayCost { get; } = 30;
        public override int IronCost { get; } = 60;
        public override int ProvisionCost { get; } = 1;
        public override int FightingPower { get; set; } = 25;
        public override int DefenseFromInfantry { get; set; } = 10;
        public override int DefenseFromCavalry { get; set; } = 30;
        public override int DefenseFromArchers { get; set; } = 60;
        public override int LoadCapacity { get; set; } = 10;
        public override TimeSpan BaseRecruitmentTime { get; set; } = new TimeSpan(0, 3, 0);

        public override TimeSpan TravelTimePerTile { get; set; } = new TimeSpan(0, 14, 0);

    }
}
