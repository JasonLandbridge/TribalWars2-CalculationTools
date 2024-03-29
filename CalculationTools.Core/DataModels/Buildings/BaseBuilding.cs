﻿using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CalculationTools.Core
{
    public abstract class BaseBuilding
    {
        public abstract string Code { get; }
        public string Name => Regex.Replace(this.Code, "([A-Z])", " $1").Trim();

        public abstract List<int> HitpointList { get; }

        public int GetHitPoints(int level)
        {
            if (level == 0) return 0;
            return level - 1 < HitpointList.Count ? HitpointList[level - 1] : 0;
        }

    }
}
