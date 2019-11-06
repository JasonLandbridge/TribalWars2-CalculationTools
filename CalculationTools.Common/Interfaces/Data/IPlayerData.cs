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
        List<CharacterWorld> CharacterWorlds { get; }

        void SetLoginData(ILoginData loginData);

        event EventHandler LoginDataIsUpdated;
        void SetGroups(IList<IGroup> groupList);
        void SetCharacterData(ICharacterData characterData);
        void SetActiveCharacterId(int characterId);
    }
}