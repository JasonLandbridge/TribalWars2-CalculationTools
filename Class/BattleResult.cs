using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using TribalWars2_CalculationTools.Class.Units;

namespace TribalWars2_CalculationTools.Class
{
    public class BattleResult
    {
        #region AttackingInput

        public int AtkSpearman { get; set; }
        public int AtkSwordsman { get; set; }
        public int AtkAxeFighter { get; set; }
        public int AtkArcher { get; set; }
        public int AtkLightCavalry { get; set; }
        public int AtkMountedArcher { get; set; }
        public int AtkHeavyCavalry { get; set; }
        public int AtkRam { get; set; }
        public int AtkCatapult { get; set; }
        public int AtkBerserker { get; set; }
        public int AtkTrebuchet { get; set; }
        public int AtkNobleman { get; set; }
        public int AtkPaladin { get; set; }

        #endregion

        #region LostOnAttackingInput

        public int LostOnAtkSpearman { get; set; }
        public int LostOnAtkSwordsman { get; set; }
        public int LostOnAtkAxeFighter { get; set; }
        public int LostOnAtkArcher { get; set; }
        public int LostOnAtkLightCavalry { get; set; }
        public int LostOnAtkMountedArcher { get; set; }
        public int LostOnAtkHeavyCavalry { get; set; }
        public int LostOnAtkRam { get; set; }
        public int LostOnAtkCatapult { get; set; }
        public int LostOnAtkBerserker { get; set; }
        public int LostOnAtkTrebuchet { get; set; }
        public int LostOnAtkNobleman { get; set; }
        public int LostOnAtkPaladin { get; set; }

        #endregion


        #region DefendingInput

        public int DefSpearman { get; set; }
        public int DefSwordsman { get; set; }
        public int DefAxeFighter { get; set; }
        public int DefArcher { get; set; }
        public int DefLightCavalry { get; set; }
        public int DefMountedArcher { get; set; }
        public int DefHeavyCavalry { get; set; }
        public int DefRam { get; set; }
        public int DefCatapult { get; set; }
        public int DefBerserker { get; set; }
        public int DefTrebuchet { get; set; }
        public int DefNobleman { get; set; }
        public int DefPaladin { get; set; }
        #endregion

        #region LostOnDefendingInput
        public int LostOnDefSpearman { get; set; }
        public int LostOnDefSwordsman { get; set; }
        public int LostOnDefAxeFighter { get; set; }
        public int LostOnDefArcher { get; set; }
        public int LostOnDefLightCavalry { get; set; }
        public int LostOnDefMountedArcher { get; set; }
        public int LostOnDefHeavyCavalry { get; set; }
        public int LostOnDefRam { get; set; }
        public int LostOnDefCatapult { get; set; }
        public int LostOnDefBerserker { get; set; }
        public int LostOnDefTrebuchet { get; set; }
        public int LostOnDefNobleman { get; set; }
        public int LostOnDefPaladin { get; set; }
        #endregion

        #region ResultList

        public BindableCollection<ValueType> ListOfAtkNumbers =>
            new BindableCollection<ValueType>
            {
                new ValueType(AtkSpearman),
                new ValueType(AtkSwordsman),
                new ValueType(AtkAxeFighter),
                new ValueType(AtkArcher),
                new ValueType(AtkLightCavalry),
                new ValueType(AtkMountedArcher),
                new ValueType(AtkHeavyCavalry),
                new ValueType(AtkRam),
                new ValueType(AtkCatapult),
                new ValueType(AtkBerserker),
                new ValueType(AtkTrebuchet),
                new ValueType(AtkNobleman),
                new ValueType(AtkPaladin),
            };
        public BindableCollection<ValueType> ListOfAtkLostNumbers =>
            new BindableCollection<ValueType>
            {
                new ValueType(LostOnAtkSpearman),
                new ValueType(LostOnAtkSwordsman),
                new ValueType(LostOnAtkAxeFighter),
                new ValueType(LostOnAtkArcher),
                new ValueType(LostOnAtkLightCavalry),
                new ValueType(LostOnAtkMountedArcher),
                new ValueType(LostOnAtkHeavyCavalry),
                new ValueType(LostOnAtkRam),
                new ValueType(LostOnAtkCatapult),
                new ValueType(LostOnAtkBerserker),
                new ValueType(LostOnAtkTrebuchet),
                new ValueType(LostOnAtkNobleman),
                new ValueType(LostOnAtkPaladin),
            };

