using System;
using System.Collections.Generic;
using System.Text;

namespace TribalWars2_CalculationTools.Class.Units
{
    public abstract class BaseUnit
    {
        public abstract string Name { get; } 
        public abstract int WoodCost { get; } 
        public abstract int ClayCost { get; }
        public abstract int IronCost { get; }
        public abstract int ProvisionCost { get; }

        public abstract int NumberOnAttack { get; set; }
        public abstract int NumberOnDefense { get; set; }

        public string ImagePath => $"/Resources/Img/unit_{this.Name.ToLower()}.png";
    }
}
