﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TribalWars2_CalculationTools.Class.Units;
using TribalWars2_CalculationTools.ViewModels;

namespace TribalWars2_CalculationTools.Class
{
    public class BattleResult
    {
        #region AttackingInput

        public int AtkSpearman { get; set; }
        public int AtkSwordsman { get; set; }
        public int AtkAxeFighter { get; set; }
        public int AtkArcher { get; set; }
        public int AtkLightCavalry { get; set; }
        public int AtkMountedArcher { get; set; }
        public int AtkHeavyCavalry { get; set; }
        public int AtkRam { get; set; }
        public int AtkCatapult { get; set; }
        public int AtkBerserker { get; set; }
        public int AtkTrebuchet { get; set; }
        public int AtkNobleman { get; set; }
        public int AtkPaladin { get; set; }

        #endregion

        #region LostOnAttackingInput

        public int LostOnAtkSpearman { get; set; }
        public int LostOnAtkSwordsman { get; set; }
        public int LostOnAtkAxeFighter { get; set; }
        public int LostOnAtkArcher { get; set; }
        public int LostOnAtkLightCavalry { get; set; }
        public int LostOnAtkMountedArcher { get; set; }
        public int LostOnAtkHeavyCavalry { get; set; }
        public int LostOnAtkRam { get; set; }
        public int LostOnAtkCatapult { get; set; }
        public int LostOnAtkBerserker { get; set; }
        public int LostOnAtkTrebuchet { get; set; }
        public int LostOnAtkNobleman { get; set; }
        public int LostOnAtkPaladin { get; set; }

        #endregion

        #region LeftOnAttackingInput

        public int LeftOnAtkSpearman => AtkSpearman - LostOnAtkSpearman;
        public int LeftOnAtkSwordsman => AtkSwordsman - LostOnAtkSwordsman;
        public int LeftOnAtkAxeFighter => AtkAxeFighter - LostOnAtkAxeFighter;
        public int LeftOnAtkArcher => AtkArcher - LostOnAtkArcher;
        public int LeftOnAtkLightCavalry => AtkLightCavalry - LostOnAtkLightCavalry;
        public int LeftOnAtkMountedArcher => AtkMountedArcher - LostOnAtkMountedArcher;
        public int LeftOnAtkHeavyCavalry => AtkHeavyCavalry - LostOnAtkHeavyCavalry;
        public int LeftOnAtkRam => AtkRam - LostOnAtkRam;
        public int LeftOnAtkCatapult => AtkCatapult - LostOnAtkCatapult;
        public int LeftOnAtkBerserker => AtkBerserker - LostOnAtkBerserker;
        public int LeftOnAtkTrebuchet => AtkTrebuchet - LostOnAtkTrebuchet;
        public int LeftOnAtkNobleman => AtkNobleman - LostOnAtkNobleman;
        public int LeftOnAtkPaladin => AtkPaladin - LostOnAtkPaladin;

        #endregion

        #region DefendingInput

        public int DefSpearman { get; set; }
        public int DefSwordsman { get; set; }
        public int DefAxeFighter { get; set; }
        public int DefArcher { get; set; }
        public int DefLightCavalry { get; set; }
        public int DefMountedArcher { get; set; }
        public int DefHeavyCavalry { get; set; }
        public int DefRam { get; set; }
        public int DefCatapult { get; set; }
        public int DefBerserker { get; set; }
        public int DefTrebuchet { get; set; }
        public int DefNobleman { get; set; }
        public int DefPaladin { get; set; }
        #endregion

        #region LostOnDefendingInput
        public int LostOnDefSpearman { get; set; }
        public int LostOnDefSwordsman { get; set; }
        public int LostOnDefAxeFighter { get; set; }
        public int LostOnDefArcher { get; set; }
        public int LostOnDefLightCavalry { get; set; }
        public int LostOnDefMountedArcher { get; set; }
        public int LostOnDefHeavyCavalry { get; set; }
        public int LostOnDefRam { get; set; }
        public int LostOnDefCatapult { get; set; }
        public int LostOnDefBerserker { get; set; }
        public int LostOnDefTrebuchet { get; set; }
        public int LostOnDefNobleman { get; set; }
        public int LostOnDefPaladin { get; set; }
        #endregion


        #region LeftOnDefendingInput

