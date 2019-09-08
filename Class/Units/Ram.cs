﻿using System;
using System.Collections.Generic;
using System.Text;
using TribalWars2_CalculationTools.Class.Enum;
using TribalWars2_CalculationTools.Models;

namespace TribalWars2_CalculationTools.Class.Units
{
    public class Ram : BaseUnit
    {
        public override string Name { get; } = "Ram";
        public override UnitType UnitType { get; set; } = UnitType.Ram;
        public override int WoodCost { get; } = 300;
        public override int ClayCost { get; } = 200;
        public override int IronCost { get; } = 200;
        public override int ProvisionCost { get; } = 5;
        public override int FightingPower { get; set; } = 2;
        public override int DefenseFromInfantry { get; set; } = 20;
        public override int DefenseFromCavalry { get; set; } = 50;
        public override int DefenseFromArchers { get; set; } = 20;
        public override int LoadCapacity { get; set; } = 0;
        public override TimeSpan BaseRecruitmentTime { get; set; } = new TimeSpan(0, 8, 0);
        public override TimeSpan TravelTimePerTile { get; set; } = new TimeSpan(0, 24, 0);

        public override int NumberOnDefense { get; set; } = 0;

        public Ram(CalculatorData parent) : base(parent)
        {

        }
    }
}
