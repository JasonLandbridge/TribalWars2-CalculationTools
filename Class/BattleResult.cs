using System;
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
        #region AttackingInput

        public UnitSet AtkUnits;

        public UnitSet AtkUnitsLost { get; set; }

        public int AtkArcher { get; set; }
        public int AtkAxeFighter { get; set; }
        public int AtkBerserker { get; set; }
        public int AtkCatapult { get; set; }
        public int AtkHeavyCavalry { get; set; }
        public int AtkLightCavalry { get; set; }
        public int AtkMountedArcher { get; set; }
        public int AtkNobleman { get; set; }
        public int AtkPaladin { get; set; }
        public int AtkRam { get; set; }
        public int AtkSpearman { get; set; }
        public int AtkSwordsman { get; set; }
        public int AtkTrebuchet { get; set; }
        #endregion

        #region LostOnAttackingInput

        public int LostOnAtkArcher { get; set; }
        public int LostOnAtkAxeFighter { get; set; }
        public int LostOnAtkBerserker { get; set; }
        public int LostOnAtkCatapult { get; set; }
        public int LostOnAtkHeavyCavalry { get; set; }
        public int LostOnAtkLightCavalry { get; set; }
        public int LostOnAtkMountedArcher { get; set; }
        public int LostOnAtkNobleman { get; set; }
        public int LostOnAtkPaladin { get; set; }
        public int LostOnAtkRam { get; set; }
        public int LostOnAtkSpearman { get; set; }
        public int LostOnAtkSwordsman { get; set; }
        public int LostOnAtkTrebuchet { get; set; }
        #endregion

        #region LeftOnAttackingInput

        public int LeftOnAtkArcher => AtkArcher - LostOnAtkArcher;
        public int LeftOnAtkAxeFighter => AtkAxeFighter - LostOnAtkAxeFighter;
        public int LeftOnAtkBerserker => AtkBerserker - LostOnAtkBerserker;
        public int LeftOnAtkCatapult => AtkCatapult - LostOnAtkCatapult;
        public int LeftOnAtkHeavyCavalry => AtkHeavyCavalry - LostOnAtkHeavyCavalry;
        public int LeftOnAtkLightCavalry => AtkLightCavalry - LostOnAtkLightCavalry;
        public int LeftOnAtkMountedArcher => AtkMountedArcher - LostOnAtkMountedArcher;
        public int LeftOnAtkNobleman => AtkNobleman - LostOnAtkNobleman;
        public int LeftOnAtkPaladin => AtkPaladin - LostOnAtkPaladin;
        public int LeftOnAtkRam => AtkRam - LostOnAtkRam;
        public int LeftOnAtkSpearman => AtkSpearman - LostOnAtkSpearman;
        public int LeftOnAtkSwordsman => AtkSwordsman - LostOnAtkSwordsman;
        public int LeftOnAtkTrebuchet => AtkTrebuchet - LostOnAtkTrebuchet;
        #endregion

        #region DefendingInput

        public int DefArcher { get; set; }
        public int DefAxeFighter { get; set; }
        public int DefBerserker { get; set; }
        public int DefCatapult { get; set; }
        public int DefHeavyCavalry { get; set; }
        public int DefLightCavalry { get; set; }
        public int DefMountedArcher { get; set; }
        public int DefNobleman { get; set; }
        public int DefPaladin { get; set; }
        public int DefRam { get; set; }
        public int DefSpearman { get; set; }
        public int DefSwordsman { get; set; }
        public int DefTrebuchet { get; set; }
        #endregion

        #region LostOnDefendingInput
        public int LostOnDefArcher { get; set; }
        public int LostOnDefAxeFighter { get; set; }
        public int LostOnDefBerserker { get; set; }
        public int LostOnDefCatapult { get; set; }
        public int LostOnDefHeavyCavalry { get; set; }
        public int LostOnDefLightCavalry { get; set; }
        public int LostOnDefMountedArcher { get; set; }
        public int LostOnDefNobleman { get; set; }
        public int LostOnDefPaladin { get; set; }
        public int LostOnDefRam { get; set; }
        public int LostOnDefSpearman { get; set; }
        public int LostOnDefSwordsman { get; set; }
        public int LostOnDefTrebuchet { get; set; }
        #endregion


        #region LeftOnDefendingInput

        public int LeftOnDefArcher => DefArcher - LostOnDefArcher;
        public int LeftOnDefAxeFighter => DefAxeFighter - LostOnDefAxeFighter;
        public int LeftOnDefBerserker => DefBerserker - LostOnDefBerserker;
        public int LeftOnDefCatapult => DefCatapult - LostOnDefCatapult;
        public int LeftOnDefHeavyCavalry => DefHeavyCavalry - LostOnDefHeavyCavalry;
        public int LeftOnDefLightCavalry => DefLightCavalry - LostOnDefLightCavalry;
        public int LeftOnDefMountedArcher => DefMountedArcher - LostOnDefMountedArcher;
        public int LeftOnDefNobleman => DefNobleman - LostOnDefNobleman;
        public int LeftOnDefPaladin => DefPaladin - LostOnDefPaladin;
        public int LeftOnDefRam => DefRam - LostOnDefRam;
        public int LeftOnDefSpearman => DefSpearman - LostOnDefSpearman;
        public int LeftOnDefSwordsman => DefSwordsman - LostOnDefSwordsman;
        public int LeftOnDefTrebuchet => DefTrebuchet - LostOnDefTrebuchet;
        #endregion

        #region Constructors

        public BattleResult()
        {
        }

        public BattleResult(InputCalculatorData input)
        {
            AtkUnits.Spearman = input.Spearman.NumberOnAttack;
            this.DefSpearman = input.Spearman.NumberOnDefense;

            AtkUnits.Swordsman = input.Swordsman.NumberOnAttack;
            this.DefSwordsman = input.Swordsman.NumberOnDefense;

            AtkUnits.AxeFighter = input.AxeFighter.NumberOnAttack;
            this.DefAxeFighter = input.AxeFighter.NumberOnDefense;

            AtkUnits.Archer = input.Archer.NumberOnAttack;
            this.DefArcher = input.Archer.NumberOnDefense;

            AtkUnits.LightCavalry = input.LightCavalry.NumberOnAttack;
            this.DefLightCavalry = input.LightCavalry.NumberOnDefense;

            AtkUnits.MountedArcher = input.MountedArcher.NumberOnAttack;
            this.DefMountedArcher = input.MountedArcher.NumberOnDefense;

            AtkUnits.HeavyCavalry = input.HeavyCavalry.NumberOnAttack;
            this.DefHeavyCavalry = input.HeavyCavalry.NumberOnDefense;

            AtkUnits.Ram = input.Ram.NumberOnAttack;
            this.DefRam = input.Ram.NumberOnDefense;

            AtkUnits.Catapult = input.Catapult.NumberOnAttack;
            this.DefCatapult = input.Catapult.NumberOnDefense;

            AtkUnits.Berserker = input.Berserker.NumberOnAttack;
            this.DefBerserker = input.Berserker.NumberOnDefense;

            AtkUnits.Trebuchet = input.Trebuchet.NumberOnAttack;
            this.DefTrebuchet = input.Trebuchet.NumberOnDefense;

            AtkUnits.Nobleman = input.Nobleman.NumberOnAttack;
            this.DefNobleman = input.Nobleman.NumberOnDefense;

            AtkUnits.Paladin = input.Paladin.NumberOnAttack;
            this.DefPaladin = input.Paladin.NumberOnDefense;

        }

        #endregion Constructors

        #region Properties

        public int AtkBattleModifier { get; set; }
        public int DefBattleModifier { get; set; }

        #endregion Properties

        #region ResultList

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
        #endregion

        #region Methods

        public BattleResult Copy()
        {
            return (BattleResult)this.MemberwiseClone();
        }

        public int GetTotalArcherAttack()
        {
            int totalAttack = 0;
            // Use the Left on to reuse this method every battle round with leftover units
            totalAttack += LeftOnAtkArcher * GameData.Archer.FightingPower;
            totalAttack += LeftOnAtkMountedArcher * GameData.MountedArcher.FightingPower;

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

        public void KillAtkArchers(decimal lostCoefficient, decimal ratio = 1)
        {
            LostOnAtkArcher = ToLossUnit(AtkArcher, ratio, lostCoefficient);
            LostOnAtkMountedArcher = ToLossUnit(AtkMountedArcher, ratio, lostCoefficient);
        }

        public void KillAtkCavalry(decimal lostCoefficient, decimal ratio = 1)
        {
            LostOnAtkLightCavalry = ToLossUnit(AtkLightCavalry, ratio, lostCoefficient); ;
            LostOnAtkHeavyCavalry = ToLossUnit(AtkHeavyCavalry, ratio, lostCoefficient); ;
        }

        public void KillAllDefInfantry()
        {
            LostOnDefSpearman = DefSpearman;
            LostOnDefSwordsman = DefSwordsman;
            LostOnDefAxeFighter = DefAxeFighter;
            LostOnDefArcher = DefArcher;
        }

        public void KillAtkInfantry(decimal lostCoefficient, decimal ratio = 1)
        {
            LostOnAtkSpearman = ToLossUnit(AtkSpearman, ratio, lostCoefficient);
            LostOnAtkSwordsman = ToLossUnit(AtkSwordsman, ratio, lostCoefficient);
            LostOnAtkAxeFighter = ToLossUnit(AtkAxeFighter, ratio, lostCoefficient);
            LostOnAtkArcher = ToLossUnit(AtkArcher, ratio, lostCoefficient);
        }

        public void KillDefInfantry(decimal ratio, decimal lostCoefficient)
        {
            LostOnDefSpearman = ToLossUnit(DefSpearman, ratio, lostCoefficient);
            LostOnDefSwordsman = ToLossUnit(DefSwordsman, ratio, lostCoefficient);
            LostOnDefAxeFighter = ToLossUnit(DefAxeFighter, ratio, lostCoefficient);
            LostOnDefArcher = ToLossUnit(DefArcher, ratio, lostCoefficient);
        }

        public void SetDefUnits(UnitSet survivingDefUnits)
        {
            DefSpearman = survivingDefUnits.Spearman;
            DefSwordsman = survivingDefUnits.Swordsman;
            DefAxeFighter = survivingDefUnits.AxeFighter;
            DefArcher = survivingDefUnits.Archer;
            DefLightCavalry = survivingDefUnits.LightCavalry;
            DefMountedArcher = survivingDefUnits.MountedArcher;
            DefHeavyCavalry = survivingDefUnits.HeavyCavalry;
            DefRam = survivingDefUnits.Ram;
            DefCatapult = survivingDefUnits.Catapult;
            DefBerserker = survivingDefUnits.Berserker;
            DefTrebuchet = survivingDefUnits.Trebuchet;
            DefNobleman = survivingDefUnits.Nobleman;
            DefPaladin = survivingDefUnits.Paladin;
        }

        public int ToLossUnit(int currentNumber, decimal ratio, decimal lostCoefficient)
        {
            decimal x = (currentNumber * ratio) * lostCoefficient;
            return (int)Math.Round(x + 0.000001m, MidpointRounding.AwayFromZero);
        }

        #endregion Methods

        #region AtkProvisions

        public int GetTotalArcherProvisions()
        {
            int totalProvisions = 0;

            totalProvisions += AtkArcher * GameData.Archer.ProvisionCost;
            totalProvisions += AtkMountedArcher * GameData.MountedArcher.ProvisionCost;

            return totalProvisions;
        }

        public int GetTotalCavalryProvisions()
        {
            int totalProvisions = 0;

            totalProvisions += AtkLightCavalry * GameData.LightCavalry.ProvisionCost;
            totalProvisions += AtkHeavyCavalry * GameData.HeavyCavalry.ProvisionCost;

            return totalProvisions;
        }

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
        #endregion
        #region Defense

        public UnitSet GetDefUnitSet(decimal ratio = 1m)
        {
            // Get proportional units 
            return new UnitSet
            {
                Spearman = GetUnitRatio(ratio, this.DefSpearman),
                Swordsman = GetUnitRatio(ratio, this.DefSwordsman),
                AxeFighter = GetUnitRatio(ratio, this.DefAxeFighter),
                Archer = GetUnitRatio(ratio, this.DefArcher),
                LightCavalry = GetUnitRatio(ratio, this.DefLightCavalry),
                MountedArcher = GetUnitRatio(ratio, this.DefMountedArcher),
                HeavyCavalry = GetUnitRatio(ratio, this.DefHeavyCavalry),
                Ram = GetUnitRatio(ratio, this.DefRam),
                Catapult = GetUnitRatio(ratio, this.DefCatapult),
                Berserker = GetUnitRatio(ratio, this.DefBerserker),
                Trebuchet = GetUnitRatio(ratio, this.DefTrebuchet),
                Nobleman = GetUnitRatio(ratio, this.DefNobleman),
                Paladin = GetUnitRatio(ratio, this.DefPaladin)
            };
        }

        public int GetTotalDefFromArchers(decimal ratio = 1m)
        {
            return GetDefUnitSet(ratio).GetTotalDefFromArchers();
        }

        public int GetTotalDefFromCavalry(decimal ratio = 1m)
        {
            return GetDefUnitSet(ratio).GetTotalDefFromCavalry();
        }

        public int GetTotalDefFromInfantry(decimal ratio = 1m)
        {
            return GetDefUnitSet(ratio).GetTotalDefFromInfantry();
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

        public int GetUnitRatio(decimal ratio, int numberOfUnits)
        {
            decimal unitRatio = ratio * (decimal)numberOfUnits;
            int unitCount = (int)Math.Round(unitRatio, MidpointRounding.AwayFromZero);
            return unitCount;
        }
        #endregion
    }
}
