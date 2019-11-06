using System.Collections.Generic;

namespace CalculationTools.Common
{
    public class Character
    {
        public int Id => CharacterId;
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public int CharacterOwnerId { get; set; }
        public string CharacterOwnerName { get; set; }
        public ICollection<World> Worlds { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
