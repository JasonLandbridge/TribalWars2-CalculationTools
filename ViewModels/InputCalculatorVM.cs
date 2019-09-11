using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TribalWars2_CalculationTools.Class;
using System.Windows;

namespace TribalWars2_CalculationTools.ViewModels
{
    public class InputCalculatorVM
    {
        public InputCalculatorClass InputCalculatorClass { get; set; }

        public InputCalculatorVM()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                InputCalculatorClass = new InputCalculatorClass();
            }
        }
    }
}
