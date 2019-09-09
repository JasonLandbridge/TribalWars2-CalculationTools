﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Caliburn.Micro;
using TribalWars2_CalculationTools.Class.Enum;
using TribalWars2_CalculationTools.Models;

namespace TribalWars2_CalculationTools.Class.Units
{
    public abstract class BaseUnit : PropertyChangedBase
    {

        private int _numberOnAttack;
        private int _numberOnDefense;
        public abstract string Name { get; }
        public abstract UnitType UnitType { get; set; }

        /*Unit Cost*/
        public abstract int WoodCost { get; }
        public abstract int ClayCost { get; }
        public abstract int IronCost { get; }
        public abstract int ProvisionCost { get; }

        /*Fight values*/
        public abstract int FightingPower { get; set; }
        public abstract int DefenseFromInfantry { get; set; }
        public abstract int DefenseFromCavalry { get; set; }
        public abstract int DefenseFromArchers { get; set; }

        public abstract int LoadCapacity { get; set; }

        public abstract TimeSpan BaseRecruitmentTime { get; set; }
        public abstract TimeSpan TravelTimePerTile { get; set; }

        public int NumberOnAttack
        {
            get => _numberOnAttack;
            set
            {
                _numberOnAttack = value;
                NotifyOfPropertyChange(() => NumberOnAttack);
                this.PropertyUpdated();
            }
        }

        public int NumberOnDefense
        {
            get => _numberOnDefense;
            set
            {
                _numberOnDefense = value;
                NotifyOfPropertyChange(() => NumberOnDefense);
                this.PropertyUpdated();
            }
        }

        public string ImagePath => $"/Resources/Img/unit_{this.Name.ToLower().Replace(' ', '_')}.png";


        // Used as a signal to update the calculation 
        public void PropertyUpdated()
        {
            //this.Parent.ValueUpdated();
        }

        public int GetTotalInfantryAtkStrength => FightingPower * NumberOnAttack;

        public int GetTotalDefFromInfantry => DefenseFromInfantry * NumberOnDefense;

    }
}
