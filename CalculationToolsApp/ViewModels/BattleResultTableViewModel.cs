using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using TribalWars2_CalculationTools.Annotations;
using TribalWars2_CalculationTools.Class;

namespace TribalWars2_CalculationTools.ViewModels
{
    public class BattleResultTableViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<BattleResultValue> _unitLost = new ObservableCollection<BattleResultValue>();
        private ObservableCollection<BattleResultValue> _unitAmount = new ObservableCollection<BattleResultValue>();
        private ObservableCollection<BattleResultValue> _unitsLeft = new ObservableCollection<BattleResultValue>();
        private string _wallResult = "";
        private bool _showWallResult = false;

        public ObservableCollection<BattleResultValue> UnitAmount
        {
            get => _unitAmount;
            set
            {
                _unitAmount = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BattleResultValue> UnitLost
        {
            get => _unitLost;
            set
            {
                _unitLost = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BattleResultValue> UnitsLeft
        {
            get => _unitsLeft;
            set
            {
                _unitsLeft = value;
                OnPropertyChanged();
            }
        }

        public string WallResult
        {
            get => _wallResult;
            set
            {
                _wallResult = value;
                OnPropertyChanged();
            }
        }
        public bool ShowWallResult
        {
            get => _showWallResult;
            set
            {
                _showWallResult = value;
                OnPropertyChanged();
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
