using System;
using System.Collections.Generic;
using System.Text;
using TribalWars2_CalculationTools.Class.Units;

namespace TribalWars2_CalculationTools.Class
{
    public static class GameData
    {
        public static Spearman Spearman { get; } = new Spearman();
        public static Swordsman Swordsman { get; } = new Swordsman();
        public static AxeFighter AxeFighter { get; } = new AxeFighter();
        public static Archer Archer { get; } = new Archer();
        public static LightCavalry LightCavalry { get; } = new LightCavalry();
        public static MountedArcher MountedArcher { get; } = new MountedArcher();
        public static HeavyCavalry HeavyCavalry { get; } = new HeavyCavalry();
        public static Ram Ram { get; } = new Ram();
        public static Catapult Catapult { get; } = new Catapult();
        public static Berserker Berserker { get; } = new Berserker();
        public static Trebuchet Trebuchet { get; } = new Trebuchet();
        public static Nobleman Nobleman { get; } = new Nobleman();
        public static Paladin Paladin { get; } = new Paladin();

        public static List<BaseUnit> UnitList => new List<BaseUnit>
        {
            Spearman,
            Swordsman,
            AxeFighter,
            Archer,
            LightCavalry,
            MountedArcher,
            HeavyCavalry,
            Ram,
            Catapult,
            Berserker,
            Trebuchet,
            Nobleman,
            Paladin,
        };
    }
}
