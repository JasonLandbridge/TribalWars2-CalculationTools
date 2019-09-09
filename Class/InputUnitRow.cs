using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Caliburn.Micro;

namespace TribalWars2_CalculationTools.Class
{
    public class InputUnitRow : PropertyChangedBase
    {
        private int _numberOnAttack = 0;
        private int _numberOnDefense = 0;
        public string ImagePath { get; set; }

        public string Name { get; set; }

        public int NumberOnAttack
        {
            get => _numberOnAttack;
            set
            {
                _numberOnAttack = value;
                Debug.WriteLine("SUCCESS NumberOnAttack!!! " + value);

                NotifyOfPropertyChange(() => NumberOnAttack);
            }
        }

        public int NumberOnDefense
        {
            get => _numberOnDefense;
            set
            {
                _numberOnDefense = value;
                Debug.WriteLine("SUCCESS NumberOnDefense!!! " + value);
                NotifyOfPropertyChange(() => NumberOnDefense);
            }
        }
    }
}
