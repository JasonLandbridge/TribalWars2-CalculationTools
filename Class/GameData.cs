using System;
using System.Collections.Generic;
using System.Text;
using TribalWars2_CalculationTools.Class.Units;
using TribalWars2_CalculationTools.Models;

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

        public static List<FaithLevel> FaithOptions { get; } = new List<FaithLevel>
        {
            new FaithLevel
            {
                Code ="Faith_0",
                Name = "None",
                Modifier = 0.5m
            },
            new FaithLevel
            {
                Code ="Faith_1",
                Name = "Chapel",
                Modifier = 1m
            },
            new FaithLevel
            {
                Code ="Faith_2",
                Name = "Church Level 1",
                Modifier = 1m
            },
            new FaithLevel
            {
                Code ="Faith_3",
                Name = "Church Level 2",
                Modifier = 1.05m,

            },
            new FaithLevel
            {
                Code ="Faith_4",
                Name = "Church Level 3",
                Modifier = 1.1m
            }
        };
    }
}
