﻿using System;
using System.Collections.Generic;
using System.Text;
using TribalWars2_CalculationTools.Class.Enum;
using TribalWars2_CalculationTools.Models;

namespace TribalWars2_CalculationTools.Class.Units
{
    public class LightCavalry : BaseUnit
    {
        public override string Code { get; } = "LightCavalry";
        public override UnitType UnitType { get; set; } = UnitType.Cavalry;
        public override int WoodCost { get; } = 125;
        public override int ClayCost { get; } = 100;
        public override int IronCost { get; } = 250;
        public override int ProvisionCost { get; } = 4;
        public override int FightingPower { get; set; } = 130;
        public override int DefenseFromInfantry { get; set; } = 30;
        public override int DefenseFromCavalry { get; set; } = 40;
        public override int DefenseFromArchers { get; set; } = 30;
        public override int LoadCapacity { get; set; } = 50;
        public override TimeSpan BaseRecruitmentTime { get; set; } = new TimeSpan(0, 6, 0);
        public override TimeSpan TravelTimePerTile { get; set; } = new TimeSpan(0, 8, 0);


    }
}
