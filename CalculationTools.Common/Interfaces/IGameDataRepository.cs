using System.Collections.Generic;

namespace CalculationTools.Common
{
    public interface IGameDataRepository
    {
        void UpdateVillages(List<IVillage> villages);
        void UpdateWorlds(List<IWorld> worlds);
        void UpdateWorlds(List<ICharacter> characters);
        void UpdateGroups(List<IGroup> groupList);
    }
}