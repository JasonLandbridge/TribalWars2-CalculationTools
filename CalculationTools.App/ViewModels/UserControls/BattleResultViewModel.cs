using System.Collections.ObjectModel;
using CalculationTools.Core;

namespace CalculationTools.App
{
    public class BattleResultViewModel : BaseViewModel
    {

        public int AtkBattleModifier { get; set; }
        public int DefBattleModifier { get; set; }

        public BattleResultTableViewModel AttackBattleResultTable { get; set; } = new BattleResultTableViewModel();
        public BattleResultTableViewModel DefenseBattleResultTable { get; set; } = new BattleResultTableViewModel();

        public BattleResultViewModel()
        {


            AttackBattleResultTable.Header = "Attacking Units";
            DefenseBattleResultTable.Header = "Defending Units";

            AttackBattleResultTable.UnitAmount.Header = "Amount";
            AttackBattleResultTable.UnitLost.Header = "Losses";
            AttackBattleResultTable.UnitsLeft.Header = "Survivors";
            AttackBattleResultTable.ShowWallResult = false;

            DefenseBattleResultTable.UnitAmount.Header = "Amount";
            DefenseBattleResultTable.UnitLost.Header = "Losses";
            DefenseBattleResultTable.UnitsLeft.Header = "Survivors";
            DefenseBattleResultTable.WallResult.Header = "Wall";
            DefenseBattleResultTable.ShowWallResult = true;

            for (int i = 0; i < GameData.NumberOfUnits; i++)
            {
                BattleResultValue defaultValue = new BattleResultValue(0);

                AttackBattleResultTable.UnitAmount.BattleResultValues.Add(defaultValue);
                AttackBattleResultTable.UnitLost.BattleResultValues.Add(defaultValue);

                DefenseBattleResultTable.UnitAmount.BattleResultValues.Add(defaultValue);
                DefenseBattleResultTable.UnitLost.BattleResultValues.Add(defaultValue);
            }
        }

        public void UpdateBattleResult(BattleResult battleResult)
        {

            AttackBattleResultTable.BattleModifier = battleResult.AtkBattleModifier.ToString();
            DefenseBattleResultTable.BattleModifier = battleResult.DefBattleModifier.ToString();

            DefenseBattleResultTable.WallResult.Content = battleResult.WallResult;

            AttackBattleResultTable.UnitAmount.BattleResultValues = new ObservableCollection<BattleResultValue>(battleResult.ListOfAtkNumbers);
            AttackBattleResultTable.UnitLost.BattleResultValues = new ObservableCollection<BattleResultValue>(battleResult.ListOfAtkLostNumbers);
            AttackBattleResultTable.UnitsLeft.BattleResultValues = new ObservableCollection<BattleResultValue>(battleResult.ListOfAtkLeftNumbers);

            DefenseBattleResultTable.UnitAmount.BattleResultValues = new ObservableCollection<BattleResultValue>(battleResult.ListOfDefNumbers);
            DefenseBattleResultTable.UnitLost.BattleResultValues = new ObservableCollection<BattleResultValue>(battleResult.ListOfDefLostNumbers);
            DefenseBattleResultTable.UnitsLeft.BattleResultValues = new ObservableCollection<BattleResultValue>(battleResult.ListOfDefLeftNumbers);

        }
    }
}
