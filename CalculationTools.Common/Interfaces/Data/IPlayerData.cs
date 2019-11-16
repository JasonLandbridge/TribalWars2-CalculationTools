using System;
using System.Collections.Generic;

namespace CalculationTools.Common
{
    public interface IPlayerData
    {
        DateTime LastUpdated { get; set; }
        bool IsLoggedIn { get; set; }
        int PlayerId { get; set; }
        string Name { get; set; }


        void SetGroups(IList<IGroup> groupList);
    }
}