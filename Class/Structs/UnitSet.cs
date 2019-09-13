﻿using System;
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


        public UnitSet(int defaultValue = 0)
        {
            Spearman = defaultValue;
            Swordsman = defaultValue;
            AxeFighter = defaultValue;
            Archer = defaultValue;
            LightCavalry = defaultValue;
            MountedArcher = defaultValue;
            HeavyCavalry = defaultValue;
            Ram = defaultValue;
            Catapult = defaultValue;
            Berserker = defaultValue;
            Trebuchet = defaultValue;
            Nobleman = defaultValue;
            Paladin = defaultValue;
        }

        public int GetTotalDefFromInfantry()
        {
            int totalDefense = 0;
            totalDefense += Spearman * GameData.GetUnit("Spearman").DefenseFromInfantry;
            totalDefense += Swordsman * GameData.GetUnit("Swordsman").DefenseFromInfantry;
            totalDefense += AxeFighter * GameData.GetUnit("AxeFighter").DefenseFromInfantry;
            totalDefense += Archer * GameData.GetUnit("Archer").DefenseFromInfantry;
            totalDefense += LightCavalry * GameData.GetUnit("LightCavalry").DefenseFromInfantry;
            totalDefense += MountedArcher * GameData.GetUnit("MountedArcher").DefenseFromInfantry;
            totalDefense += HeavyCavalry * GameData.GetUnit("HeavyCavalry").DefenseFromInfantry;
            totalDefense += Ram * GameData.GetUnit("Ram").DefenseFromInfantry;
            totalDefense += Catapult * GameData.GetUnit("Catapult").DefenseFromInfantry;
            totalDefense += Berserker * GameData.GetUnit("Berserker").DefenseFromInfantry;
            totalDefense += Trebuchet * GameData.GetUnit("Trebuchet").DefenseFromInfantry;
            totalDefense += Nobleman * GameData.GetUnit("Nobleman").DefenseFromInfantry;
            totalDefense += Paladin * GameData.GetUnit("Paladin").DefenseFromInfantry;

            return totalDefense;
        }

        public int GetTotalDefFromCavalry()
        {
            int totalDefense = 0;
            totalDefense += Spearman * GameData.GetUnit("Spearman").DefenseFromCavalry;
            totalDefense += Swordsman * GameData.GetUnit("Swordsman").DefenseFromCavalry;
            totalDefense += AxeFighter * GameData.GetUnit("AxeFighter").DefenseFromCavalry;
            totalDefense += Archer * GameData.GetUnit("Archer").DefenseFromCavalry;
            totalDefense += LightCavalry * GameData.GetUnit("LightCavalry").DefenseFromCavalry;
            totalDefense += MountedArcher * GameData.GetUnit("MountedArcher").DefenseFromCavalry;
            totalDefense += HeavyCavalry * GameData.GetUnit("HeavyCavalry").DefenseFromCavalry;
            totalDefense += Ram * GameData.GetUnit("Ram").DefenseFromCavalry;
            totalDefense += Catapult * GameData.GetUnit("Catapult").DefenseFromCavalry;
            totalDefense += Berserker * GameData.GetUnit("Berserker").DefenseFromCavalry;
            totalDefense += Trebuchet * GameData.GetUnit("Trebuchet").DefenseFromCavalry;
            totalDefense += Nobleman * GameData.GetUnit("Nobleman").DefenseFromCavalry;
            totalDefense += Paladin * GameData.GetUnit("Paladin").DefenseFromCavalry;

            return totalDefense;
        }

        public int GetTotalDefFromArchers()
        {
            int totalDefense = 0;
            totalDefense += Spearman * GameData.GetUnit("Spearman").DefenseFromArchers;
            totalDefense += Swordsman * GameData.GetUnit("Swordsman").DefenseFromArchers;
            totalDefense += AxeFighter * GameData.GetUnit("AxeFighter").DefenseFromArchers;
            totalDefense += Archer * GameData.GetUnit("Archer").DefenseFromArchers;
            totalDefense += LightCavalry * GameData.GetUnit("LightCavalry").DefenseFromArchers;
            totalDefense += MountedArcher * GameData.GetUnit("MountedArcher").DefenseFromArchers;
            totalDefense += HeavyCavalry * GameData.GetUnit("HeavyCavalry").DefenseFromArchers;
            totalDefense += Ram * GameData.GetUnit("Ram").DefenseFromArchers;
            totalDefense += Catapult * GameData.GetUnit("Catapult").DefenseFromArchers;
            totalDefense += Berserker * GameData.GetUnit("Berserker").DefenseFromArchers;
            totalDefense += Trebuchet * GameData.GetUnit("Trebuchet").DefenseFromArchers;
            totalDefense += Nobleman * GameData.GetUnit("Nobleman").DefenseFromArchers;
            totalDefense += Paladin * GameData.GetUnit("Paladin").DefenseFromArchers;

            return totalDefense;
        }

        public void ApplyKillRate(decimal killRate)
        {
            // Subtract the loses from the current number of units
            Spearman -= GameData.GetUnitsKilled(Spearman, killRate);
            Swordsman -= GameData.GetUnitsKilled(Swordsman, killRate);
            AxeFighter -= GameData.GetUnitsKilled(AxeFighter, killRate);
            Archer -= GameData.GetUnitsKilled(Archer, killRate);
            LightCavalry -= GameData.GetUnitsKilled(LightCavalry, killRate);
            MountedArcher -= GameData.GetUnitsKilled(MountedArcher, killRate);
            HeavyCavalry -= GameData.GetUnitsKilled(HeavyCavalry, killRate);
            Ram -= GameData.GetUnitsKilled(Ram, killRate);
            Catapult -= GameData.GetUnitsKilled(Catapult, killRate);
            Berserker -= GameData.GetUnitsKilled(Berserker, killRate);
            Trebuchet -= GameData.GetUnitsKilled(Trebuchet, killRate);
            Nobleman -= GameData.GetUnitsKilled(Nobleman, killRate);
            Paladin -= GameData.GetUnitsKilled(Paladin, killRate);
        }


        public static UnitSet operator +(UnitSet u1, UnitSet u2) => new UnitSet
        {
            Spearman = u1.Spearman + u2.Spearman,
            Swordsman = u1.Swordsman + u2.Swordsman,
            AxeFighter = u1.AxeFighter + u2.AxeFighter,
            Archer = u1.Archer + u2.Archer,
            LightCavalry = u1.LightCavalry + u2.LightCavalry,
            MountedArcher = u1.MountedArcher + u2.MountedArcher,
            HeavyCavalry = u1.HeavyCavalry + u2.HeavyCavalry,
            Ram = u1.Ram + u2.Ram,
            Catapult = u1.Catapult + u2.Catapult,
            Berserker = u1.Berserker + u2.Berserker,
            Trebuchet = u1.Trebuchet + u2.Trebuchet,
            Nobleman = u1.Nobleman + u2.Nobleman,
            Paladin = u1.Paladin + u2.Paladin,
        };

        public static UnitSet operator -(UnitSet u1, UnitSet u2) => new UnitSet
        {
            Spearman = u1.Spearman - u2.Spearman,
            Swordsman = u1.Swordsman - u2.Swordsman,
            AxeFighter = u1.AxeFighter - u2.AxeFighter,
            Archer = u1.Archer - u2.Archer,
            LightCavalry = u1.LightCavalry - u2.LightCavalry,
            MountedArcher = u1.MountedArcher - u2.MountedArcher,
            HeavyCavalry = u1.HeavyCavalry - u2.HeavyCavalry,
            Ram = u1.Ram - u2.Ram,
            Catapult = u1.Catapult - u2.Catapult,
            Berserker = u1.Berserker - u2.Berserker,
            Trebuchet = u1.Trebuchet - u2.Trebuchet,
            Nobleman = u1.Nobleman - u2.Nobleman,
            Paladin = u1.Paladin - u2.Paladin,
        };
    }
}