        public int LeftOnDefSpearman => DefSpearman - LostOnDefSpearman;
        public int LeftOnDefSwordsman => DefSwordsman - LostOnDefSwordsman;
        public int LeftOnDefAxeFighter => DefAxeFighter - LostOnDefAxeFighter;
        public int LeftOnDefArcher => DefArcher - LostOnDefArcher;
        public int LeftOnDefLightCavalry => DefLightCavalry - LostOnDefLightCavalry;
        public int LeftOnDefMountedArcher => DefMountedArcher - LostOnDefMountedArcher;
        public int LeftOnDefHeavyCavalry => DefHeavyCavalry - LostOnDefHeavyCavalry;
        public int LeftOnDefRam => DefRam - LostOnDefRam;
        public int LeftOnDefCatapult => DefCatapult - LostOnDefCatapult;
        public int LeftOnDefBerserker => DefBerserker - LostOnDefBerserker;
        public int LeftOnDefTrebuchet => DefTrebuchet - LostOnDefTrebuchet;
        public int LeftOnDefNobleman => DefNobleman - LostOnDefNobleman;
        public int LeftOnDefPaladin => DefPaladin - LostOnDefPaladin;

        #endregion

        public int AtkBattleModifier { get; set; }
        public int DefBattleModifier { get; set; }

        #region ResultList

        public List<BattleResultValue> ListOfAtkNumbers =>
            new List<BattleResultValue>
            {
                new BattleResultValue(AtkSpearman),
                new BattleResultValue(AtkSwordsman),
                new BattleResultValue(AtkAxeFighter),
                new BattleResultValue(AtkArcher),
                new BattleResultValue(AtkLightCavalry),
                new BattleResultValue(AtkMountedArcher),
                new BattleResultValue(AtkHeavyCavalry),
                new BattleResultValue(AtkRam),
                new BattleResultValue(AtkCatapult),
                new BattleResultValue(AtkBerserker),
                new BattleResultValue(AtkTrebuchet),
                new BattleResultValue(AtkNobleman),
                new BattleResultValue(AtkPaladin),
            };
        public List<BattleResultValue> ListOfAtkLostNumbers =>
            new List<BattleResultValue>
            {
                new BattleResultValue(LostOnAtkSpearman),
                new BattleResultValue(LostOnAtkSwordsman),
                new BattleResultValue(LostOnAtkAxeFighter),
                new BattleResultValue(LostOnAtkArcher),
                new BattleResultValue(LostOnAtkLightCavalry),
                new BattleResultValue(LostOnAtkMountedArcher),
                new BattleResultValue(LostOnAtkHeavyCavalry),
                new BattleResultValue(LostOnAtkRam),
                new BattleResultValue(LostOnAtkCatapult),
                new BattleResultValue(LostOnAtkBerserker),
                new BattleResultValue(LostOnAtkTrebuchet),
                new BattleResultValue(LostOnAtkNobleman),
                new BattleResultValue(LostOnAtkPaladin),
            };

        public List<BattleResultValue> ListOfDefNumbers =>
            new List<BattleResultValue>
            {
                new BattleResultValue(DefSpearman),
                new BattleResultValue(DefSwordsman),
                new BattleResultValue(DefAxeFighter),
                new BattleResultValue(DefArcher),
                new BattleResultValue(DefLightCavalry),
                new BattleResultValue(DefMountedArcher),
                new BattleResultValue(DefHeavyCavalry),
                new BattleResultValue(DefRam),
                new BattleResultValue(DefCatapult),
                new BattleResultValue(DefBerserker),
                new BattleResultValue(DefTrebuchet),
                new BattleResultValue(DefNobleman),
                new BattleResultValue(DefPaladin),
            };

        public List<BattleResultValue> ListOfDefLostNumbers =>
            new List<BattleResultValue>
            {
                new BattleResultValue(LostOnDefSpearman),
                new BattleResultValue(LostOnDefSwordsman),
                new BattleResultValue(LostOnDefAxeFighter),
                new BattleResultValue(LostOnDefArcher),
                new BattleResultValue(LostOnDefLightCavalry),
                new BattleResultValue(LostOnDefMountedArcher),
                new BattleResultValue(LostOnDefHeavyCavalry),
                new BattleResultValue(LostOnDefRam),
                new BattleResultValue(LostOnDefCatapult),
                new BattleResultValue(LostOnDefBerserker),
                new BattleResultValue(LostOnDefTrebuchet),
                new BattleResultValue(LostOnDefNobleman),
                new BattleResultValue(LostOnDefPaladin),
            };




