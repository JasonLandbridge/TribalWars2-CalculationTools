using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ClassLibrary.Class;
using ClassLibrary.Enums;
using ClassLibrary.Structs;
using TribalWars2_CalculationTools.ViewModels;

namespace TribalWars2_CalculationTools.Models
{
    public class CalculatorData : INotifyPropertyChanged
    {

        private BattleResult _lastBattleResult = new BattleResult();

        public BattleResult LastBattleResult
        {
            get => _lastBattleResult;
            set
            {
                _lastBattleResult = value;
                OnPropertyChanged();

            }
        }

        private BattleResultViewModel _battleResultViewModel;

        public CalculatorData()
        {

        }
        public CalculatorData(BattleResultViewModel BattleResultViewModel)
        {
            _battleResultViewModel = BattleResultViewModel;
        }


        public int PreRound(ref BattleResult result, WeaponSet atkWeapon)
        {
            // Based on https://en.forum.tribalwars2.com/index.php?threads/unraveling-some-myths-regarding-the-battle-engine.3959/

            int wallLevel = result.WallLevelBefore;
            decimal atkModifier = result.AtkBattleModifier / 100m;
            int ironWall = 0; //TODO Need to make an battleCalculatorInput that takes the Tribe skill Iron wall into account
            decimal paladinModifier = (atkWeapon.BelongsToUnitType == UnitType.Ram ? atkWeapon.AtkModifier : 0) + 1;

            UnitSet atkUnits = result.AtkUnits;
            UnitSet defUnits = result.DefUnits;
            UnitSet atkUnitsLost = result.AtkUnitsLost;

            // Calculate how many rams were killed by the Trebuchet
            int ramsKilled = GameData.GetRamsKilled(atkUnits.Ram, atkUnits.Catapult, defUnits.Trebuchet);
            atkUnits.Ram -= ramsKilled;
            atkUnitsLost.Ram = ramsKilled;

            // Get the total attacking provisions without the ram provisions included.
            int totalProvisionsWithNoRams = atkUnits.GetTotalProvisions() - atkUnits.GetTotalRamProvisions();

            // Get base wall defense
            int wallDefense = GameData.GetWallDefense(wallLevel);

            // This is meant to calculate the "active" rams that will do damage in relation to the attacking force. 
            // More infantry = more damage with the rams
            decimal ramRatio = Math.Clamp((decimal)totalProvisionsWithNoRams / (defUnits.GetTotalProvisions() + wallDefense), 0, 1);
            int wallHitPoints = (GameData.Wall.GetHitPoints(wallLevel) * 2);

            // This is the net wall damage done by the rams
            decimal wallDamage = (atkUnits.Ram * ramRatio * atkModifier * paladinModifier) / wallHitPoints > 0 ? wallHitPoints : 0.0001m;


            // Calculate the new wall level after damage applied
            int resultingWallLevel;
            // If the wall is already below the Iron Wall threshold then don't change anything. 
            if (wallLevel <= ironWall)
            {
                resultingWallLevel = wallLevel;
            }
            else
            {
                if (wallLevel - ironWall < -wallDamage)
                {
                    resultingWallLevel = wallLevel < ironWall ? wallLevel : ironWall;
                }
                else
                {
                    decimal rawLevel = wallLevel - wallDamage;
                    resultingWallLevel = (int)Math.Round(rawLevel, MidpointRounding.AwayFromZero);
                }
            }

            result.WallLevelAfter = Math.Clamp(resultingWallLevel, 0, 20);
            result.AtkUnitsLost = atkUnitsLost;
            return result.WallLevelAfter;

        }

