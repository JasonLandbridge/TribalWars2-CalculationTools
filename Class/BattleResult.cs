using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TribalWars2_CalculationTools.Class.Units;

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

        #region ResultList

        public BindingList<ValueType> ListOfAtkNumbers =>
            new BindingList<ValueType>
            {
                new ValueType(AtkSpearman),
                new ValueType(AtkSwordsman),
                new ValueType(AtkAxeFighter),
                new ValueType(AtkArcher),
                new ValueType(AtkLightCavalry),
                new ValueType(AtkMountedArcher),
                new ValueType(AtkHeavyCavalry),
                new ValueType(AtkRam),
                new ValueType(AtkCatapult),
                new ValueType(AtkBerserker),
                new ValueType(AtkTrebuchet),
                new ValueType(AtkNobleman),
                new ValueType(AtkPaladin),
            };
        public BindingList<ValueType> ListOfAtkLostNumbers =>
            new BindingList<ValueType>
            {
                new ValueType(LostOnAtkSpearman),
                new ValueType(LostOnAtkSwordsman),
                new ValueType(LostOnAtkAxeFighter),
                new ValueType(LostOnAtkArcher),
                new ValueType(LostOnAtkLightCavalry),
                new ValueType(LostOnAtkMountedArcher),
                new ValueType(LostOnAtkHeavyCavalry),
                new ValueType(LostOnAtkRam),
                new ValueType(LostOnAtkCatapult),
                new ValueType(LostOnAtkBerserker),
                new ValueType(LostOnAtkTrebuchet),
                new ValueType(LostOnAtkNobleman),
                new ValueType(LostOnAtkPaladin),
            };

        public BindingList<ValueType> ListOfDefNumbers =>
            new BindingList<ValueType>
            {
                new ValueType(DefSpearman),
                new ValueType(DefSwordsman),
                new ValueType(DefAxeFighter),
                new ValueType(DefArcher),
                new ValueType(DefLightCavalry),
                new ValueType(DefMountedArcher),
                new ValueType(DefHeavyCavalry),
                new ValueType(DefRam),
                new ValueType(DefCatapult),
                new ValueType(DefBerserker),
                new ValueType(DefTrebuchet),
                new ValueType(DefNobleman),
                new ValueType(DefPaladin),
            };

        public BindingList<ValueType> ListOfDefLostNumbers =>
            new BindingList<ValueType>
            {
                new ValueType(LostOnDefSpearman),
                new ValueType(LostOnDefSwordsman),
                new ValueType(LostOnDefAxeFighter),
                new ValueType(LostOnDefArcher),
                new ValueType(LostOnDefLightCavalry),
                new ValueType(LostOnDefMountedArcher),
                new ValueType(LostOnDefHeavyCavalry),
                new ValueType(LostOnDefRam),
                new ValueType(LostOnDefCatapult),
                new ValueType(LostOnDefBerserker),
                new ValueType(LostOnDefTrebuchet),
                new ValueType(LostOnDefNobleman),
                new ValueType(LostOnDefPaladin),
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

        public void KillAllAtkInfantry()
        {
            LostOnAtkSpearman = AtkSpearman;
            LostOnAtkSwordsman = AtkSwordsman;
            LostOnAtkAxeFighter = AtkAxeFighter;
            LostOnAtkArcher = AtkArcher;
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

            totalAttack += AtkSpearman * GameData.Spearman.FightingPower;
            totalAttack += AtkSwordsman * GameData.Swordsman.FightingPower;
            totalAttack += AtkAxeFighter * GameData.AxeFighter.FightingPower;
            totalAttack += AtkNobleman * GameData.Nobleman.FightingPower;

            // Take into account that berserkers
            // are twice as strong when fighting a superior army
            if (defenseIsSuperior)
            {
                totalAttack += AtkBerserker * (GameData.Berserker.FightingPower * 2);
            }
            else
            {
                totalAttack += AtkBerserker * GameData.Berserker.FightingPower;
            }

            return totalAttack;
        }

        public int GetTotalCavalryAttack()
        {
            int totalAttack = 0;

            totalAttack += AtkLightCavalry * GameData.LightCavalry.FightingPower;
            totalAttack += AtkHeavyCavalry * GameData.HeavyCavalry.FightingPower;

            return totalAttack;
        }

        public int GetTotalArcherAttack()
        {
            int totalAttack = 0;

            totalAttack += AtkArcher * GameData.Archer.FightingPower;
            totalAttack += AtkMountedArcher * GameData.MountedArcher.FightingPower;

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
