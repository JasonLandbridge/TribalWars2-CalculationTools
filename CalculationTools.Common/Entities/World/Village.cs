﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace CalculationTools.Common
{
    public class Village : IVillage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        [NotMapped]
        public Point Position => new Point(X, Y);
    }
}