        public BindableCollection<ValueType> ListOfDefNumbers =>
            new BindableCollection<ValueType>
            {
                new ValueType(DefSpearman),
                new ValueType(DefSwordsman),
                new ValueType(DefAxeFighter),
                new ValueType(DefArcher),
                new ValueType(DefLightCavalry),
                new ValueType(DefMountedArcher),
                new ValueType(DefHeavyCavalry),
                new ValueType(DefRam),
                new ValueType(DefCatapult),
                new ValueType(DefBerserker),
                new ValueType(DefTrebuchet),
                new ValueType(DefNobleman),
                new ValueType(DefPaladin),
            };

        public BindableCollection<ValueType> ListOfDefLostNumbers =>
            new BindableCollection<ValueType>
            {
                new ValueType(LostOnDefSpearman),
                new ValueType(LostOnDefSwordsman),
                new ValueType(LostOnDefAxeFighter),
                new ValueType(LostOnDefArcher),
                new ValueType(LostOnDefLightCavalry),
                new ValueType(LostOnDefMountedArcher),
                new ValueType(LostOnDefHeavyCavalry),
                new ValueType(LostOnDefRam),
                new ValueType(LostOnDefCatapult),
                new ValueType(LostOnDefBerserker),
                new ValueType(LostOnDefTrebuchet),
                new ValueType(LostOnDefNobleman),
                new ValueType(LostOnDefPaladin),
            };
        

        #endregion
        public BattleResult()
        {

        }
        public BattleResult(List<BaseUnit> list)
        {
            this.AtkSpearman = list[0].NumberOnAttack;
            this.DefSpearman = list[0].NumberOnDefense;

            this.AtkSwordsman = list[1].NumberOnAttack;
            this.DefSwordsman = list[1].NumberOnDefense;

            this.AtkAxeFighter = list[2].NumberOnAttack;
            this.DefAxeFighter = list[2].NumberOnDefense;

            this.AtkArcher = list[3].NumberOnAttack;
            this.DefArcher = list[3].NumberOnDefense;

            this.AtkLightCavalry = list[4].NumberOnAttack;
            this.DefLightCavalry = list[4].NumberOnDefense;

            this.AtkMountedArcher = list[5].NumberOnAttack;
            this.DefMountedArcher = list[5].NumberOnDefense;

            this.AtkHeavyCavalry = list[6].NumberOnAttack;
            this.DefHeavyCavalry = list[6].NumberOnDefense;

            this.AtkRam = list[7].NumberOnAttack;
            this.DefRam = list[7].NumberOnDefense;

            this.AtkCatapult = list[8].NumberOnAttack;
            this.DefCatapult = list[8].NumberOnDefense;

            this.AtkBerserker = list[9].NumberOnAttack;
            this.DefBerserker = list[9].NumberOnDefense;

            this.AtkTrebuchet = list[10].NumberOnAttack;
            this.DefTrebuchet = list[10].NumberOnDefense;

            this.AtkNobleman = list[11].NumberOnAttack;
            this.DefNobleman = list[11].NumberOnDefense;

            this.AtkPaladin = list[12].NumberOnAttack;
            this.DefPaladin = list[12].NumberOnDefense;
        }

        public void KillAllAtkInfantry()
        {
            LostOnAtkSpearman = AtkSpearman;
            LostOnAtkSwordsman = AtkSwordsman;
            LostOnAtkAxeFighter = AtkAxeFighter;
            LostOnAtkArcher = AtkArcher;
        }
        public void KillAllDefInfantry()
        {
            LostOnDefSpearman = DefSpearman;
            LostOnDefSwordsman = DefSwordsman;
            LostOnDefAxeFighter = DefAxeFighter;
            LostOnDefArcher = DefArcher;
        }
    }
}