        #endregion



        public BattleResult()
        {
        }
        public BattleResult(InputCalculatorData input)
        {
            this.AtkSpearman = input.Spearman.NumberOnAttack;
            this.DefSpearman = input.Spearman.NumberOnDefense;

            this.AtkSwordsman = input.Swordsman.NumberOnAttack;
            this.DefSwordsman = input.Swordsman.NumberOnDefense;

            this.AtkAxeFighter = input.AxeFighter.NumberOnAttack;
            this.DefAxeFighter = input.AxeFighter.NumberOnDefense;

            this.AtkArcher = input.Archer.NumberOnAttack;
            this.DefArcher = input.Archer.NumberOnDefense;

            this.AtkLightCavalry = input.LightCavalry.NumberOnAttack;
            this.DefLightCavalry = input.LightCavalry.NumberOnDefense;

            this.AtkMountedArcher = input.MountedArcher.NumberOnAttack;
            this.DefMountedArcher = input.MountedArcher.NumberOnDefense;

            this.AtkHeavyCavalry = input.HeavyCavalry.NumberOnAttack;
            this.DefHeavyCavalry = input.HeavyCavalry.NumberOnDefense;

            this.AtkRam = input.Ram.NumberOnAttack;
            this.DefRam = input.Ram.NumberOnDefense;

            this.AtkCatapult = input.Catapult.NumberOnAttack;
            this.DefCatapult = input.Catapult.NumberOnDefense;

            this.AtkBerserker = input.Berserker.NumberOnAttack;
            this.DefBerserker = input.Berserker.NumberOnDefense;

            this.AtkTrebuchet = input.Trebuchet.NumberOnAttack;
            this.DefTrebuchet = input.Trebuchet.NumberOnDefense;

            this.AtkNobleman = input.Nobleman.NumberOnAttack;
            this.DefNobleman = input.Nobleman.NumberOnDefense;

            this.AtkPaladin = input.Paladin.NumberOnAttack;
            this.DefPaladin = input.Paladin.NumberOnDefense;
        }

        public int ToLossUnit(decimal lostCoefficient, int currentNumber)
        {
            return (int)Math.Round(currentNumber * lostCoefficient, MidpointRounding.ToZero);
        }

        public BattleResult Copy()
        {
            return (BattleResult)this.MemberwiseClone();
        }

        public void KillAllAtkInfantry()
        {
            LostOnAtkSpearman = AtkSpearman;
            LostOnAtkSwordsman = AtkSwordsman;
            LostOnAtkAxeFighter = AtkAxeFighter;
            LostOnAtkBerserker = AtkBerserker;

        }

        public void KillAllAtkCavalry()
        {
            LostOnAtkLightCavalry = AtkLightCavalry;
            LostOnAtkHeavyCavalry = AtkHeavyCavalry;
        }

        public void KillAllAtkArchers()
        {
            LostOnAtkArcher = AtkArcher;
            LostOnAtkMountedArcher = AtkMountedArcher;
        }

        public void KillAtkInfantry(decimal lostCoefficient)
        {
            LostOnAtkSpearman = ToLossUnit(lostCoefficient, AtkSpearman);
            LostOnAtkSwordsman = ToLossUnit(lostCoefficient, AtkSwordsman);
            LostOnAtkAxeFighter = ToLossUnit(lostCoefficient, AtkAxeFighter);
            LostOnAtkArcher = ToLossUnit(lostCoefficient, AtkArcher);
        }

        public void KillAllDefInfantry()
        {
            LostOnDefSpearman = DefSpearman;
            LostOnDefSwordsman = DefSwordsman;
            LostOnDefAxeFighter = DefAxeFighter;
            LostOnDefArcher = DefArcher;
        }

        public void KillDefInfantry(decimal lostCoefficient)
        {
            LostOnDefSpearman = ToLossUnit(lostCoefficient, DefSpearman);
            LostOnDefSwordsman = ToLossUnit(lostCoefficient, DefSwordsman);
            LostOnDefAxeFighter = ToLossUnit(lostCoefficient, DefAxeFighter);
            LostOnDefArcher = ToLossUnit(lostCoefficient, DefArcher);
        }


        #region AtkProvisions

