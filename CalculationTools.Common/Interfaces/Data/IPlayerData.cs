﻿using System;
using System.Collections.Generic;

namespace CalculationTools.Common.Data
{
    public interface IPlayerData
    {
        DateTime LastUpdated { get; set; }
        bool IsLoggedIn { get; set; }
        int PlayerId { get; set; }
        string Name { get; set; }
        List<CharacterWorld> CharacterWorlds { get; }

        void SetLoginData(ILoginData loginData);

        event EventHandler LoginDataIsUpdated;
    }
}