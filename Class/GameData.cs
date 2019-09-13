using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TribalWars2_CalculationTools.Class;
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

        public static ObservableCollection<UnitRow> UnitImageList
        {
            get
            {
                ObservableCollection<UnitRow> unitImageList = new ObservableCollection<UnitRow>();

                foreach (BaseUnit baseUnit in UnitList)
                {
                    unitImageList.Add(new UnitRow
                    {
                        ImagePath = baseUnit.ImagePath,
                        Name = baseUnit.Name,
                    });
                }

                return unitImageList;
            }
        }

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

        public static int NumberOfUnits => UnitList.Count;

        static GameData()
        {

        }

        public static BaseUnit GetUnit(string code)
        {
            return UnitList.First(unit => unit.Code == code);
        }


        #region Formulas
        /// <summary>
        /// Applies the killRate to the number of units and returns the number of killed units.
        /// </summary>
        /// <param name="numberOfUnits"></param>
        /// <param name="killRate"></param>
        /// <returns></returns>
        public static int GetUnitsKilled(int numberOfUnits, decimal killRate)
        {
            decimal x = numberOfUnits * killRate;
            return (int)Math.Round(x + 0.000001m, MidpointRounding.AwayFromZero);
        }

        public static decimal GetDefKillRate(int atkStrength, int defStrength)
        {
            if (atkStrength == 0 || defStrength == 0)
            {
                return 0;
            }

            decimal atkDefRatio = Math.Round(defStrength / (decimal)atkStrength, 9, MidpointRounding.AwayFromZero);
            decimal atkKillRate = defStrength <= atkStrength ? 1 : (decimal)Math.Sqrt(1 / (double)atkDefRatio) / atkDefRatio;

            return Math.Round(atkKillRate, 6, MidpointRounding.AwayFromZero);
        }

        public static decimal GetAtkKillRate(int atkStrength, int defStrength)
        {
            if (atkStrength == 0 || defStrength == 0)
            {
                return 0;
            }

            decimal atkDefRatio = Math.Round(atkStrength / (decimal)defStrength, 9, MidpointRounding.AwayFromZero);
            decimal defKillRate = atkStrength <= defStrength ? 1 : (decimal)Math.Sqrt(1 / (double)atkDefRatio) / atkDefRatio;

            return Math.Round(defKillRate, 6, MidpointRounding.AwayFromZero);
        }
        #endregion
    }
}
