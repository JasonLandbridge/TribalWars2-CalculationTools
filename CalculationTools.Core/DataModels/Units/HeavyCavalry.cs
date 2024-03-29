﻿using System;

namespace CalculationTools.Core
{
    public class HeavyCavalry : BaseUnit
    {

        public override string Code { get; } = "HeavyCavalry";
        public override UnitType UnitType { get; set; } = UnitType.HeavyCavalry;
        public override AttackType AttackType { get; set; } = AttackType.Cavalry;

        public override int WoodCost { get; } = 200;
        public override int ClayCost { get; } = 150;
        public override int IronCost { get; } = 600;
        public override int ProvisionCost { get; } = 6;
        public override int FightingPower { get; set; } = 150;
        public override int DefenseFromInfantry { get; set; } = 200;
        public override int DefenseFromCavalry { get; set; } = 160;
        public override int DefenseFromArchers { get; set; } = 180;
        public override int LoadCapacity { get; set; } = 50;
        public override TimeSpan BaseRecruitmentTime { get; set; } = new TimeSpan(0, 10, 0);
        public override TimeSpan TravelTimePerTile { get; set; } = new TimeSpan(0, 9, 0);

    }
}
