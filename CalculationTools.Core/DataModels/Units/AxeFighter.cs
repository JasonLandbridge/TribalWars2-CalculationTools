﻿using System;

namespace CalculationTools.Core
{
    public class AxeFighter : BaseUnit
    {
        public override string Code { get; } = "AxeFighter";
        public override UnitType UnitType { get; set; } = UnitType.AxeFighter;
        public override AttackType AttackType { get; set; } = AttackType.Infantry;

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
