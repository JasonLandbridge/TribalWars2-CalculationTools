using System;
using System.Collections.Generic;
using System.Text;

namespace TribalWars2_CalculationTools.Class.Structs
{
    public struct UnitSet
    {
        public int Spearman { get; set; }
        public int Swordsman { get; set; }
        public int AxeFighter { get; set; }
        public int Archer { get; set; }
        public int LightCavalry { get; set; }
        public int MountedArcher { get; set; }
        public int HeavyCavalry { get; set; }
        public int Ram { get; set; }
        public int Catapult { get; set; }
        public int Berserker { get; set; }
        public int Trebuchet { get; set; }
        public int Nobleman { get; set; }
        public int Paladin { get; set; }

        public UnitSet(int defaultValue)
        {
            Spearman = 0;
            Swordsman = 0;
            AxeFighter = 0;
            Archer = 0;
            LightCavalry = 0;
            MountedArcher = 0;
            HeavyCavalry = 0;
            Ram = 0;
            Catapult = 0;
            Berserker = 0;
            Trebuchet = 0;
            Nobleman = 0;
            Paladin = 0;
        }
    }
}
