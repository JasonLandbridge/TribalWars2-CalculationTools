using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Caliburn.Micro;

namespace TribalWars2_CalculationTools.Class
{
    public class InputCalculatorClass : PropertyChangedBase
    {
        private BindableCollection<InputUnitRow> _units = new BindableCollection<InputUnitRow>();

        private InputUnitRow _spearman;
        private InputUnitRow _swordsman;

        public InputUnitRow Spearman
        {
            get => _spearman;
            set
            {
                _spearman = value;
                Debug.WriteLine("SUCCESS Spearman!!! " + value);

                NotifyOfPropertyChange(() => Spearman);
            }
        }

        public InputUnitRow Swordsman
        {
            get => _swordsman;
            set
            {
                _swordsman = value;
                Debug.WriteLine("SUCCESS Swordsman!!! " + value);

                NotifyOfPropertyChange(() => Swordsman);
            }
        }

        public BindableCollection<InputUnitRow> Units
        {
            get => _units;
            set
            {

                _units = value;
            }
        }

        public InputCalculatorClass()
        {
            Units.Add(Spearman = new InputUnitRow
            {
                ImagePath = GameData.Spearman.ImagePath,
                Name = GameData.Spearman.Name,
            });

            Units.Add(Swordsman = new InputUnitRow
            {
                ImagePath = GameData.Swordsman.ImagePath,
                Name = GameData.Swordsman.Name,
            });


        }
    }
}
