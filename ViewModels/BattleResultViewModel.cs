
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using TribalWars2_CalculationTools.Annotations;
using TribalWars2_CalculationTools.Class;
using TribalWars2_CalculationTools.Views.UserControls;

namespace TribalWars2_CalculationTools.ViewModels
{
    public class BattleResultViewModel : INotifyPropertyChanged
    {
        private int _atkBattleModifier = 0;

        public int AtkBattleModifier
        {
            get => _atkBattleModifier;
            set
            {
                _atkBattleModifier = value;
                OnPropertyChanged();
            }
        }

        private int _defBattleModifier = 0;

        public int DefBattleModifier
        {
            get => _defBattleModifier;
            set
            {
                _defBattleModifier = value;
                OnPropertyChanged();
            }
        }

        private BattleResultTableViewModel _attackBattleResultTable = new BattleResultTableViewModel();

        public BattleResultTableViewModel AttackBattleResultTable
        {
            get => _attackBattleResultTable;
            set
            {
                _attackBattleResultTable = value;
                OnPropertyChanged();
            }
        }

        private BattleResultTableViewModel _defenseBattleResultTable = new BattleResultTableViewModel();

        public BattleResultTableViewModel DefenseBattleResultTable
        {
            get => _defenseBattleResultTable;
            set
            {
                _defenseBattleResultTable = value;
                OnPropertyChanged();
            }
        }

        public BattleResultViewModel()
        {
            for (int i = 0; i < GameData.NumberOfUnits; i++)
            {
                BattleResultValue defaultValue = new BattleResultValue(0);

                AttackBattleResultTable.UnitAmount.Add(defaultValue);
                AttackBattleResultTable.UnitLost.Add(defaultValue);

                DefenseBattleResultTable.UnitAmount.Add(defaultValue);
                DefenseBattleResultTable.UnitLost.Add(defaultValue);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public void UpdateBattleResult(BattleResult battleResult)
        {

            // AttackBattleResultTable.UnitAmount.Add(new BattleResultValue(9));

            AtkBattleModifier = battleResult.AtkBattleModifier;
            DefBattleModifier = battleResult.DefBattleModifier;

            AttackBattleResultTable.UnitAmount.Clear();
            AttackBattleResultTable.UnitLost.Clear();
            AttackBattleResultTable.UnitsLeft.Clear();

            DefenseBattleResultTable.UnitAmount.Clear();
            DefenseBattleResultTable.UnitLost.Clear();
            DefenseBattleResultTable.UnitsLeft.Clear();

            for (int i = 0; i < battleResult.ListOfAtkNumbers.Count; i++)
            {
                AttackBattleResultTable.UnitAmount.Add(battleResult.ListOfAtkNumbers[i]);
                AttackBattleResultTable.UnitLost.Add(battleResult.ListOfAtkLostNumbers[i]);
                AttackBattleResultTable.UnitsLeft.Add(battleResult.ListOfAtkLeftNumbers[i]);

                DefenseBattleResultTable.UnitAmount.Add(battleResult.ListOfDefNumbers[i]);
                DefenseBattleResultTable.UnitLost.Add(battleResult.ListOfDefLostNumbers[i]);
                DefenseBattleResultTable.UnitsLeft.Add(battleResult.ListOfDefLeftNumbers[i]);


            }
        }
    }
}
