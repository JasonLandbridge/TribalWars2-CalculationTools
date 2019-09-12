
using System;
using System.Collections.Generic;
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
                BattleResultValue defaultValue = new BattleResultValue { Value = 5 };

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
    }
}
