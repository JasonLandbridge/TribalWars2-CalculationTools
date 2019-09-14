﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TribalWars2_CalculationTools.Class.Structs;
using TribalWars2_CalculationTools.Class.Units;
using TribalWars2_CalculationTools.ViewModels;

namespace TribalWars2_CalculationTools.Class
{
    public class BattleResult
    {
        #region Attacking

        public UnitSet AtkUnits;
        public UnitSet AtkUnitsLost;
        public UnitSet AtkUnitsLeft => AtkUnits - AtkUnitsLost;

        #endregion

        #region Defending

        public UnitSet DefUnits;
        public UnitSet DefUnitsLost;
        public UnitSet DefUnitsLeft => DefUnits - DefUnitsLost;

        #endregion


        #region Constructors

        public BattleResult()
        {
        }

        public BattleResult(InputCalculatorData input)
        {
            AtkUnits.Spearman = input.Spearman.NumberOnAttack;
            DefUnits.Spearman = input.Spearman.NumberOnDefense;

            AtkUnits.Swordsman = input.Swordsman.NumberOnAttack;
            DefUnits.Swordsman = input.Swordsman.NumberOnDefense;

            AtkUnits.AxeFighter = input.AxeFighter.NumberOnAttack;
            DefUnits.AxeFighter = input.AxeFighter.NumberOnDefense;

            AtkUnits.Archer = input.Archer.NumberOnAttack;
            DefUnits.Archer = input.Archer.NumberOnDefense;

            AtkUnits.LightCavalry = input.LightCavalry.NumberOnAttack;
            DefUnits.LightCavalry = input.LightCavalry.NumberOnDefense;

            AtkUnits.MountedArcher = input.MountedArcher.NumberOnAttack;
            DefUnits.MountedArcher = input.MountedArcher.NumberOnDefense;

            AtkUnits.HeavyCavalry = input.HeavyCavalry.NumberOnAttack;
            DefUnits.HeavyCavalry = input.HeavyCavalry.NumberOnDefense;

            AtkUnits.Ram = input.Ram.NumberOnAttack;
            DefUnits.Ram = input.Ram.NumberOnDefense;

            AtkUnits.Catapult = input.Catapult.NumberOnAttack;
            DefUnits.Catapult = input.Catapult.NumberOnDefense;

            AtkUnits.Berserker = input.Berserker.NumberOnAttack;
            DefUnits.Berserker = input.Berserker.NumberOnDefense;

            AtkUnits.Trebuchet = input.Trebuchet.NumberOnAttack;
            DefUnits.Trebuchet = input.Trebuchet.NumberOnDefense;

            AtkUnits.Nobleman = input.Nobleman.NumberOnAttack;
            DefUnits.Nobleman = input.Nobleman.NumberOnDefense;

            AtkUnits.Paladin = input.Paladin.NumberOnAttack;
            DefUnits.Paladin = input.Paladin.NumberOnDefense;

        }

        #endregion Constructors

        #region Properties

        public int AtkBattleModifier { get; set; }
        public int DefBattleModifier { get; set; }

        #endregion Properties

        #region ResultList

        public List<BattleResultValue> ListOfAtkNumbers => AtkUnits.ToBattleResultList();
        public List<BattleResultValue> ListOfAtkLostNumbers => AtkUnitsLost.ToBattleResultList();
        public List<BattleResultValue> ListOfAtkLeftNumbers => AtkUnitsLeft.ToBattleResultList();


        public List<BattleResultValue> ListOfDefNumbers => DefUnits.ToBattleResultList();
        public List<BattleResultValue> ListOfDefLostNumbers => DefUnitsLost.ToBattleResultList();
        public List<BattleResultValue> ListOfDefLeftNumbers => DefUnitsLeft.ToBattleResultList();

        #endregion

        #region Methods

        public BattleResult Copy()
        {
            return (BattleResult)this.MemberwiseClone();
        }


        public int ToLossUnit(int currentNumber, decimal ratio, decimal lostCoefficient)
        {
            decimal x = (currentNumber * ratio) * lostCoefficient;
            return (int)Math.Round(x + 0.000001m, MidpointRounding.AwayFromZero);
        }

        #endregion Methods

    }
}