        public void PostBattle(ref BattleResult result, WeaponSet atkWeapon)
        {
            if (result.WallLevelAfter == 0)
            {
                return;
            }
            decimal atkModifier = result.AtkBattleModifier / 100m;

            decimal paladinModifier = (atkWeapon.BelongsToUnitType == UnitType.Ram ? atkWeapon.AtkModifier : 0) + 1;

            int wallHitPoints = GameData.Wall.GetHitPoints(result.WallLevelAfter) * 2;
            decimal damage = (result.AtkUnitsLeft.Ram * atkModifier * paladinModifier) / wallHitPoints;
            decimal wallDamage = damage / wallHitPoints;


            // Calculate the new wall level after damage applied
            int finalWallLevel;
            int afterBattleWall = result.WallLevelAfter;
            int ironWall = 0; //TODO Need to make an battleCalculatorInput that takes the Tribe skill Iron wall into account
            // If the wall is already below the Iron Wall threshold then don't change anything. 
            if (afterBattleWall <= ironWall)
            {
                finalWallLevel = afterBattleWall;
            }
            else
            {
                if (afterBattleWall - ironWall < -wallDamage)
                {
                    finalWallLevel = afterBattleWall < ironWall ? afterBattleWall : ironWall;
                }
                else
                {
                    decimal rawLevel = Math.Clamp(afterBattleWall - wallDamage, 0, 20);
                    finalWallLevel = (int)Math.Round(rawLevel, MidpointRounding.AwayFromZero);
                }
            }
            result.WallLevelAfter = finalWallLevel;

        }
        public void SimulateBattle(BattleCalculatorInputViewModel battleCalculatorInput)
        {


            // Based on: Tribal Wars 2 - Tutorial: Basic Battle System - https://www.youtube.com/watch?v=SG_qI1-go88
            // Based on: Battle Simulator - http://www.ds-pro.de/2/simulator.php
            BattleResult result = battleCalculatorInput.ToBattleResult();

            decimal atkFaithBonus = battleCalculatorInput.InputAtkChurch.Modifier;
            decimal defFaithBonus = battleCalculatorInput.InputDefChurch.Modifier;
            decimal morale = battleCalculatorInput.InputMorale / 100m;
            decimal luck = 1m + (battleCalculatorInput.InputLuck / 100m);
            decimal nightBonus = (battleCalculatorInput.InputNightBonus ? 2m : 1m);
            decimal officerBonus = (battleCalculatorInput.InputGrandmasterBonus ? 0.1m : 0m);
            WeaponSet paladinAtkWeapon = battleCalculatorInput.GetAtkWeapon();
            WeaponSet paladinDefWeapon = battleCalculatorInput.GetDefWeapon();


            decimal atkModifier = GameData.GetAtkBattleModifier(atkFaithBonus, morale, luck, officerBonus);
            result.AtkBattleModifier = (int)(atkModifier * 100m);
            decimal defModifier = GameData.GetDefBattleModifier(defFaithBonus, result.WallLevelBefore, nightBonus);
            result.DefBattleModifier = (int)(defModifier * 100m);

            int resultingWallLevel = PreRound(ref result, paladinAtkWeapon);
            defModifier = GameData.GetDefBattleModifier(defFaithBonus, resultingWallLevel, nightBonus);
            result.DefBattleModifier = (int)(defModifier * 100m);


            // Stop here if there are no units given
            if (!battleCalculatorInput.IsValid)
            {
                LastBattleResult = result;

                _battleResultViewModel.UpdateBattleResult(LastBattleResult);

                return;
            }

            List<BattleResult> BattleHistory = new List<BattleResult> { result.Copy() };
            BattleResult currentRound = BattleHistory.Last();

            // Set the atkUnits - minus the lost siege units and set that as the attacking group
            currentRound.AtkUnits -= result.AtkUnitsLost;
            currentRound.AtkUnitsLost.Clear();
            // Simulate for 3 rounds (infantry, cavalry and archers)
            bool battleDetermined = false;
            int wallDefense = result.WallDefenseAfter;

            while (!battleDetermined)
            {
                currentRound = BattleHistory.Last();

                UnitSet atkUnits = currentRound.AtkUnits;
                UnitSet atkUnitsLost = currentRound.AtkUnitsLost;
                UnitSet defUnits = currentRound.DefUnits;

                int atkInfantryProvisions = atkUnits.GetTotalInfantryProvisions();
                int atkCavalryProvisions = atkUnits.GetTotalCavalryProvisions();
                int atkArchersProvisions = atkUnits.GetTotalArcherProvisions();
                int atkSpecialProvisions = atkUnits.GetTotalSpecialProvisions();

                int totalAtkProvisions = atkInfantryProvisions + atkCavalryProvisions + atkArchersProvisions + atkSpecialProvisions;

                int totalDefProvisions = defUnits.GetTotalProvisions();
                if (totalAtkProvisions == 0)
                {
                    break;
                }

                bool defSuperior = (totalAtkProvisions * 2 <= totalDefProvisions);
                int atkInfantry = atkUnits.GetTotalInfantryAttack(paladinAtkWeapon, defSuperior);
                int atkCavalry = atkUnits.GetTotalCavalryAttack(paladinAtkWeapon);
                int atkArchers = atkUnits.GetTotalArcherAttack(paladinAtkWeapon);
                int atkSpecial = atkUnits.GetTotalSpecialAtk();
                int totalAtk = atkInfantry + atkCavalry + atkArchers + atkSpecial;

                int strongestGroupIndex;

                // Determine which group the special units join during battle, the strongest one gets them
                if (atkInfantry > atkCavalry && atkInfantry > atkArchers)
                {
                    //AtkInfantry is the strongest group
                    atkInfantry += atkSpecial;
                    atkInfantryProvisions += atkSpecialProvisions;
                    strongestGroupIndex = 1;
                }
                else if (atkCavalry > atkInfantry && atkCavalry > atkInfantry)
                {
                    //atkCavalry is the strongest group
                    atkCavalry += atkSpecial;
                    atkCavalryProvisions += atkSpecialProvisions;
                    strongestGroupIndex = 2;
                }
                else
                {
                    //AtkArchers is the strongest group
                    atkArchers += atkSpecial;
                    atkArchersProvisions += atkSpecialProvisions;
                    strongestGroupIndex = 3;
                }

                // Add Atk modifier
                atkInfantry = GameData.AddAtkModifier(atkInfantry, atkModifier);
                atkCavalry = GameData.AddAtkModifier(atkCavalry, atkModifier);
                atkArchers = GameData.AddAtkModifier(atkArchers, atkModifier);

                decimal atkInfantryRatio = GameData.GetUnitProvisionRatio(atkInfantryProvisions, totalAtkProvisions);
                decimal atkCavalryRatio = GameData.GetUnitProvisionRatio(atkCavalryProvisions, totalAtkProvisions);
                decimal atkArchersRatio = GameData.GetUnitProvisionRatio(atkArchersProvisions, totalAtkProvisions);
                decimal totalRatio = atkInfantryRatio + atkCavalryRatio + atkArchersRatio;


                // These units sets contains the defensive units proportionate to the atkInfantry ratio
                UnitSet infantryGroupDefUnitSet = defUnits.GetUnitsByRatio(atkInfantryRatio);
                UnitSet cavalryGroupDefUnitSet = defUnits.GetUnitsByRatio(atkCavalryRatio);
                UnitSet archerGroupDefUnitSet = defUnits.GetUnitsByRatio(atkArchersRatio);


                int totalDefFromInfantry = infantryGroupDefUnitSet.GetTotalDefFromInfantry(paladinDefWeapon);
                int totalDefFromCavalry = cavalryGroupDefUnitSet.GetTotalDefFromCavalry(paladinDefWeapon);
                int totalDefFromArchers = archerGroupDefUnitSet.GetTotalDefFromArchers(paladinDefWeapon);

                // Add defense modifier
                totalDefFromInfantry = GameData.AddDefModifier(totalDefFromInfantry, defModifier) + wallDefense;
                totalDefFromCavalry = GameData.AddDefModifier(totalDefFromCavalry, defModifier) + wallDefense;
                totalDefFromArchers = GameData.AddDefModifier(totalDefFromArchers, defModifier) + wallDefense;


                int totalDef = totalDefFromInfantry + totalDefFromCavalry + totalDefFromArchers;

                // Used to keep track of how many units are lost this round
                UnitSet infantryGroupDefUnitSetLost = new UnitSet(), cavalryGroupDefUnitSetLost = new UnitSet(), archerGroupDefUnitSetLost = new UnitSet();

                // Determine the result of each mini-round by comparing the attack vs defense strength.
                // If both sides have 0 forces then ignore the result and leave the result undefined (null)
                bool? atkWonRound1 = null, atkWonRound2 = null, atkWonRound3 = null;
                List<bool> miniBattleResult = new List<bool>();
                if (atkInfantry > 0 && totalDefFromInfantry > 0)
                {
                    atkWonRound1 = atkInfantry >= totalDefFromInfantry;
                    miniBattleResult.Add((bool)atkWonRound1);
                }

                if (atkCavalry > 0 && totalDefFromCavalry > 0)
                {
                    atkWonRound2 = (atkCavalry >= totalDefFromCavalry);
                    miniBattleResult.Add((bool)atkWonRound2);
                }

                if (atkArchers > 0 && totalDefFromArchers > 0)
                {
                    atkWonRound3 = (atkArchers >= totalDefFromArchers);
                    miniBattleResult.Add((bool)atkWonRound3);
                }

                // For every round that Defense won, kill the attacking party and vice versa
                // General / Infantry round
                decimal killRate;
                if (atkWonRound1 != null)
                {
                    if ((bool)atkWonRound1)
                    {
                        killRate = GameData.GetAtkKillRate(atkInfantry, totalDefFromInfantry);
                        atkUnitsLost += atkUnits.ApplyKillRateAtkInfantry(killRate);
                        infantryGroupDefUnitSetLost = infantryGroupDefUnitSet.ApplyKillRate(1);
                        if (strongestGroupIndex == 1) atkUnitsLost += atkUnits.ApplyKillRateAtkSpecial(killRate);
                    }
                    else
                    {
                        killRate = GameData.GetDefKillRate(atkInfantry, totalDefFromInfantry);
                        atkUnitsLost += atkUnits.ApplyKillRateAtkInfantry(1);
                        atkUnitsLost += atkUnits.ApplyKillRateAtkSpecial(1);
                        infantryGroupDefUnitSetLost = infantryGroupDefUnitSet.ApplyKillRate(killRate);
                    }
                }

                // Cavalry round
                if (atkWonRound2 != null)
                {
                    if ((bool)atkWonRound2)
                    {
                        killRate = GameData.GetAtkKillRate(atkCavalry, totalDefFromCavalry);
                        atkUnitsLost += atkUnits.ApplyKillRateAtkCavalry(killRate);
                        cavalryGroupDefUnitSetLost = cavalryGroupDefUnitSet.ApplyKillRate(1);
                        if (strongestGroupIndex == 2) atkUnitsLost += atkUnits.ApplyKillRateAtkSpecial(killRate);
                    }
                    else
                    {
                        killRate = GameData.GetDefKillRate(atkCavalry, totalDefFromCavalry);
                        atkUnitsLost += atkUnits.ApplyKillRateAtkCavalry(1);
                        atkUnitsLost += atkUnits.ApplyKillRateAtkSpecial(1);
                        cavalryGroupDefUnitSetLost = cavalryGroupDefUnitSet.ApplyKillRate(killRate);
                    }
                }

                // Archer round
                if (atkWonRound3 != null)
                {
                    if ((bool)atkWonRound3)
                    {
                        killRate = GameData.GetAtkKillRate(atkArchers, totalDefFromArchers);
                        atkUnitsLost += atkUnits.ApplyKillRateAtkArchers(killRate);
                        archerGroupDefUnitSetLost = archerGroupDefUnitSet.ApplyKillRate(1);
                        if (strongestGroupIndex == 3) atkUnitsLost += atkUnits.ApplyKillRateAtkSpecial(killRate);

                    }
                    else
                    {
                        killRate = GameData.GetDefKillRate(atkArchers, totalDefFromArchers);
                        atkUnitsLost += atkUnits.ApplyKillRateAtkArchers(1);
                        atkUnitsLost += atkUnits.ApplyKillRateAtkSpecial(1);
                        archerGroupDefUnitSetLost = archerGroupDefUnitSet.ApplyKillRate(killRate);

                    }

                }

                UnitSet survivingDefUnits = infantryGroupDefUnitSet + cavalryGroupDefUnitSet + archerGroupDefUnitSet;
                UnitSet defUnitsLost = infantryGroupDefUnitSetLost + cavalryGroupDefUnitSetLost + archerGroupDefUnitSetLost;

                currentRound.AtkUnits = atkUnits;
                currentRound.AtkUnitsLost = atkUnitsLost;
                currentRound.DefUnits = survivingDefUnits;
                currentRound.DefUnitsLost = defUnitsLost;

                // Check if during the 3 mini-battles either attack of defense won all mini-battles
                battleDetermined = !(miniBattleResult.Contains(false) && miniBattleResult.Contains(true));
                if (!battleDetermined)
                {
                    // WallDefense is only added the first round
                    wallDefense = 0;
                    BattleHistory.Add(currentRound.Copy());
                    //Reset the UnitLost for subsequent rounds
                    BattleHistory.Last().AtkUnitsLost = new UnitSet();
                    BattleHistory.Last().DefUnitsLost = new UnitSet();

                }
            }


            BattleResult finalResult = result.Copy();

            foreach (BattleResult battleResult in BattleHistory)
            {
                finalResult.AtkUnitsLost += battleResult.AtkUnitsLost;
                finalResult.DefUnitsLost += battleResult.DefUnitsLost;
            }

            // Simulate the post battle calculations
            PostBattle(ref finalResult, paladinAtkWeapon);

            LastBattleResult = finalResult;
            _battleResultViewModel.UpdateBattleResult(LastBattleResult);

        }




        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
