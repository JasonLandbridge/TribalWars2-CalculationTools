using System;
using System.Collections.Generic;
using System.Text;

namespace CalculationTools.Core
{
    public struct ResourceSet
    {
        public int Wood { get; set; }

        public int Clay { get; set; }

        public int Iron { get; set; }

        public int Total => Wood + Clay + Iron;


        public ResourceSet(int defaultValue = 0)
        {
            Wood = defaultValue;
            Clay = defaultValue;
            Iron = defaultValue;
        }

        public ResourceSet(int wood, int clay, int iron)
        {
            Wood = wood;
            Clay = clay;
            Iron = iron;
        }
    }
}