        public int GetTotalInfantryProvisions()
        {
            int totalProvisions = 0;
            // Count all units that are NOT Cavalry and NOT archers
            totalProvisions += AtkSpearman * GameData.Spearman.ProvisionCost;
            totalProvisions += AtkSwordsman * GameData.Swordsman.ProvisionCost;
            totalProvisions += AtkAxeFighter * GameData.AxeFighter.ProvisionCost;

            totalProvisions += AtkRam * GameData.Ram.ProvisionCost;
            totalProvisions += AtkCatapult * GameData.Catapult.ProvisionCost;
            totalProvisions += AtkTrebuchet * GameData.Trebuchet.ProvisionCost;

            totalProvisions += AtkBerserker * GameData.Berserker.ProvisionCost;

            totalProvisions += AtkNobleman * GameData.Nobleman.ProvisionCost;
            totalProvisions += AtkPaladin * GameData.Paladin.ProvisionCost;


            return totalProvisions;
        }

        public int GetTotalCavalryProvisions()
        {
            int totalProvisions = 0;

            totalProvisions += AtkLightCavalry * GameData.LightCavalry.ProvisionCost;
            totalProvisions += AtkHeavyCavalry * GameData.HeavyCavalry.ProvisionCost;

            return totalProvisions;
        }

        public int GetTotalArcherProvisions()
        {
            int totalProvisions = 0;

            totalProvisions += AtkArcher * GameData.Archer.ProvisionCost;
            totalProvisions += AtkMountedArcher * GameData.MountedArcher.ProvisionCost;

            return totalProvisions;
        }

        #endregion


        public int GetTotalInfantryAttack(bool defenseIsSuperior)
        {
            int totalAttack = 0;
            // Use the LeftOn to reuse this method every battle round with leftover units
            totalAttack += LeftOnAtkSpearman * GameData.Spearman.FightingPower;
            totalAttack += LeftOnAtkSwordsman * GameData.Swordsman.FightingPower;
            totalAttack += LeftOnAtkAxeFighter * GameData.AxeFighter.FightingPower;
            totalAttack += LeftOnAtkNobleman * GameData.Nobleman.FightingPower;

            // Take into account that berserkers
            // are twice as strong when fighting a superior army
            if (defenseIsSuperior)
            {
                totalAttack += LeftOnAtkBerserker * (GameData.Berserker.FightingPower * 2);
            }
            else
            {
                totalAttack += LeftOnAtkBerserker * GameData.Berserker.FightingPower;
            }

            return totalAttack;
        }

        public int GetTotalCavalryAttack()
        {
            int totalAttack = 0;
            // Use the Left on to reuse this method every battle round with leftover units
            totalAttack += LeftOnAtkLightCavalry * GameData.LightCavalry.FightingPower;
            totalAttack += LeftOnAtkHeavyCavalry * GameData.HeavyCavalry.FightingPower;

            return totalAttack;
        }

        public int GetTotalArcherAttack()
        {
            int totalAttack = 0;
            // Use the Left on to reuse this method every battle round with leftover units
            totalAttack += LeftOnAtkArcher * GameData.Archer.FightingPower;
            totalAttack += LeftOnAtkMountedArcher * GameData.MountedArcher.FightingPower;

            return totalAttack;
        }

        public int GetTotalDefProvisions()
        {
            int provisions = 0;

            for (int i = 0; i < GameData.UnitList.Count; i++)
            {
                provisions += ListOfDefNumbers[i].Value * GameData.UnitList[i].ProvisionCost;
            }

            return provisions;
        }

        public int GetTotalDefFromInfantry()
        {
            int defenseFromInfantry = 0;

            for (int i = 0; i < GameData.UnitList.Count; i++)
            {
                defenseFromInfantry += ListOfDefNumbers[i].Value * GameData.UnitList[i].DefenseFromInfantry;
            }

            return defenseFromInfantry;
        }

        public int GetTotalDefFromCavalry()
        {
            int defenseFromCavalry = 0;

            for (int i = 0; i < GameData.UnitList.Count; i++)
            {
                defenseFromCavalry += ListOfDefNumbers[i].Value * GameData.UnitList[i].DefenseFromCavalry;
            }

            return defenseFromCavalry;
        }

        public int GetTotalDefFromArchers()
        {
            int defenseFromArchers = 0;

            for (int i = 0; i < GameData.UnitList.Count; i++)
            {
                defenseFromArchers += ListOfDefNumbers[i].Value * GameData.UnitList[i].DefenseFromArchers;
            }

            return defenseFromArchers;
        }

    }
}
