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

        public int GetTotalArcherAttack()
        {
            int totalAttack = 0;
            // Use the Left on to reuse this method every battle round with leftover units
            totalAttack += AtkUnitsLeft.Archer * GameData.Archer.FightingPower;
            totalAttack += AtkUnitsLeft.MountedArcher * GameData.MountedArcher.FightingPower;

            return totalAttack;
        }

        public int GetTotalCavalryAttack()
        {
            int totalAttack = 0;
            // Use the Left on to reuse this method every battle round with leftover units
            totalAttack += AtkUnitsLeft.LightCavalry * GameData.LightCavalry.FightingPower;
            totalAttack += AtkUnitsLeft.HeavyCavalry * GameData.HeavyCavalry.FightingPower;

            return totalAttack;
        }

        public int GetTotalInfantryAttack(bool defenseIsSuperior)
        {
            int totalAttack = 0;
            // Use the LeftOn to reuse this method every battle round with leftover units
            totalAttack += AtkUnitsLeft.Spearman * GameData.Spearman.FightingPower;
            totalAttack += AtkUnitsLeft.Swordsman * GameData.Swordsman.FightingPower;
            totalAttack += AtkUnitsLeft.AxeFighter * GameData.AxeFighter.FightingPower;
            totalAttack += AtkUnitsLeft.Nobleman * GameData.Nobleman.FightingPower;

            // Take into account that berserkers
            // are twice as strong when fighting a superior army
            if (defenseIsSuperior)
            {
                totalAttack += AtkUnitsLeft.Berserker * (GameData.Berserker.FightingPower * 2);
            }
            else
            {
                totalAttack += AtkUnitsLeft.Berserker * GameData.Berserker.FightingPower;
            }

            return totalAttack;
        }

        public void KillAtkArchers(decimal lostCoefficient, decimal ratio = 1)
        {
            AtkUnitsLost.Archer = ToLossUnit(AtkUnits.Archer, ratio, lostCoefficient);
            AtkUnitsLost.MountedArcher = ToLossUnit(AtkUnits.MountedArcher, ratio, lostCoefficient);
        }

        public void KillAtkCavalry(decimal lostCoefficient, decimal ratio = 1)
        {
            AtkUnitsLost.LightCavalry = ToLossUnit(AtkUnits.LightCavalry, ratio, lostCoefficient); ;
            AtkUnitsLost.HeavyCavalry = ToLossUnit(AtkUnits.HeavyCavalry, ratio, lostCoefficient); ;
        }

        public void KillAtkInfantry(decimal lostCoefficient, decimal ratio = 1)
        {
            AtkUnitsLost.Spearman = ToLossUnit(AtkUnitsLost.Spearman, ratio, lostCoefficient);
            AtkUnitsLost.Swordsman = ToLossUnit(AtkUnitsLost.Swordsman, ratio, lostCoefficient);
            AtkUnitsLost.AxeFighter = ToLossUnit(AtkUnitsLost.AxeFighter, ratio, lostCoefficient);
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

            totalProvisions += AtkUnits.Archer * GameData.Archer.ProvisionCost;
            totalProvisions += AtkUnits.MountedArcher * GameData.MountedArcher.ProvisionCost;

            return totalProvisions;
        }

        public int GetTotalCavalryProvisions()
        {
            int totalProvisions = 0;

            totalProvisions += AtkUnits.LightCavalry * GameData.LightCavalry.ProvisionCost;
            totalProvisions += AtkUnits.HeavyCavalry * GameData.HeavyCavalry.ProvisionCost;

            return totalProvisions;
        }

        public int GetTotalInfantryProvisions()
        {
            int totalProvisions = 0;
            // Count all units that are NOT Cavalry and NOT archers
            totalProvisions += AtkUnits.Spearman * GameData.Spearman.ProvisionCost;
            totalProvisions += AtkUnits.Swordsman * GameData.Swordsman.ProvisionCost;
            totalProvisions += AtkUnits.AxeFighter * GameData.AxeFighter.ProvisionCost;

            totalProvisions += AtkUnits.Ram * GameData.Ram.ProvisionCost;
            totalProvisions += AtkUnits.Catapult * GameData.Catapult.ProvisionCost;
            totalProvisions += AtkUnits.Trebuchet * GameData.Trebuchet.ProvisionCost;
            totalProvisions += AtkUnits.Berserker * GameData.Berserker.ProvisionCost;

            totalProvisions += AtkUnits.Nobleman * GameData.Nobleman.ProvisionCost;
            totalProvisions += AtkUnits.Paladin * GameData.Paladin.ProvisionCost;

            return totalProvisions;
        }
        #endregion
        #region Defense

        public UnitSet GetDefUnitSet(decimal ratio = 1m)
        {
            // Get proportional units 
            return new UnitSet
            {
                Spearman = GetUnitRatio(ratio, DefUnits.Spearman),
                Swordsman = GetUnitRatio(ratio, DefUnits.Swordsman),
                AxeFighter = GetUnitRatio(ratio, DefUnits.AxeFighter),
                Archer = GetUnitRatio(ratio, DefUnits.Archer),
                LightCavalry = GetUnitRatio(ratio, DefUnits.LightCavalry),
                MountedArcher = GetUnitRatio(ratio, DefUnits.MountedArcher),
                HeavyCavalry = GetUnitRatio(ratio, DefUnits.HeavyCavalry),
                Ram = GetUnitRatio(ratio, DefUnits.Ram),
                Catapult = GetUnitRatio(ratio, DefUnits.Catapult),
                Berserker = GetUnitRatio(ratio, DefUnits.Berserker),
                Trebuchet = GetUnitRatio(ratio, DefUnits.Trebuchet),
                Nobleman = GetUnitRatio(ratio, DefUnits.Nobleman),
                Paladin = GetUnitRatio(ratio, DefUnits.Paladin)
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
