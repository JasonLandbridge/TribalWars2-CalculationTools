using System;
using System.Collections.Generic;
using System.Text;

namespace TribalWars2_CalculationTools.Class.Units
{
    public class Spearman : BaseUnit
    {
        public override string Name { get; } = "Spearman";
        public override int WoodCost { get; } = 50;
        public override int ClayCost { get; } = 30;
        public override int IronCost { get; } = 20;
        public override int ProvisionCost { get; } = 1;
        public override int NumberOnAttack { get; set; }
        public override int NumberOnDefense { get; set; }

        public Spearman()
        {

        }
    }
}
