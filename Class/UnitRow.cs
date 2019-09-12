using System;
using System.Collections.Generic;
using System.Text;

namespace TribalWars2_CalculationTools.Class
{
    public class UnitRow
    {
        public string ImagePath { get; set; }

        public string Name { get; set; }

        public string Title => Name;
    }
}
